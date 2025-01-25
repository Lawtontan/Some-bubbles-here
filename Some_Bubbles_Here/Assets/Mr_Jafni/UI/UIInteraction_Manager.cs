using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIInteraction_Manager : MonoBehaviour
{
    public GameObject CoolDownOverlayPickUp, CoolDownOverlaySpawn;
    public ControlPanel controlPanel;
    Image CooldownPickUp, CooldownSpawn;
    public TextMeshProUGUI cooldownPickUpSec, cooldownSpawnSec;
   
    // Start is called before the first frame update
    void Start()
    {
        CooldownPickUp = CoolDownOverlayPickUp.GetComponent<Image>();
        CooldownSpawn = CoolDownOverlaySpawn.GetComponent<Image>();
        CooldownPickUp.fillAmount = 0;
        CooldownSpawn.fillAmount = 0;

    }

    // Update is called once per frame
    void Update()
    {
        ReplenishAbility();

        
    }

    /// <summary>
    /// Set the ability with index to cool down
    /// </summary>
    /// <param name="index">0 == Normal attack, 1 == pick up and down bubble, 2 == spawn bubble</param>
    public void SetAbilityCoolDown(int index){

        if (index == 1)
        {
            CoolDownOverlayPickUp.SetActive(true);
            CooldownPickUp.fillAmount = 1;
        }

        if (index == 2)
        {
            CoolDownOverlaySpawn.SetActive(true);
            CooldownSpawn.fillAmount = 1;
        }
    }

    /// <summary>
    /// Set the ability with the given index to the active one (e.g. Apply the boarder effect, to visually indicates the current active ability)
    /// </summary>
    /// <param name="index"></param>
    public void SetActiveAbility_UI(int index){

    }

    public void ReplenishAbility()
    {
        if (CooldownPickUp.fillAmount > 0)
        {
            CooldownPickUp.fillAmount -= controlPanel.pickUpAndDownBubble_coolDown_player/5f * Time.deltaTime;
            cooldownPickUpSec.text = CooldownPickUp.fillAmount.ToString("0.0");
        }
        else
        {
            CoolDownOverlayPickUp.SetActive(false);


        }


        if (CooldownSpawn.fillAmount > 0)
        {
            CooldownSpawn.fillAmount -= controlPanel.pickUpAndDownBubble_coolDown_player / 5f * Time.deltaTime;
            cooldownSpawnSec.text = CooldownSpawn.fillAmount.ToString("0.0");
        }
        else
        {
            CoolDownOverlaySpawn.SetActive(false);

        }
    }
}
