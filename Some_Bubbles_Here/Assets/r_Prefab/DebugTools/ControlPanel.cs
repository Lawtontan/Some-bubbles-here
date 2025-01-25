using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPanel : MonoBehaviour
{
    public Indicator overall = Indicator.Overall;
    public int gameDuration_overall;

    public Indicator player = Indicator.Player;
    public int pickUpAndDownBubble_coolDown_player;
    public int spawnBubble_coolDown_player;
    public float attackRange_player;
    public float movingSpeed_player;


    public Indicator enemy = Indicator.Enemy;
    public int heal_enemy;
    public int movingSpeed_enemy;
    public float attackRange_enemy; //distance allow to start attacking the target
    public float minSpawnInterval_enemy;
    public float maxSpawnInterval_enemy;

    public Indicator envBubble = Indicator.EnvBubble;
    public int initialHealth_envBubble;
    public int maximumHealth_envBubble; //more than this will explode
    public int damagePerSecond_envBubble;
    public int defaultShrinkPerSecond_envBubble;
    public int attackShrinkPerSecond_envBubble;
    public float attackRange_envBubble;

    public Indicator weather = Indicator.Weather;
    [Tooltip("The sequence of the season change in the game, season start from index 0")]
    public WeatherType[] weatherSequence = new WeatherType[4];
    public int duration_weather;

    public WeatherType summer = WeatherType.Summer;
    public float bubbleChargeUpMultiplier_summer;

    public WeatherType rain = WeatherType.Rain;
    public float windForce_rain;

    public WeatherType winter = WeatherType.Winter;
    //public float tbh


    public enum WeatherType
    {
        Default,
        Summer,
        Rain,
        Winter
    }

    public enum Indicator
    {
        Overall,
        Player,
        Enemy,
        EnvBubble,
        Weather
    }
}
