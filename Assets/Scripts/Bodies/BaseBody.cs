﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseBody : MonoBehaviour {

    public int Id {
        get { return id; }
        set { id = value; }
    }
    [SerializeField]
    protected int id;

    public bool Active
    {
        get { return gameObject.activeSelf; }
        set { gameObject.SetActive(value); }
    }
    // Note: Active stored in gameObject.  Property below emulates the primitive for direct read/write -access.
    [SerializeField]
    protected bool active {
        get { return gameObject.activeSelf; }
        set { gameObject.SetActive(value); }
    }

    /// <summary>
    /// Body Type.
    /// </summary>
    public BodyType Type
    {
        get { return type; }
        set { type = value; }
    }
    [SerializeField]
    protected BodyType type = BodyType.Undefined;

    /// <summary>
    /// Unique Body Name.
    /// </summary>
    public string Name
    {
        get { return gameObject.name; }
        set { gameObject.name = value; }
    }

    /// <summary>
    /// Mass in Earths.
    /// </summary>
    public double Mass
    {
        get { return mass; }
        set
        {
            if (value < 0) Debugger.log("Warning: Body '{0}' set to negative mass.".Format(id));
            mass = value;
        }
    }
    [SerializeField]
    protected double mass;

    /// <summary>
    /// Diameter in Earths.
    /// </summary>
    public double Diameter
    {
        get { return diameter; }
        set { diameter = value; }
    }
    [SerializeField]
    protected double diameter;

    /// <summary>x
    /// Current Position in AUs.
    /// </summary>
    public Vector3d Position
    {
        get { return position; }
        set { position.Set(value); }
    }
    [SerializeField]
    protected Vector3d position = new Vector3d();

    /// <summary>
    /// Rotation in Earth Days.
    /// </summary>
    public double Rotation
    {
        get { return rotation; }
        set { rotation = value; }
    }
    [SerializeField]
    protected double rotation;

    /// <summary>
    /// Sum of all vectors on the object.
    /// </summary>
    public Vector3d Velocity
    {
        get { return velocity; }
        set { velocity.Set(value); }
    }
    [SerializeField]
    protected Vector3d velocity = new Vector3d();

    public bool InitialVelocity
    {
        get { return initialVelocity; }
        set { initialVelocity = value;}
    }
    [SerializeField] protected bool initialVelocity;


    public BodyMaterial Material
    {
        get { return material; }
        set { material = value; }
    }
    [SerializeField]
    protected BodyMaterial material = BodyMaterial.Star_Yellow;

    protected GameObject sunLighting;
    protected Light light;

    public void Awake()
    {
        sunLighting = gameObject.transform.GetChild(1).gameObject;
        light = gameObject.GetComponent<Light>();
        
        // Leave in to ensure no issues with extended classes.
    }

    public void Start()
    {
        // Leave in to ensure no issues with extended classes.
    }

    public void Update()
    {
        // Leave in to ensure no issues with extended classes.
    }
}
