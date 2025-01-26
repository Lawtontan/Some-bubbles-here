using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Manager : MonoBehaviour
{
    public ControlPanel panel;
    public Enemy_Behaviour enemy_prefax;

    public Transform topLeft_map;
    public Transform bottomRight_map;

    private float min_height, min_width, max_height, max_width;
    private float minSpawnInterval, maxSpawnInterval;
    private float spawnTriggerTime;
    private void Awake()
    {
        EnemyPool.instance = enemy_prefax;

        min_height = bottomRight_map.position.z;
        min_width = topLeft_map.position.x;
        max_height = topLeft_map.position.z;
        max_width = bottomRight_map.position.z;

        minSpawnInterval = panel.minSpawnInterval_enemy;
        maxSpawnInterval = panel.maxSpawnInterval_enemy;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= spawnTriggerTime)
        {
            spawnTriggerTime = Time.time + Random.Range(minSpawnInterval, maxSpawnInterval);
            SpawnEnemy();
        }
    }

    public void RaiseTargetLost(Enemy_Behaviour enemy)
    {
        Vector3 enemy_pos = enemy.transform.position;
        Transform nearestBubble = null;

        float currentNearest = int.MaxValue;
        foreach (var bubbleParent in BubblePool.activeEnvBubblesParent)
        {
            float distance = Vector3.Distance(enemy_pos, bubbleParent.position);
            if (distance < currentNearest)
            {
                nearestBubble = bubbleParent.transform;
                currentNearest = distance;
            }
        }

        if (nearestBubble != null)
        {
            enemy.SetAttackTarget(nearestBubble);
        }
    }

    public void SpawnEnemy()
    {
        int edgeIndex = Random.Range(0, 4);
        Vector3 position = Vector3.zero;

        switch (edgeIndex)
        {
            //left edge
            case (0):
                position.x = min_width;
                position.z = Random.Range(min_height, max_height);
                break;

            //right edge
            case (1):
                position.x = max_width;
                position.z = Random.Range(min_height, max_height);
                break;

            //top edge
            case (2):
                position.x = Random.Range(min_width, max_width);
                position.z = max_height;
                break;

            //bottom edge
            default:
                position.x = Random.Range(min_width, max_width);
                position.z = min_height;
                break;
        }

        position.y = topLeft_map.position.y;
        EnemyPool.GetEnemy().transform.position = position;
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
