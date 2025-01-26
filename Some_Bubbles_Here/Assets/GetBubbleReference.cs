using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetBubbleReference : MonoBehaviour
{
    public Transform RefBubble;
    // Start is called before the first frame update
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "EnvBubble")
        {
            RefBubble = other.gameObject.transform;
        }
    }
}
