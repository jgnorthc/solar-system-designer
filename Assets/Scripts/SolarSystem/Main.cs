﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Main Class for SS Game. Handles SimCapi interface.
/// </summary>
public class Main : MonoBehaviour {

    // Program states.
    public enum States
    {
        Startup, Active, Paused, Stopped
    }
    public States State = States.Startup;

    readonly static public String SIM_ID = "SolarSystemDesigner";
    public ExposedData Exposed;
    public PersistentData Persistent;

    SimCapi.Transporter transporter = SimCapi.Transporter.getInstance();

    // Initialization
    void Start () {
        // Create SSDaa 
        Exposed = new ExposedData();
        Exposed.expose();

        SimCapi.Transporter transporter = SimCapi.Transporter.getInstance();
        transporter.addInitialSetupCompleteListener(this.setupComplete);
        transporter.notifyOnReady();
    }

    /// <summary>
    /// Called by SimCapi when the initial Capi snapshot has been applied.
    /// Starts project initalization (objects/etc).
    /// </summary>
    /// <param name="message"></param>
    public void setupComplete(SimCapi.Message message)
    {
        //TODO: Add future init code here.

        // Move to ready state.
        State = States.Active;
    }

    // Frame Update
    void Update () {
        /// Code that occurs every frame, regardless of state.
        //TODO: Add future frame code here.    

        /** Active state only. **/
        if (State != States.Active) return;
        
        //TODO: Add future frame code here.
	}
}