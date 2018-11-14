﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stores all data for the NBody system and handles over-time calculations of body locations.
/// </summary>
public class NBody: Dictionary<string, Body> {

    public NBody() : base() { } 

    /// <summary>
    /// List of all bodies within the system.
    /// </summary>
    //public Dictionary<string, Body> Bodies { get; private set; }

    /// <summary>
    /// Current time for the simulation, in N days from starting points.
    /// </summary>
    public double Time
    {
        get { return time; }
        set
        {
            time = value;
            Main.Instance.Exposed.Time.setValue((float)value);
        }
    }
    private double time;

    /// <summary>
    /// Amount of days that pass every frame. Intially starts at 0.
    /// </summary>
    public double Speed { get; set; }

    /// <summary>
    /// Name of the body that currently has focus.
    /// 
    /// If Null or empty string, focus is on coordinates (0,0,0).
    /// </summary>
    public string Focused {
        get { return focused; }
        set
        {
            focused = value;
            // Instantly updates Exposed value.
            Main.Instance.Exposed.FocusedBody.setValue(value);
        }
    }
    private string focused;

	// Update is called once per frame.
	void Update () {
        // Time is updated internally so Capi doesn't require an update on every frame. 
        time += Speed;	
	}
    
    /// <summary>
    /// Returns the current distance between two bodies.
    /// </summary>
    /// <param name="body1"></param>
    /// <param name="body2"></param>
    /// <returns></returns>
    public double getCurrentDistance(Body body1, Body body2)
    {
        return body1.GetDistance(body2, Time);
    }

    /// <summary>
    /// Returns the current position of a body.
    /// </summary>
    /// <param name="body"></param>
    /// <returns></returns>
    public Vector3d getCurrentPosition(Body body)
    {
        return body.GetPosition(Time);
    }

}
