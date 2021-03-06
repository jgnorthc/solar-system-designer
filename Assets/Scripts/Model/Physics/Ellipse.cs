﻿/// <summary>
/// This Class is used for the creation of an Ellipse object
/// This class contains setters and getter methods for an
/// ellipse object.
///
/// @author Jack Northcutt
/// </summary>
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Ellipse{

	/// <summary>
	/// gets and sets the major axis length of the ellipse
	/// </summary>
    public double MajorAxis
    {
        get { return majorAxis; }
        set { majorAxis = value; }
    }
    [SerializeField]
	private double majorAxis;

	/// <summary>
	/// gets and sets minor axis length of the ellipse
	/// <summary>
    public double MinorAxis
    {
        get { return minorAxis; }
        set  { minorAxis = value; }
    }
    [SerializeField]
    private double minorAxis;


    /// <summary>
    /// constructor
    /// </summary>
    public Ellipse(double major, double minor){
		this.MajorAxis = major;
		this.MinorAxis = minor;
	}

	/// <summary>
	/// this method yields a double value that represents
	/// how far the foci are offset from the center of
	/// the ellipse. The foci is where the sun will be placed.
	/// </summary>
	public double calcFoci(){
		double a = System.Math.Pow(this.MajorAxis/2, 2);
		double b = System.Math.Pow(this.MinorAxis/2, 2);

		double foci = System.Math.Sqrt(a-b);

		return foci;
	}

	/// <summary>
	/// This function calculates the eccentricity of an ellipse,
	/// which is the elongation ratio of an ellipse. The closer
	/// to zero the more circular the ellipse is and the closer
	/// to 1 the more elongated the ellipse is. 
	/// </summary>
	public double calcEccentricity(){
		double foci = calcFoci();
		double aSqr = System.Math.Pow(MinorAxis/2, 2);
		double bSqr = System.Math.Pow(foci, 2);
		double cSqr = aSqr + bSqr;
		double c = System.Math.Sqrt(cSqr);
		double e = foci/c;

		return e;
	}

	/// <summary>
	/// This Function takes a double angle and calculates 
	/// the x y and z position of where the point lies 
	/// on the ellipse. THis function returns a Vector3D
	/// </summary>
	public Vector3d calcPoint(double theta){
		//converting the angle give to radians
		theta = (theta * System.Math.PI)/ 180;

		//calculating x y and defaulting z to zero
		double x = (this.MajorAxis/2) * System.Math.Cos(theta);

		double y = (this.MinorAxis/2) * System.Math.Sin(theta);

		double z = 0.0;

		//createing a vector and returning
		Vector3d coords = new Vector3d(x,y,z);

		return coords;
	}

	/// <summary>
	/// This function is to deterime the y coordinate
	/// when given an x value
	/// </summary>
	public double findY(double x){
			//squrar each of the values
			double a= System.Math.Pow(MajorAxis, 2);
			double b = System.Math.Pow(MinorAxis, 2);
			x = System.Math.Pow(x, 2);

			//use equaltion (x^2)/a^2 + (y^2)/b^2 = 1
			double y = System.Math.Sqrt((1 - (x/a))*b);

			//return the new why value and combined with
			//x with give you the coordinates
			return y;
	}

	/// <summary>
	/// This function is to determine the x coordinate
	/// when given a y value.
	/// </summary>
	public double findx(double y){
			//squrar each of the values
			double a= System.Math.Pow(MajorAxis, 2);
			double b = System.Math.Pow(MinorAxis, 2);
			y = System.Math.Pow(y, 2);

			//use equaltion (x^2)/a^2 + (y^2)/b^2 = 1
			double x = System.Math.Sqrt((1 - (y/b))*a);

			//return the new why value and combined with
			//x with give you the coordinates
			return x;
	}

}
