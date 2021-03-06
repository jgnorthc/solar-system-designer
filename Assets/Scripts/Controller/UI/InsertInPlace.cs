using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using Model.Util;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Util;

/// <summary>
/// This class places an orbital body a specified postion
/// </summary>
public class InsertInPlace : MonoBehaviour
{
    public Button button;
    public GameObject bodyBase;
    public TMP_InputField objName;
    public TMP_Dropdown mat;
    public TMP_Dropdown type;
    public TMP_InputField radius;
    public TMP_InputField mass;
    public TMP_InputField xPos;
    public TMP_InputField yPos;
    public TMP_InputField zPos;
    public Toggle autoVel;
    public TMP_InputField initialVel;

    public TextMeshProUGUI errorText;

    public GameObject UseParticleSystem;

    private UnitType unitType;

    


public void Awake()
    {
        // Load Material List from BodyMaterial.cs
        List<string> matList = new List<string>();
        foreach (String name in Enum.GetNames(typeof(BodyMaterial)))
        {
            matList.Add(name.Replace("_", ": "));
        }
        // Loads BodyMaterials into Material list
        mat.ClearOptions();
        mat.AddOptions(matList);
    }


    /// <summary>
    /// initializes class and begins listening for mouse click
    /// </summary>
    void Start()
    {
        button.onClick.AddListener(insert);
        type.onValueChanged.AddListener(delegate { updateUnits(); });
        unitType = UnitType.Absolute;
        updateUnits();
    }

    private void updateUnits()
    {
        UnitType unit = UnitConverter.unitTypes[type.options[type.value].text.ToLower()];

        GameObject[] comps = GameObject.FindGameObjectsWithTag("Radius");
        foreach (var comp in comps)
        {
            TMP_InputField inp = comp.GetComponentInChildren<TMP_InputField>();
            TextMeshProUGUI suff = comp.transform.Find("Unit").gameObject.GetComponent<TextMeshProUGUI>();
            Debug.Log(suff);
            updateRadius(inp,unit);
            suff.text = UnitConverter.units[unit].DistSuff;
        }
        comps = GameObject.FindGameObjectsWithTag("Mass");
        foreach (var comp in comps)
        {
            TMP_InputField inp = comp.GetComponentInChildren<TMP_InputField>();
            TextMeshProUGUI suff = comp.transform.Find("Unit").gameObject.GetComponent<TextMeshProUGUI>();
            updateMass(inp,unit);
            suff.text = UnitConverter.units[unit].MassSuff;
        }

        unitType = unit;
    }
    
    private void updateRadius(TMP_InputField val, UnitType unit)
    {
        double v = double.Parse(val.text);
        val.text = UnitConverter.convertRadius(v, unitType, unit).ToString();
    }
    private void updateMass(TMP_InputField val, UnitType unit)
    {
        double v = double.Parse(val.text);
        val.text = UnitConverter.convertMass(v, unitType, unit).ToString();
    }

    /// <summary>
    /// This function places each body in the array in its correct position
    /// </summary>
    public void insert()
    {
        GameObject obj = Sim.Bodies.activateNext();
        Body script = obj.GetComponent<Body>();
        script.Name = objName.text;

        script.Material = mat.options[mat.value].text.Enum<BodyMaterial>();
        
        
        
        try
        {
            script.Position = new Vector3d(double.Parse(xPos.text.IfBlank("0")), double.Parse(yPos.text.IfBlank("0")), double.Parse(zPos.text.IfBlank("0")));
            if (!autoVel.isOn)
            {
                script.Vel = new Vector3d(0, 0, double.Parse(initialVel.text.IfBlank("0")));
                script.isInitialVel = !autoVel.enabled;
            }
        }
        catch (Exception)
        {
            script.Position = new Vector3d(0.0, 0.0, 0.0);
            Debugger.log("Invalid Position for Insert. Using base of (0,0,0)");
        }
        
        if (double.Parse(radius.text.IfBlank("1")) > 0 || double.Parse(mass.text.IfBlank("1")) > 0)
        {
            script.Diameter = UnitConverter.convertRadius(double.Parse(radius.text.IfBlank("1")), unitType, UnitType.Earths);
            script.Mass = double.Parse(mass.text.IfBlank("1"));
        } else {
            //Set values back to 1
            radius.text = "1";
            mass.text = "1";
            //Set Error message window
            string text = errorText.text;
            errorText.text = text.Replace("{error_msg}", "Invalid Mass and Radius. Value must be greater than 0.");
            //Set Debugger
            Debugger.log("Invalid Mass and Radius. Value must be greater than 0.");
        }
        
        script.Type = script.whatAmI();

    }

    /// <summary>
    /// scales radius
    /// </summary>
    private double convertRadiUnits(double value)
    {
        return value * 6.3781;
    }
}