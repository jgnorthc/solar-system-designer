﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stores all data for the NBody system and handles over-time calculations of body locations.
/// </summary>
public class NBody {
    
    public NBody()
    {
        Bodies = new Dictionary<string, Body>();
        Speed = 0;
    }

    /// <summary>
    /// List of all bodies within the system.
    /// </summary>
    public Dictionary<string, Body> Bodies { get; private set; }

    /// <summary>
    /// Current time for the simulation, in N days from starting points.
    /// </summary>
    public double Days { get; set; }

    /// <summary>
    /// Amount of days that pass every frame. Intially starts at 0.
    /// </summary>
    public double Speed { get; set; }

	// Update is called once per frame
	void Update () {
        Days += Speed;	
	}
    
    /// <summary>
    /// Returns the current distance between two bodies.
    /// </summary>
    /// <param name="body1"></param>
    /// <param name="body2"></param>
    /// <returns></returns>
    public double getCurrentDistance(Body body1, Body body2)
    {
        return body1.GetDistance(body2, Days);
    }

    /// <summary>
    /// Returns the current position of a body.
    /// </summary>
    /// <param name="body"></param>
    /// <returns></returns>
    public Vector3d getCurrentPosition(Body body)
    {
        return body.GetPosition(Days);
    }

}
