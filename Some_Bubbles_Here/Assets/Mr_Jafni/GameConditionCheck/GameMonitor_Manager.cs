using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMonitor_Manager : MonoBehaviour
{
    public UIInteraction_Manager uIInteraction_Manager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (uIInteraction_Manager == null)
        {
            return;
        }
    }

    /// <summary>
    /// Start loading the lose scene
    /// </summary>
    public void TriggerLoseScene(){

    }   

    /// <summary>
    /// Start loading the win scene
    /// </summary>
    public void TriggerWinScene(){

        if (uIInteraction_Manager.ProgressBar.fillAmount >= 1)
        {
            SceneManager.LoadScene("WinScene");
        }
    }

    /// <summary>
    /// Start loading the main menu
    /// </summary>
    public void TriggerMainMenu(){

    }

    /// <summary>
    /// Start loading the main menu
    /// </summary>
    public void TriggerMainGame(){

    }
}
