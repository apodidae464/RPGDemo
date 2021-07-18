using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFade : MonoBehaviour
{
    public static UIFade instance;
    public Image fadeScreen;

    public float fadeSpeed;
    private bool shouldFateToBlack;
    private bool shouldFateFromBlack;
    // Start is called before the first frame update
    void Start()
    {
        
        instance = this;
        
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(shouldFateToBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if(fadeScreen.color.a == 1f)
            {
                shouldFateToBlack = false;
            }
        }
        if(shouldFateFromBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if(fadeScreen.color.a == 0f)
            {
                shouldFateFromBlack = false;
            }
        }
    }
    public void FateToBlack()
    {
        shouldFateToBlack = true;
        shouldFateFromBlack = false;
    }

    public void FateFromBlack()
    {
        shouldFateToBlack = false;
        shouldFateFromBlack = true;
    }
}
