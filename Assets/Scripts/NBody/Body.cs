﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public enum BodyType
{
    Unclassified, Sun, Planet, Moon, Astroid
}

public class Body
{

    /// <summary>
    /// Create an unclassfied Body at (0,0,0).
    /// </summary>
    /// <param name="name"></param>
    public Body(String name) : this(name, BodyType.Unclassified) { }

    /// <summary>
    /// Creates a body at (0,0,0).
    /// </summary>
    /// <param name="name"></param>
    /// <param name="type"></param>
    public Body(String name, BodyType type) : this(name, type, new Vector3d()) { }

    /// <summary>
    /// Creates a new body.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="type"></param>
    /// <param name="position"></param>
    public Body(string name, BodyType type, Vector3d position)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentException("Name is null.");
        }
       
        if (position == null)
        {
            throw new ArgumentNullException("Position is null.");
        }

        this.Name = name;
        this.Type = type;
        this.Position = position;
        this.Layers = new List<Layer>();
    }

    public string Name { get; set; }
    public Vector3d Position { get; private set; }
    public BodyType Type { get; set; }

    /// <summary>
    /// Mass in kg.
    /// </summary>
    public double Mass { get; set; }

    /// <summary>
    /// Average radius of the body.
    /// </summary>
    public double Radius { get; set; }

    /// <summary>
    /// Individual Layers, in order from the outside in. Atmosphere, Crust, Mandle, etc.  May contain
    /// limited information and simplified layers based on current knowledge. 
    /// </summary>
    public List<Layer> Layers { get; private set; }

    /// <summary>
    /// Retrives the full composition of the planet. Any changes needs to be made at the individual layer. 
    /// </summary>
    public Dictionary<Compound, double> getComposition()
    {
        Dictionary<Compound, double> result = new Dictionary<Compound, double>();
        foreach (Layer layer in Layers)
        {
            foreach (Compound compound in layer.Composition.Keys)
            {
                // Adds the new compound ammounts to the current one.
                result.Add(compound, layer.Composition[compound] +
                    (result.ContainsKey(compound) ? result[compound] : 0)
                    );
            }
        }
        return result;
    }

    public void setPosition(Vector3d position)
    {
        Position.x = position.x;
        Position.y = position.y;
        Position.z = position.z;

    }
       
    /// <summary>
    /// Returns the core(center point) distance between two bodies.
    /// </summary>
    /// <param name="target">Second body to compare to.</param>
    /// <returns>Distance in meters.</returns>
    public double getDistance(Body target)
    {
        return Position.getDistance(target.Position);
    }

}
