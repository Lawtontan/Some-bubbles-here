using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_Constructor : MonoBehaviour{
    public AudioSource bgm_default;
    public AudioSource bgm_summer;
    public AudioSource bgm_rain;
    public AudioSource bgm_winter;

    public AudioSource buble_charging;
    public AudioSource buble_pop;
    public AudioSource player_normalAttack;
    public AudioSource player_pickUpAndDownBubble;
    public AudioSource player_spawnBubble;

    public AudioSource enemy_spawn;
    public AudioSource enemy_killed;

    public AudioSource weatherTransit_summer, weatherTransit_Rain, weatherTransit_winter;

    private void Awake() {
        SoundPlayer.bgm_default = bgm_default;
        SoundPlayer.bgm_summer = bgm_summer;
        SoundPlayer.bgm_rain = bgm_rain;
        SoundPlayer.bgm_winter = bgm_winter;

        SoundPlayer.buble_charging = buble_charging;
        SoundPlayer.buble_pop = buble_pop;
        SoundPlayer.player_normalAttack = player_normalAttack;
        SoundPlayer.player_pickUpAndDownBubble = player_pickUpAndDownBubble;
        SoundPlayer.player_spawnBubble = player_spawnBubble;

        SoundPlayer.enemy_spawn = enemy_spawn;
        SoundPlayer.enemy_killed = enemy_killed;

        SoundPlayer.weatherTransit_summer = weatherTransit_summer;
        SoundPlayer.weatherTransit_Rain = weatherTransit_Rain;
        SoundPlayer.weatherTransit_winter = weatherTransit_winter;
    }
}
