using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class UIInteraction_Manager : MonoBehaviour
{
    public GameObject CoolDownOverlayPickUp, CoolDownOverlaySpawn;
    public GameObject[] Highlights;
    public ControlPanel controlPanel;
    public Image ProgressBar;
    public Weather_Manager weather_Manager;
    Image CooldownPickUp, CooldownSpawn;
    public TextMeshProUGUI cooldownPickUpSec, cooldownSpawnSec;
    public float progressSpeed;
    bool[] checkPoint;

    

    // Start is called before the first frame update
    void Start()
    {
        CooldownPickUp = CoolDownOverlayPickUp.GetComponent<Image>();
        CooldownSpawn = CoolDownOverlaySpawn.GetComponent<Image>();
        CooldownPickUp.fillAmount = 0;
        CooldownSpawn.fillAmount = 0;
        ProgressBar.fillAmount = 0;
        checkPoint = new bool[3];

    }

    // Update is called once per frame
    void Update()
    {
        ReplenishAbility();
        ProgressBarActive();
     
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

    public void ResetHighlights ()
    {
        Highlights[0].SetActive(false);
        Highlights[1].SetActive(false);
        Highlights[2].SetActive(false);
    }

    public void ProgressBarActive()
    {
        if (ProgressBar.fillAmount < 1)
        {
            ProgressBar.fillAmount += progressSpeed * Time.deltaTime;
        }

        if (ProgressBar.fillAmount >= 0.2f && ProgressBar.fillAmount < 0.5f && checkPoint[0] == false)
        {
            Debug.Log("Weather 1");
            weather_Manager.GoToNextSeason();
            checkPoint[0] = true;

        }
        else if (ProgressBar.fillAmount >= 0.5f && ProgressBar.fillAmount < 0.7f && checkPoint[1] == false)
        {
            Debug.Log("Weather 2");
            weather_Manager.GoToNextSeason();
            checkPoint[1] = true;
        }
        else if (ProgressBar.fillAmount >= 0.7f && checkPoint[2] == false)
        {
            Debug.Log("Weather 3");
            weather_Manager.GoToNextSeason();
            checkPoint[2] = true;
        }
        else if (ProgressBar.fillAmount >= 1f)
        {
            SceneManager.LoadSceneAsync("WinScene");

        }

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
