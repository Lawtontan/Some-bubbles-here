using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Manager : MonoBehaviour
{
    public ControlPanel panel;
    public Enemy_Behaviour enemy_prefax;

    private void Awake()
    {
        EnemyPool.instance = enemy_prefax;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RaiseTargetLost(Enemy_Behaviour enemy)
    {
        Vector3 enemy_pos = enemy.transform.position;
        Transform nearestBubble = null;

        float currentNearest = int.MaxValue;
        foreach(var bubbleParent in BubblePool.activeEnvBubbles)
        {
            float distance = Vector3.Distance(enemy_pos, bubbleParent.position);
            if (distance < currentNearest)
            {
                nearestBubble = bubbleParent.transform;
                currentNearest = distance;
            }
        }

        if(nearestBubble != null)
        {
            enemy.SetAttackTarget(nearestBubble);
        }
    }

    public void SpawnEnemy()
    {
        EnemyPool.GetEnemy().transform.position = Vector3.zero;
    }
}

public static class EnemyPool
{
    public static Enemy_Behaviour instance;
    static Queue<Enemy_Behaviour> pool = new();

    public static void ResetEnemy(Enemy_Behaviour enemy)
    {
        pool.Enqueue(enemy);

        enemy.InitEnemy();
        enemy.parent.gameObject.SetActive(false);
    }

    public static GameObject GetEnemy()
    {
        GameObject op = null;
        if (pool.Count == 0)
        {
            op = GameObject.Instantiate(instance.parent.gameObject);
        }
        else
        {
            op = pool.Dequeue().parent.gameObject;
        }

        op.SetActive(true);
        return op;
    }
}
