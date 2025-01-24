using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Behaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Set the enemy attack target, to make the current enemy start moving towards the attackTarget
    /// </summary>
    /// <param name="attackTarget"></param>
    public void SetAttackTarget(Transform attackTarget){

    }

    /// <summary>
    /// Apply reduction to the enemy healty
    /// </summary>
    /// <param name="damage">Value to reduce to enemy health</param>
    public void ReceiveDamage(int damage){

    }

    /// <summary>
    /// Instantly kill the current enemy
    /// </summary>
    public void KillEnemy(){

    }

    private void OnCollisionEnter(Collision other) {
        
    }
}
