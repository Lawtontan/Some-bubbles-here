using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleInteraction_Manager : MonoBehaviour
{
    public ControlPanel panel;
    public GameObject attackDetection_player;

    public EnvBubble_Behaviour envBubble_prefab;
    public Transform attachPoint_bubble;
    public float pickUpAndDownBubble_duration;

    private Transform currentlyOnhand;
    private EnvBubble_Behaviour currentlyOnhand_behaviour;
    private float currentlyOnHand_initHeight;

    public Transform topLeft_map;
    public Transform bottomRight_map;
    public int initBubble_count;
    private float min_height, min_width, max_height, max_width, spawnTriggerTime;
    private void Awake()
    {
        BubblePool.instance = envBubble_prefab;

        min_height = bottomRight_map.position.z;
        min_width = topLeft_map.position.x;
        max_height = topLeft_map.position.z;
        max_width = bottomRight_map.position.x;

        for(int i = 0; i < initBubble_count; i++){
            BubblePool.GetBubble().transform.position = new(Random.Range(min_width, max_width), topLeft_map.position.y, Random.Range(min_height, max_height));
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //turn off charging state, as if collision set inactive, it ownt detects as exit
        if(!attackDetection_player.activeSelf){
            foreach(var activeBubble in BubblePool.activeEnvBubblesParent){
                activeBubble.GetComponentInChildren<EnvBubble_Behaviour>().chargingState = false;
            }

        }

        if(Time.time >= spawnTriggerTime)
        {
            spawnTriggerTime = Time.time + 5;
            BubblePool.GetBubble().transform.position = new(Random.Range(min_width, max_width), topLeft_map.position.y, Random.Range(min_height, max_height));
        }
    }

    /// <summary>
    /// Pick up the current bubble, required pick up bubble first before putting down bubble
    /// </summary>
    /// <param name="bubble">Transform of the bubble to pick up</param>
    // public void PickUpBubble(Transform bubble){
    //     bubble.parent = attachPoint_bubble;
    //     currentlyOnhand = bubble;
    //     currentlyOnHand_initHeight = bubble.position.y;

    //     currentlyOnhand_behaviour = bubble.GetComponentInChildren<EnvBubble_Behaviour>();
    //     if(currentlyOnhand_behaviour != null)
    //     {
    //         currentlyOnhand_behaviour.transform.GetComponent<Rigidbody>().isKinematic = true;
    //         currentlyOnhand_behaviour.parent.transform.localPosition = Vector3.zero;
    //         currentlyOnhand_behaviour.enabled = false;
    //     }
    //     StartCoroutine(MoveObject(bubble, attachPoint_bubble.position));
    // }

    /// <summary>
    /// Put down the currently holding bubble
    /// </summary>
    public void PutDownBubble(){

        if(currentlyOnhand_behaviour != null)
        {
            currentlyOnhand_behaviour.transform.GetComponent<Rigidbody>().isKinematic = false;
            currentlyOnhand_behaviour.enabled = true;
        }

        Vector3 destination = currentlyOnhand.position;
        destination.y = currentlyOnHand_initHeight;
        StartCoroutine(MoveObject(currentlyOnhand, destination));

        currentlyOnhand.parent = null;
        currentlyOnhand = null;
    }

    /// <summary>
    /// Spawn a bubble object at the given position
    /// </summary>
    /// <param name="position">Vector3 position to spawn the bubble object</param>
    public void SpawnBubble(Vector3 position){

        SoundPlayer.PlayPlayerSpawnBubble();
        BubblePool.GetBubble().transform.position = position;
    }

    public void SpawnBubble()
    {

        SoundPlayer.PlayPlayerSpawnBubble();
        BubblePool.GetBubble().transform.position = new(Random.Range(-10, 10f), 0, Random.Range(-10, 10f));
    }

    IEnumerator MoveObject(Transform trans, Vector3 destination)
    {
        float speed = Vector3.Distance(trans.position, destination) / pickUpAndDownBubble_duration;
        
        while(trans.position != destination)
        {
            trans.position = Vector3.MoveTowards(trans.position, destination, speed * Time.deltaTime);
            yield return null;
        }
    }
}

public static class BubblePool
{
    public static EnvBubble_Behaviour instance;
    public static List<Transform> activeEnvBubblesParent = new();
    static Queue<EnvBubble_Behaviour> pool = new();

    public static void ResetBubble(EnvBubble_Behaviour bubble)
    {
        activeEnvBubblesParent.Remove(bubble.parent.transform);
        pool.Enqueue(bubble);

        bubble.InitEnvBubble();
        bubble.parent.gameObject.SetActive(false);
    }

    public static GameObject GetBubble()
    {
        GameObject op = null;
        if(pool.Count == 0)
        {
            op = GameObject.Instantiate(instance.parent.gameObject);
        }
        else
        {
            op = pool.Dequeue().parent.gameObject;
            
        }

        op.SetActive(true);
        activeEnvBubblesParent.Add(op.transform);
        return op;
    }
}
