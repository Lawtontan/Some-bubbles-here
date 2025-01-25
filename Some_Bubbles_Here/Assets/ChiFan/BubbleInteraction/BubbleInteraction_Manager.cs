using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleInteraction_Manager : MonoBehaviour
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
    /// Pick up the current bubble, required pick up bubble first before putting down bubble
    /// </summary>
    /// <param name="bubble">Transform of the bubble to pick up</param>
    public void PickUpBubble(Transform bubble){

    }

    /// <summary>
    /// Put down the currently holding bubble
    /// </summary>
    public void PutDownBubble(){

    }

    /// <summary>
    /// Spawn a bubble object at the given position
    /// </summary>
    /// <param name="position">Vector3 position to spawn the bubble object</param>
    public void SpawnBubble(Vector3 position){

    }
}

public static class BubblePool
{
    static Queue<EnvBubble_Behaviour> pool = new();

    public static void ResetBubble(EnvBubble_Behaviour bubble)
    {
        pool.Enqueue(bubble);
        bubble.parent.transform.localScale = Vector3.one;
        bubble.parent.gameObject.SetActive(false);
    }

    public static GameObject GetBubble()
    {
        return pool.Dequeue().gameObject;
    }
}
