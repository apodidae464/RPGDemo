using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEntrance : MonoBehaviour
{
    public string transitionName;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerController.instance != null && transitionName == PlayerController.instance.areaTransitionName)
        {
            PlayerController.instance.transform.position = transform.position;
        }
        if(UIFade.instance != null)
        {
             UIFade.instance.FateFromBlack();
            if(GameManager.instance != null)
            {
                GameManager.instance.fadingBetweenArea = false;

            }

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
