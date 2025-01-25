using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvBubble_Behaviour : MonoBehaviour
{
    public ControlPanel panel;

    public Transform parent;
    public float maxScale;
    
    private int maximumHealth_envBubble; //more than this will explode
    private int damagePerSecond_envBubble;
    private int defaultShrinkPerSecond_envBubble;
    private int attackShrinkPerSecond_envBubble;
    private int chargePersecond_envBubble;
    private float attackRange_envBubble;

    private int health;
    /// <summary>
    /// 0 for default, 1 for charging, 2 for attacking, 3 for both charging and attacking
    /// </summary>
    private bool chargingState, attackingState; 
    private float lastChargeTime;
    private float scaleIncreasementPerHeath;
    private void Awake()
    {
        health = panel.initialHealth_envBubble;
        maximumHealth_envBubble = panel.maximumHealth_envBubble;
        damagePerSecond_envBubble = panel.damagePerSecond_envBubble;
        defaultShrinkPerSecond_envBubble = panel.defaultShrinkPerSecond_envBubble;
        attackShrinkPerSecond_envBubble = panel.attackShrinkPerSecond_envBubble;
        chargePersecond_envBubble = panel.chargePerSeocnd_envBubble;
        attackRange_envBubble = panel.attackRange_envBubble;

        scaleIncreasementPerHeath = (maxScale - parent.localScale.x) / maximumHealth_envBubble;
        float rate = scaleIncreasementPerHeath * health;
        Vector3 new_scale = new(parent.localScale.x + rate, parent.localScale.y + rate, parent.localScale.z + rate);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - lastChargeTime >= 1) 
        {
            if (chargingState)
            {
                ChargeEnvBubble();
                lastChargeTime = Time.time;
            }

            if (attackingState)
            {
                lastChargeTime = Time.time;
            }
            else if (!chargingState)
            {
                ShrinkEnvBubble();
                lastChargeTime = Time.time;
            }
        
        }
    }

    /// <summary>
    /// To charge up the current bubble base on the set value (chargeRate) in the script
    /// </summary>
    public void ChargeEnvBubble(){

        health += chargePersecond_envBubble;
        float rate = scaleIncreasementPerHeath * chargePersecond_envBubble;
        Vector3 new_scale = new(parent.localScale.x + rate, parent.localScale.y + rate, parent.localScale.z + rate);
        parent.localScale = new_scale;
        if(health > maximumHealth_envBubble)
        {
            KillBubble();
        }

    }

    /// <summary>
    /// To shrink don the current bubble base on the set value (shrinkValue) in the script
    /// </summary>
    public void ShrinkEnvBubble(){

        health -= defaultShrinkPerSecond_envBubble;
        float rate = scaleIncreasementPerHeath * defaultShrinkPerSecond_envBubble;
        Vector3 new_scale = new(parent.localScale.x - rate, parent.localScale.y - rate, parent.localScale.z - rate);
        parent.localScale = new_scale;
        if (health <= 0)
        {
            KillBubble();
        }
    }

    /// <summary>
    /// To instantly destroy the current bubble
    /// </summary>
    public void KillBubble(){

        SoundPlayer.PlayBublePop();
        BubblePool.ResetBubble(this);
    }

    private void OnTriggerEnter(Collider other) {

        // **condition to check for wther player is firing is needed** //
        if (other.transform.CompareTag("WeaponBubble"))
        {
            chargingState = true;
        }
        //print("trigger enter" + (Time.time - lastChargeTime));
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("WeaponBubble"))
        {
            chargingState = false;
        }
    }
}
