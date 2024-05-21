using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Lightning : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LightingSettings lightingSettings = new LightingSettings();

        // Configure the LightingSettings object
        lightingSettings.albedoBoost = 8.0f;

        //RenderSettings.ambientMode.Skybox = "Color";

        // Assign the LightingSettings object to the active Scene
        Lightmapping.lightingSettings = lightingSettings;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
