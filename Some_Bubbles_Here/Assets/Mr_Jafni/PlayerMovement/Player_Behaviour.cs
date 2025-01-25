using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Behaviour : MonoBehaviour
{
    public KeyCode normalAttack_key, pickUpAndDownBubble_key, spawnBubble_key;
    public KeyCode confirm_key, cancel_key;
    public BubbleInteraction_Manager bubbleInteraction_Manager;
    public UIInteraction_Manager uIInteraction_Manager;
    public BubbleInteraction_Manager bubbleInteraction;
    public ControlPanel controlPanel;
    public Transform Cam;
    public GameObject PlayerPrefab, Attack_range, EvBubble, HoldBubblePos;
    public ParticleSystem Bubble_Attack_Effect;
    int AbilityId;
    float PlayerAngle;

    // Start is called before the first frame update
    void Start()
    {
        Attack_range.SetActive(false);
        Debug.Log("NormalAttack");
        AbilityId = 0;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerRotation();
        PlayerMovement();

        //Ability 
        TriggerNormalAttack();
        TriggerPickUpBubble();
        TriggerSpawnBubble();
        ToggleAbility(AbilityId);
        


    }

    /// <summary>
    /// Toggle the player ability to continue moving
    /// </summary>
    /// <param name="isSetActive"> Enable the player movement? </param>
    public void TogglePlayerMovement(bool isSetActive){



    }

    public void PlayerRotation()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            // Get the direction from the player to the hit point
            Vector3 direction = hitInfo.point - PlayerPrefab.transform.position;

            // Ignore the Y component to keep rotation flat
            direction.y = 0;

            // Create the target rotation
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Smoothly rotate towards the target
            PlayerPrefab.transform.rotation = Quaternion.Slerp(PlayerPrefab.transform.rotation, targetRotation, 1000 * Time.deltaTime);
        }
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
          
            PlayerPrefab.transform.Translate(MoveDir.normalized * controlPanel.movingSpeed_player * Time.deltaTime, Space.World);

        }

    

    }


    /// <summary>
    /// Ability One, Fire the bubbles as normal attack
    /// </summary>
    public void TriggerNormalAttack(){

        if (Input.GetKeyDown(normalAttack_key))
        {
            AbilityId = 0;
            uIInteraction_Manager.ResetHighlights();
            uIInteraction_Manager.Highlights[0].SetActive(true);
            Debug.Log("NormalAttack");
        }

    }

    /// <summary>
    /// Ability Two.1: Pick up the cubble
    /// </summary>
    public void TriggerPickUpBubble(){

        if (Input.GetKeyDown(pickUpAndDownBubble_key))
        {
            CancelNormalAttack();

            AbilityId = 1;
            uIInteraction_Manager.ResetHighlights();
            uIInteraction_Manager.Highlights[1].SetActive(true);
            Debug.Log("PickUpBubble");

        }

        //Call this at the last, bubble param is the bubble that the player is interacting with
        //bubbleInteraction_Manager.PickUpBubble(bubble);
    }

    /// <summary>
    /// Ability 2.2: Put down the current bubble
    /// </summary>
    public void TriggerPutDownBubble(){

       
        //Call this at the last
        //bubbleInteraction_Manager.PutDownBubble();
    }

    /// <summary>
    /// Ability 3: Spawn a bubble
    /// </summary>
    public void TriggerSpawnBubble(){

        if (Input.GetKeyDown(spawnBubble_key))
        {
            CancelNormalAttack();

            AbilityId = 2;
            uIInteraction_Manager.ResetHighlights();
            uIInteraction_Manager.Highlights[2].SetActive(true);
            Debug.Log("SpawnBubble");

        }

        //Call this at the last, position param is the position where you spawn the buble at
        //bubbleInteraction_Manager.SpawnBubble(position);
    }

    /// <summary>
    /// Toggle the current ability to the given index. 0 for normal atack, 1 for pick up and down bubble, 2 for spawn a bubble
    /// </summary>
    /// <param name="index"></param>
    public void ToggleAbility(int index){

        if (index == 0)
        {
            if (Input.GetKeyDown(confirm_key))
            {
                Bubble_Attack_Effect.Play();
                Attack_range.SetActive(true);
            }

            if (Input.GetKeyDown(cancel_key))
            {
                CancelNormalAttack();

            }
        }

        if (index == 1)
        {
            if (Input.GetKeyDown(confirm_key))
            {
                if (uIInteraction_Manager.CoolDownOverlayPickUp.activeSelf == false)
                {
                    uIInteraction_Manager.SetAbilityCoolDown(AbilityId);
                    bubbleInteraction.PickUpBubble(HoldBubblePos.transform); //<-

                }
            }

            if (Input.GetKeyDown(cancel_key))
            {
                bubbleInteraction.PutDownBubble(); //<-

            }
        }

        if (index == 2)
        {
            if (Input.GetKeyDown(confirm_key))
            {
                if (uIInteraction_Manager.CoolDownOverlaySpawn.activeSelf == false)
                {
                    uIInteraction_Manager.SetAbilityCoolDown(AbilityId);
                    bubbleInteraction.SpawnBubble(PlayerPrefab.transform.position); //<-

                }

            }

            if (Input.GetKeyDown(cancel_key))
            {
                //Add Your Code Here

            }
        }
    }

    public void CancelNormalAttack()
    {
        Bubble_Attack_Effect.Stop();
        Attack_range.SetActive(false);
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
