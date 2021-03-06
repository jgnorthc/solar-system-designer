﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// This class handles all movement and control of the camera
/// </summary>
public class CameraControls : MonoBehaviour
{

    private static float PosMult = 30492.3f;

    /// <summary>
    /// Method used to enable and disable the keyboard
    /// </summary>
    public bool EnableKeyboard
    {
        get { return enableKeyboard; }
        set { enableKeyboard = value; }
    }
    [SerializeField]
    private bool enableKeyboard = true;

    /// <summary>
    /// This method is used to enable and disable the
    /// mouse functionality
    /// </summary>
    public bool EnableMouse
    {
        get { return enableMouse;  }
        set { enableMouse = value; }
    }
    [SerializeField]
    private bool enableMouse = true;
    
    public static Body Body
    {
        get { return body;  }
        set { body = value; }
    }
    [SerializeField]
    private static Body body;
    
    public float DragSpeed
    {
        get { return dragSpeed; }
        set { dragSpeed = value; }
    }
    [SerializeField,Range(50,500)]
    private float dragSpeed = 100f;
    
    // Basic Constants
    const float KEYBOARD_MOVE = 1000F;
    private const float FOVAdjust = .1f;
    private const float rotateSpeed = 10f;
    
    private Vector3 dragOriginRot;
    private Vector3 dragOriginPos;
    [Range(.1f,5f)]
    public float mouseZoomFactor = 1f;
    public static float zoomlevel = 1f;
    
    private static Vector3 offset;
    public static Vector3 bodyPos;
    private Transform cam;

    private List<Image> backgrounds = new List<Image>();

    private void Start()
    {
        var images = Resources.FindObjectsOfTypeAll<Image>();
        foreach (var image in images)
        {
            if (image.tag == "UIMenu")
            {
                backgrounds.Add(image);
            }
        }

        if (body != null)
        {
            bodyPos = body.Position.Vec3 * 10;
            focusOnBody();
        }

        //numBodies = Sim.Bodies.Active.Count;
    }

    /// <summary>
    /// This function is used to update the frame once
    /// per second.
    /// </summary>
    void Update()
    {
        setControlsActive();
        if (EnableKeyboard)
            UpdateKeyboard();
        if (EnableMouse)
            UpdateMouse();
        if (zoomlevel < 1f)
            zoomlevel = 1f;
    }

    private void LateUpdate()
    {
//        if (Sim.Bodies.Active.Count != numBodies)
//        {
//            focusMass();
//        }
        if (body != null)
        {
            bodyPos = body.Position.Vec3 * 10;
            setCameraPos();
        }
    }

    /// <summary>
    /// This function Updates movement position based on the keyboard.
    /// </summary>
    void UpdateKeyboard()
    { 
        // Move Factor is based on Base movement speed * User Preferences.
        float MoveFactor = KEYBOARD_MOVE * Time.deltaTime * (float)Preferences.Keyboard.Movement;

        // Get Controls
        var controls = Sim.Settings.KeyControls;
        
        //Up
        if (Input.GetKey(controls[Direction.MoveUp]))
        {
            Camera.main.transform.position -= Camera.main.transform.up * MoveFactor;
        }

        //Down
        if (Input.GetKey(controls[Direction.MoveDown]))
        {
            Camera.main.transform.position += Camera.main.transform.up * MoveFactor;
        }

        //Left
        if (Input.GetKey(controls[Direction.MoveLeft]))
        {
            Camera.main.transform.position -= Camera.main.transform.right * MoveFactor;
        }

        //Right
        if (Input.GetKey(controls[Direction.MoveRight]))
        {
            Camera.main.transform.position += Camera.main.transform.right * MoveFactor;
        }

        // Move Forward
        if (Input.GetKey(controls[Direction.MoveForward]))
        {
            Camera.main.transform.position += Camera.main.transform.forward * MoveFactor;
        }

        // Move Backward
        if (Input.GetKey(controls[Direction.MoveBackward]))
        {
            Camera.main.transform.position -= Camera.main.transform.forward * MoveFactor;
        }

        // Zoom In
        if (Input.GetKey(controls[Direction.ZoomIn]))
        {
            Camera.main.fieldOfView += FOVAdjust;
        }

        // Move Backward
        if (Input.GetKey(controls[Direction.ZoomOut]))
        {
            Camera.main.fieldOfView -= FOVAdjust;
        }

        // Rotate Camera (note, the vectors are correct despite not matching the key)
        Vector3 rotVect = Vector3.zero;
        if (Input.GetKey(controls[Direction.RotRight]))
            rotVect = Vector3.down*rotateSpeed*Time.deltaTime;
        if (Input.GetKey(controls[Direction.RotLeft]))
            rotVect = Vector3.up*rotateSpeed*Time.deltaTime;
        if (Input.GetKey(controls[Direction.RotDown]))
            rotVect = Vector3.left*rotateSpeed*Time.deltaTime;
        if (Input.GetKey(controls[Direction.RotUp]))
            rotVect = Vector3.right*rotateSpeed*Time.deltaTime;
        if (rotVect != Vector3.zero)
            cam.Rotate(rotVect,Space.Self);
    }

    /// <summary>
    /// This function Updates movement position based on the mouse.
    /// </summary>
    private void UpdateMouse()
    {
        zoomlevel += Input.mouseScrollDelta.y*mouseZoomFactor;
        
        if (Input.GetMouseButton(1) && !dragOriginRot.Equals(Vector3.negativeInfinity))
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOriginRot)*-dragSpeed;
            float x = pos.x;
            float y = pos.y;
            pos.x = y;
            pos.y = -x;
            pos.z = 0;
            Camera.main.transform.Rotate(pos,Space.Self);
           
            dragOriginRot = Input.mousePosition;
            return;
        }
        else
        {
            dragOriginRot = Vector3.negativeInfinity;
        }

        if (Input.GetMouseButtonDown(0))
        {
            dragOriginPos = Input.mousePosition;
        }
        if (Input.GetMouseButtonDown(1))
        {
            dragOriginRot = Input.mousePosition;
        }
        
    }

    public static void changeBody(String bodyName)
    {
        body = Sim.Bodies.get(bodyName);
        Sim.selectedBody = body;
        focusOnBody();
    }

    private static void setCameraPos()
    {
        Quaternion rotation = Camera.main.transform.rotation;
        Vector3 pos = offset * zoomlevel;
        Camera.main.transform.position = rotation * pos;
    }

    private static void focusOnBody()
    {
        zoomlevel = 2f;
        float zoomBase = (float)body.Diameter * 2.5F;
        /*Camera.main.transform.rotation = Quaternion.identity;*/
        Camera.main.transform.rotation = Quaternion.LookRotation(Vector3.down,Vector3.up);
        Camera.main.transform.position = Vector3.forward * zoomBase;
        offset = Vector3.zero - Camera.main.transform.position;
        Debug.Log(offset.magnitude);
    }
    

    private void setControlsActive()
    {
        // Automatically disable Camera controls if the current object is an InputField.
        enableKeyboard = true;
        enableMouse = true;
        foreach(Image i in backgrounds)
        {
            if (i.isActiveAndEnabled)
            {
                enableKeyboard = false;
                enableMouse = false;
                break;
            }
        }
    }
}
