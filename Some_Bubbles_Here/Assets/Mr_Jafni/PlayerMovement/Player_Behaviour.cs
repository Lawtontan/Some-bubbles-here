using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Behaviour : MonoBehaviour
{
    public KeyCode normalAttack_key, pickUpAndDownBubble_key, spawnBubble_key;
    public KeyCode confirm_key, cancel_key;
    public BubbleInteraction_Manager bubbleInteraction_Manager;
    public ControlPanel controlPanel;
    public Transform Cam;
    public GameObject PlayerPrefab;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
    }

    /// <summary>
    /// Toggle the player ability to continue moving
    /// </summary>
    /// <param name="isSetActive"> Enable the player movement? </param>
    public void TogglePlayerMovement(bool isSetActive){



    }

    //Added By Faruq
    public void PlayerMovement()
    {
        var H = Input.GetAxisRaw("Horizontal");
        var V = Input.GetAxisRaw("Vertical");
        Vector3 Dir = new Vector3(H, 0, V).normalized;
        //anim.SetFloat("V", V, 1f, Time.deltaTime * 10f); //For Animations
        // anim.SetFloat("H", H, 1f, Time.deltaTime * 10f);

        if (Dir.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(Dir.x, Dir.z) * Mathf.Rad2Deg + Cam.eulerAngles.y;
            Vector3 MoveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            PlayerPrefab.transform.rotation = Quaternion.Slerp(PlayerPrefab.transform.rotation, Quaternion.LookRotation(MoveDir), controlPanel.rotateSpeed_player * Time.deltaTime);



            PlayerPrefab.transform.Translate(MoveDir.normalized * controlPanel.movingSpeed_player * Time.deltaTime, Space.World);

        }


    }

    /// <summary>
    /// Ability One, Fire the bubbles as normal attack
    /// </summary>
    public void TriggerNormalAttack(){

    }

    /// <summary>
    /// Ability Two.1: Pick up the cubble
    /// </summary>
    public void TriggerPickUpBubble(){
        // Your code here

        //Call this at the last, bubble param is the bubble that the player is interacting with
        //bubbleInteraction_Manager.PickUpBubble(bubble);
    }

    /// <summary>
    /// Ability 2.2: Put down the current bubble
    /// </summary>
    public void TriggerPutDownBubble(){
        //Your code here

        //Call this at the last
        //bubbleInteraction_Manager.PutDownBubble();
    }

    /// <summary>
    /// Ability 3: Spawn a bubble
    /// </summary>
    public void TriggerSpawnBubble(){
        //Your code here

        //Call this at the last, position param is the position where you spawn the buble at
        //bubbleInteraction_Manager.SpawnBubble(position);
    }

    /// <summary>
    /// Toggle the current ability to the given index. 0 for normal atack, 1 for pick up and down bubble, 2 for spawn a bubble
    /// </summary>
    /// <param name="index"></param>
    public void ToggleAbility(int index){

    }

    /// <summary>
    /// Code to run when the player clicked on the confirm button (Left-click)
    /// </summary>
    public void OnConfirmButtonEnter(){

    }

    /// <summary>
    /// Code to run when the player clicked on the cancel button (right-click)
    /// </summary>
    public void OnCancelButtonEnter(){

    }
}
