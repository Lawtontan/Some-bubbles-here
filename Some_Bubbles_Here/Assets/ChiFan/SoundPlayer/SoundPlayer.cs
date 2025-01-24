using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundPlayer
{
    public static AudioSource bgm_default;
    public static AudioSource bgm_summer;
    public static AudioSource bgm_rain;
    public static AudioSource bgm_winter;

    public static AudioSource buble_charging;
    public static AudioSource buble_pop;
    public static AudioSource player_normalAttack;
    public static AudioSource player_pickUpAndDownBubble;
    public static AudioSource player_spawnBubble;

    public static AudioSource enemy_spawn;
    public static AudioSource enemy_killed;

    public static AudioSource weatherTransit_summer, weatherTransit_Rain, weatherTransit_winter;


    public static void ToggleBgmDefault(bool isSetActive){
        StopAllBgm();
        if( bgm_default != null){
            bgm_default.Play();
        }
    }

    public static void ToggleBgmSummer(bool isSetActive){
        StopAllBgm();
        if( bgm_summer != null){
            bgm_summer.Play();
        }
    }

    public static void ToggleBgmRain(bool isSetActive){
        StopAllBgm();
        if( bgm_rain != null){
            bgm_rain.Play();
        }
    }

    public static void ToggleBgmWinter(bool isSetActive){
        StopAllBgm();
        if( bgm_winter != null){
            bgm_winter.Play();
        }
    }

    public static void StopAllBgm(){
        bgm_default.Stop();
        bgm_summer.Stop();
        bgm_rain.Stop();
        bgm_winter.Stop();
    }

    /// <summary>
    /// Play the bubble charging sound with the given value to modify the sound
    /// </summary>
    /// <param name="value"></param>
    public static void PlayBubbleCharging(float value){
        if(buble_charging != null){
            buble_charging.Play();
        }
    }

    public static void PlayBublePop(){
        if(buble_pop != null){
            buble_pop.Play();
        }
    }

    public static void PlayPlayerNormalAttack(){
        if(player_normalAttack != null){
            player_normalAttack.Play();
        }
    }

    public static void PlayPlayerPickUpAndDownBubble(){
        if(player_pickUpAndDownBubble != null){
            player_pickUpAndDownBubble.Play();
        }
    }

    public static void PlayPlayerSpawnBubble(){
        if(player_spawnBubble != null) {
            player_spawnBubble.Play();
        }
    }
}
