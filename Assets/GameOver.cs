using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public string mainMenuScene;
    public string loadGameScene;

    public GameObject continueButton;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("Current_Scene_"))
        {
            continueButton.SetActive(true);
        }
        else
        {
            continueButton.SetActive(false);
        }
        if (PlayerController.instance)
            PlayerController.instance.gameObject.SetActive(false);
        if(GameMenus.instance)
             GameMenus.instance.gameObject.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void goToMainMenu()
    {
        if(GameManager.instance)
             Destroy(GameManager.instance.gameObject);
        if(PlayerController.instance)
            Destroy(PlayerController.instance.gameObject);
        if(GameMenus.instance)
            Destroy(GameMenus.instance.gameObject);
        if(AudioManager.instance)
            Destroy(AudioManager.instance.gameObject);
        SceneManager.LoadScene(mainMenuScene);
    }

    public void LoadLastScene()
    {
        if (GameManager.instance)
            Destroy(GameManager.instance.gameObject);
        if (PlayerController.instance)
            Destroy(PlayerController.instance.gameObject);
        if (GameMenus.instance)
            Destroy(GameMenus.instance.gameObject);
        if (AudioManager.instance)
            Destroy(AudioManager.instance.gameObject);
        SceneManager.LoadScene(loadGameScene);
    }
}
