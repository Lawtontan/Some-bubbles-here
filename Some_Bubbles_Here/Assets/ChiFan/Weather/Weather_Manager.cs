using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weather_Manager : MonoBehaviour
{
    public ControlPanel panel;
    public WeatherSwitcher weatherSwitcher;
    public Player_Behaviour player_Behaviour;
    public int weatherIndex;

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
        weatherIndex++;
        
        switch (weatherIndex)
        {
            case (1):
                StartCoroutine(Summer());
                weatherSwitcher.SetWeatherSummer();
                break;

            case (2):
                StartCoroutine(Rain());
                weatherSwitcher.SetWeatherRain();
                break;

            case (3):
                StartCoroutine(Winter());
                weatherSwitcher.SetWeatherWinter();
                break;

            default:
                break;
        }
    }

    /// <summary>
    /// Get the current season, start from 0
    /// </summary>
    /// <returns>int value that represent the current season, base on the season sets to this script</returns>
    public int GetCurrentSeason(){
        return 0;
    }

    IEnumerator Summer()
    {
        SoundPlayer.ToggleBgmSummer(true);

        int minInterval = panel.minInterval_summer;
        int maxInterval = panel.maxInterval_summer;
        float multiplier = panel.bubbleChargeUpMultiplier_summer;
        float timeTrigger = Time.time + Random.Range(minInterval, maxInterval);

        int index = weatherIndex;
        while(index == weatherIndex)
        {
            if(Time.time < timeTrigger)
            {
                yield return null;
                continue;
            }

            foreach(var activeBubble in BubblePool.activeEnvBubblesParent)
            {
                activeBubble.GetComponentInChildren<EnvBubble_Behaviour>().ChargeEnvBubble(multiplier);
            }
            
            timeTrigger = Time.time + Random.Range(minInterval, maxInterval);
        }
    }

    IEnumerator Rain()
    {
        SoundPlayer.ToggleBgmRain(true);

        int minInterval = panel.minInterval_rain;
        int maxInterval = panel.maxInterval_rain;
        float force = panel.windForce_rain;
        float timeTrigger = Time.time + Random.Range(minInterval, maxInterval);

        int index = weatherIndex;
        Vector3 direction = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
        while(index == weatherIndex)
        {
            foreach(var activeBubble in BubblePool.activeEnvBubblesParent)
            {
                activeBubble.position += direction * force * Time.deltaTime;
            }

            yield return null;

            //change the direction after a random duration
            if(Time.time > timeTrigger)
            {
                direction = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
                timeTrigger = Time.time + Random.Range(minInterval, maxInterval);
            }
        }
    }

    IEnumerator Winter()
    {
        SoundPlayer.ToggleBgmWinter(true);

        int minInterval = panel.minInterval_winter;
        int maxInterval = panel.maxInterval_winter;
        float multipler = panel.speedMultiplier_winter;
        float timeTrigger = 0; // 0 to allow it to apply freeze right when winter is enter

        bool isFreezed = true;
        int index = weatherIndex;
        while (index == weatherIndex)
        {
            //toggle the freeze effect when time hts, if freexed then apply the multiplier to player speed, else set it back to default
            if(Time.time > timeTrigger)
            {
                float value = isFreezed ? multipler : 1;
                player_Behaviour.ToggleFreeze(value);

                isFreezed = !isFreezed;
                timeTrigger = Time.time + Random.Range(minInterval, maxInterval);
            }

            yield return null;
        }

        //set it back to default
        player_Behaviour.ToggleFreeze(1);
    }
}
