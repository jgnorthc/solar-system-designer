using Model.Util;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Collision: MonoBehaviour 
{
    public const double AU_TO_E = 11727.64743650047;

    public static bool isCollided(Body b1, Body b2)
    {
        double totalRadius = (b1.Diameter + b2.Diameter) / 4;
        Vector3d p1 = b1.Position;
        Vector3d p2 = b2.Position;
        return totalRadius >= (p2 - p1).magnitude * AU_TO_E;
    }

    public static void checkCollisions()
    {
        List<Body> bodies = Sim.Bodies.Active;
        for(int i=0;i<bodies.Count;i++)
        {
            Body body1 = bodies[i];
            for (int j = i+1; j < bodies.Count; j++)
            {
                Body body2 = bodies[j];
                if (isCollided(body1, body2))
                {
                    Sim.Event.Log(SimEvent.Collision, body1.Name, body2.Name);
                    Bodies.deactivate(body1);
                    Bodies.deactivate(body2);
                    GameObject effect = UnityEngine.Object.Instantiate(Sim.Config.CollisionEffect, Sim.Config.BodyContainer.transform);
                    effect.GetComponent<MeshRenderer>().material.color = new Color32(120,98,98,255);
                    effect.transform.position = body1.transform.position;
                }
            }
        }
    }

    public void Update()
    {
        if (Sim.Settings.Paused) return;
        checkCollisions();
    }
}