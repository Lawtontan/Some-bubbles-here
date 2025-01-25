using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherSwitcher : MonoBehaviour
{
    [Header("Use key U,I,O,P to change the weather")]
    public Light directionalLight;
    public Color defaultLight;
    public Material defaultSky;
    public Color summerLight;
    public Material summerSky;
    public Color rainLight;
    public Material rainSky;
    public Color winterLight;
    public Material winterSky;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.U)){
            SoundPlayer.ToggleBgmDefault(true);
            RenderSettings.skybox = defaultSky;
            directionalLight.color = defaultLight;
        }
        else if(Input.GetKeyDown(KeyCode.I)){
            SoundPlayer.ToggleBgmSummer(true);
            RenderSettings.skybox = summerSky;
            directionalLight.color = summerLight;
        }
        else if(Input.GetKeyDown(KeyCode.O)){
            SoundPlayer.ToggleBgmRain(true);
            RenderSettings.skybox = rainSky;
            directionalLight.color = rainLight;
        }   
        else if(Input.GetKeyDown(KeyCode.P)){
            SoundPlayer.ToggleBgmWinter(true);
            RenderSettings.skybox = winterSky;
            directionalLight.color = winterLight;
        }
    }
}
