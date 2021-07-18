using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string newGameScene;

    public GameObject continueButton;

    public string loadGameScene;

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("Current_Scene_"))
        {
            continueButton.SetActive(true);
        } else
        {
            continueButton.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Continue()
    {
        SceneManager.LoadScene(loadGameScene);
        AudioManager.instance.playSFX(4);
    }
    
    public void NewGame()
    {
        SceneManager.LoadScene(newGameScene);
        AudioManager.instance.playSFX(4);

    }

    public void Exit()
    {
        Application.Quit();
        AudioManager.instance.playSFX(4);

    }
}
