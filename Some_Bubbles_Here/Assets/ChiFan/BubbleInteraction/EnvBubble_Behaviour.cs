using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvBubble_Behaviour : MonoBehaviour
{
    public ControlPanel panel;

    public Transform parent;
    public SphereCollider interactionRange;
    public float maxScale;
    public bool chargingState, attackingState;

    private int maximumHealth_envBubble; //more than this will explode
    private int damagePerSecond_envBubble;
    private int defaultShrinkPerSecond_envBubble;
    private int attackShrinkPerSecond_envBubble;
    private int chargePersecond_envBubble;
    private float attackRange_envBubble;

    private int health; 
    private float lastChargeTime;
    private float scaleIncreasementPerHeath;
    [SerializeField]
    private Enemy_Behaviour attackTarget;
    private void Awake()
    {
        InitEnvBubble();
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
                AttackTarget();
                lastChargeTime = Time.time;
            }
            else if (!chargingState)
            {
                ShrinkEnvBubble();
                lastChargeTime = Time.time;
            }
        
        }
    }

    public void AttackTarget(){
        //if enemy died
        if(attackTarget == null || !attackTarget.parent.gameObject.activeSelf){
            attackTarget = null;
            StartCoroutine(TurnOffAttackingIfNoEnemyFound());
            return;
        }

        attackTarget.ReceiveDamage(damagePerSecond_envBubble);

        health -= attackShrinkPerSecond_envBubble;
        float rate = scaleIncreasementPerHeath * attackShrinkPerSecond_envBubble;
        Vector3 new_scale = new(parent.localScale.x - rate, parent.localScale.y - rate, parent.localScale.z - rate);
        parent.localScale = new_scale;
        if (health <= 0)
        {
            KillBubble();
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

    public void ChargeEnvBubble(float multiplier)
    {
        int increasement = (int)(chargePersecond_envBubble * multiplier);
        health += increasement;
        float rate = (scaleIncreasementPerHeath * increasement);
        Vector3 new_scale = new(parent.localScale.x + rate, parent.localScale.y + rate, parent.localScale.z + rate);
        parent.localScale = new_scale;

        if (health > maximumHealth_envBubble)
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

    public void AttackBubble(int damage)
    {
        health -= damage;
        float rate = scaleIncreasementPerHeath * damage;
        Vector3 new_scale = new(parent.localScale.x - rate, parent.localScale.y - rate, parent.localScale.z - rate);
        parent.localScale = new_scale;
        if (health <= 0)
        {
            KillBubble();
        }
    }

    public void InitEnvBubble()
    {
        health = panel.initialHealth_envBubble;
        maximumHealth_envBubble = panel.maximumHealth_envBubble;
        damagePerSecond_envBubble = panel.damagePerSecond_envBubble;
        defaultShrinkPerSecond_envBubble = panel.defaultShrinkPerSecond_envBubble;
        attackShrinkPerSecond_envBubble = panel.attackShrinkPerSecond_envBubble;
        chargePersecond_envBubble = panel.chargePerSeocnd_envBubble;
        attackRange_envBubble = panel.attackRange_envBubble;

        for(int i = 0; i < parent.childCount; i++){
            Transform child = parent.GetChild(i);
            child.localPosition = Vector3.zero;
            child.rotation = Quaternion.identity;
        }
        
        scaleIncreasementPerHeath = (maxScale - parent.localScale.x) / maximumHealth_envBubble;
        float rate = scaleIncreasementPerHeath * health;
        Vector3 new_scale = new(1 + rate, 1 + rate, 1 + rate);
        parent.localScale = new_scale;

        interactionRange.radius = attackRange_envBubble;

        chargingState = false;
        attackingState = false;
        attackTarget = null;
    }

    private void OnTriggerEnter(Collider other) {

        // **condition to check for wther player is firing is needed** //
        if (other.transform.CompareTag("WeaponBubble"))
        {
            chargingState = true;
        }
        if(attackTarget == null && other.transform.CompareTag("Enemy")){
            attackTarget = other.transform.GetComponent<Enemy_Behaviour>();
            attackingState = true;
        }
        //print("trigger enter" + (Time.time - lastChargeTime));
    }

    private void OnTriggerStay(Collider other) {
        //previous enemy killed, find new enemy
        if(attackingState && attackTarget == null){
            if(other.transform.CompareTag("Enemy")){
                attackTarget = other.transform.GetComponent<Enemy_Behaviour>();
                attackingState = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("WeaponBubble"))
        {
            chargingState = false;
        }
    }

    IEnumerator TurnOffAttackingIfNoEnemyFound(){
        yield return null;
        yield return new WaitForEndOfFrame();

        if(attackTarget == null){
            attackingState = false;
        }
    }
    
}
