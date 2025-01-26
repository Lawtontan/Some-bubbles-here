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

    public Material[] map_material;
    public Material[] grass_material;
    public MeshRenderer[] grass_mr;
    public MeshRenderer map_mr;
    public GameObject[] leafs;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.U))
        {
            SetWeatherDefault();
        }
        else if(Input.GetKeyDown(KeyCode.I))
        {
            SetWeatherSummer();
        }
        else if(Input.GetKeyDown(KeyCode.O))
        {
            SetWeatherRain();
        }
        else if(Input.GetKeyDown(KeyCode.P))
        {
            SetWeatherWinter();
        }
    }

    public void SetWeatherWinter()
    {
        SoundPlayer.ToggleBgmWinter(true);
        RenderSettings.skybox = winterSky;
        directionalLight.color = winterLight;


        map_mr.material = map_material[3];
        foreach(var leaf in leafs){
            leaf.SetActive(false);
        }
        foreach(var grass in grass_mr){
            grass.material = grass_material[3];
        }
    }

    public void SetWeatherRain()
    {
        SoundPlayer.ToggleBgmRain(true);
        RenderSettings.skybox = rainSky;
        directionalLight.color = rainLight;
    }

    public void SetWeatherSummer()
    {
        SoundPlayer.ToggleBgmSummer(true);
        RenderSettings.skybox = summerSky;
        directionalLight.color = summerLight;
    }

    public void SetWeatherDefault()
    {
        SoundPlayer.ToggleBgmDefault(true);
        RenderSettings.skybox = defaultSky;
        directionalLight.color = defaultLight;
    }
}
