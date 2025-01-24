using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weather_Manager : MonoBehaviour
{
    [Tooltip("The sequence of the season change in the game, season start from index 0")]
    public WeatherType[] weatherSequence;
    public enum WeatherType{
        Default,
        Summer,
        Rain,
        Winter
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Start the next season sets to this script
    /// </summary>
    public void GoToNextSeason(){

    }

    /// <summary>
    /// Get the current season, start from 0
    /// </summary>
    /// <returns>int value that represent the current season, base on the season sets to this script</returns>
    public int GetCurrentSeason(){
        return 0;
    }
}
