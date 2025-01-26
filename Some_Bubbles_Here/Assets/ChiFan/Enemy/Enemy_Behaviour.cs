using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Behaviour : MonoBehaviour
{
    public Enemy_Manager enemyManager;
    public Transform parent;
    //public SphereCollider interactionRange;


    private ControlPanel panel;
    private int heal_enemy;
    private int movingSpeed_enemy;
    private int damagePerSecond_enemy;
    private float attackRange_enemy; //distance allow to start attacking the target


    [SerializeField]
    private Transform attackTarget;
    private EnvBubble_Behaviour bubble_behaviour;
    private float lastAttackTime;
    [SerializeField]
    private bool isAttacking;
    private void Awake()
    {
        InitEnemy();

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(attackTarget == null || !attackTarget.gameObject.activeInHierarchy)
        {
            isAttacking = false;
            attackTarget = null;
            enemyManager.RaiseTargetLost(this);

        }

        if(isAttacking && Time.time - lastAttackTime >= 1)
        {
            bubble_behaviour.AttackBubble(damagePerSecond_enemy);

            lastAttackTime = Time.time;
        }
    }

    /// <summary>
    /// Set the enemy attack target, to make the current enemy start moving towards the attackTarget
    /// </summary>
    /// <param name="attackTarget"></param>
    public void SetAttackTarget(Transform attackTarget){

        if (attackTarget == null) return;

;       this.attackTarget = attackTarget;
        bubble_behaviour = attackTarget.GetComponentInChildren<EnvBubble_Behaviour>();
        StartCoroutine(MoveObject(parent, attackTarget.position));
        
    }

    /// <summary>
    /// Apply reduction to the enemy healty
    /// </summary>
    /// <param name="damage">Value to reduce to enemy health</param>
    public void ReceiveDamage(int damage){
        heal_enemy -= damage;

        if(heal_enemy <= 0)
        {
            KillEnemy();
        }
    }

    /// <summary>
    /// Instantly kill the current enemy
    /// </summary>
    public void KillEnemy(){
        EnemyPool.ResetEnemy(this);
    }

    public void InitEnemy()
    {
        panel = enemyManager.panel;
        heal_enemy = panel.heal_enemy;
        movingSpeed_enemy = panel.movingSpeed_enemy;
        attackRange_enemy = panel.attackRange_enemy;
        damagePerSecond_enemy = panel.damagePerSecond_enemy;

        isAttacking = false;
        attackTarget = null;
        bubble_behaviour = null;

        for(int i = 0; i < parent.childCount; i++){
            Transform child = parent.GetChild(i);
            child.localPosition = Vector3.zero;
            child.rotation = Quaternion.identity;
        }

    }

    IEnumerator MoveObject(Transform trans, Vector3 destination)
    {

        while (!isAttacking)
        {
            trans.position = Vector3.MoveTowards(trans.position, destination, movingSpeed_enemy * Time.deltaTime);
            yield return null;

            //if target died when moving towards target
            if (attackTarget == null || !attackTarget.gameObject.activeInHierarchy)
            {
                enemyManager.RaiseTargetLost(this);
                break;
            }
        }
    }

    // private bool CheckIfTargetInRange()
    // {
    //     float largestScale = Mathf.Max(parent.localScale.x, Mathf.Max(parent.localScale.y, parent.localScale.z));
    //     float effectiveDistance = largestScale * interactionRange.radius;

    //     //check if the attack target is between the interaction range
    //     return Vector3.Distance(parent.position, attackTarget.position) < effectiveDistance;
    // }

    private void OnCollisionEnter(Collision other) {
        //When attack target is in range, start attacking
        if (other.transform.CompareTag("EnvBubble") && attackTarget != null){
            if(other.transform.parent == attackTarget){
                isAttacking = true;
            }
            
        }
    }

    private void OnCollisionExit(Collision other) {
        //When attack target is not in rangen, continue chasing and stop attacking
        if (other.transform.CompareTag("EnvBubble") && attackTarget != null)
        {
            if(other.transform.parent == attackTarget){
                isAttacking = false;
                SetAttackTarget(attackTarget);
            }
            
        }
    }
}
