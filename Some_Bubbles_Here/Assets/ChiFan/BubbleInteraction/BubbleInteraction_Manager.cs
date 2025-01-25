using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleInteraction_Manager : MonoBehaviour
{
    public GameObject envBubble_prefab;
    public Transform attachPoint_bubble;
    public float pickUpAndDownBubble_duration;

    private Transform currentlyOnhand;
    private EnvBubble_Behaviour currentlyOnhand_behaviour;
    private float currentlyOnHand_initHeight;

    private void Awake()
    {
        BubblePool.instance = envBubble_prefab.GetComponentInChildren<EnvBubble_Behaviour>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Pick up the current bubble, required pick up bubble first before putting down bubble
    /// </summary>
    /// <param name="bubble">Transform of the bubble to pick up</param>
    public void PickUpBubble(Transform bubble){
        bubble.parent = attachPoint_bubble;
        currentlyOnhand = bubble;
        currentlyOnHand_initHeight = bubble.position.y;

        currentlyOnhand_behaviour = bubble.GetComponentInChildren<EnvBubble_Behaviour>();
        if(currentlyOnhand_behaviour != null)
        {
            currentlyOnhand_behaviour.enabled = false;
        }
        StartCoroutine(MoveObject(bubble, attachPoint_bubble.position));
    }

    /// <summary>
    /// Put down the currently holding bubble
    /// </summary>
    public void PutDownBubble(){

        if(currentlyOnhand_behaviour != null)
        {
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
    static Queue<EnvBubble_Behaviour> pool = new();

    public static void ResetBubble(EnvBubble_Behaviour bubble)
    {
        pool.Enqueue(bubble);
        bubble.parent.transform.localScale = Vector3.one;
        bubble.parent.gameObject.SetActive(false);
    }

    public static GameObject GetBubble()
    {
        if(pool.Count == 0)
        {
            return GameObject.Instantiate(instance.gameObject);
        }

        return pool.Dequeue().gameObject;
    }
}
