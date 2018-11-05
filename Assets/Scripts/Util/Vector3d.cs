﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Vector3d
{
    /// <summary>
    /// Creates a new vector at (0,0,0).
    /// </summary>
    public Vector3d() : this(0, 0, 0) { }

    /// <summary>
    /// Creates a new vector at choosen coordinates.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    /// <param name="magnatude"></param>
    public Vector3d(double x, double y, double z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    /// <summary>
    /// Creates a vector, duplicating the coordinates of a Unity vector.
    /// </summary>
    /// <param name="vector"></param>
    public Vector3d(Vector3 vector)
    {
        this.x = vector.x;
        this.y = vector.y;
        this.z = vector.z;
    }

    /// <summary>
    /// Creates vector at the current positon of another vector.
    /// </summary>
    /// <param name="vector"></param>
    public Vector3d(Vector3d vector)
    {
        this.x = vector.x;
        this.y = vector.y;
        this.z = vector.z;
    }

    // Position
    public double x { set; get; }
    public double y { set; get; }
    public double z { set; get; }
    public double magnatude { 
        get
        {
            return Math.Sqrt(x * x + y * y + z * z);
        }
    }  

    /// <summary>
    /// Moves the vector to another position.
    /// </summary>
    /// <param name="vector"></param>
    public void moveTo(Vector3d vector) {
        x = vector.x;
        y = vector.y;
        z = vector.z;
    }
    /// <summary>
    /// Converts Vector3d to Unity Vector3. Some precision may be lost.
    /// </summary>
    public Vector3 toVector3()
    {
        Vector3 result = new Vector3((float)x, (float)y, (float)z);
        return new Vector3((float)x, (float)y, (float)z);
    }

    /// <summary>
    /// Returns distance between this vector and another one.
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    public double getDistance(Vector3d target)
    {
        double xPow = Math.Pow(x - target.x, 2);
        double yPow = Math.Pow(y - target.y, 2);
        double zPow = Math.Pow(z - target.z, 2);
        return Math.Sqrt(xPow + yPow + zPow);
    }

}
