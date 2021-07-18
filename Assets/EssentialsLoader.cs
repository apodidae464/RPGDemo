using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialsLoader : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject UIScreen;
    public GameObject player;
    public GameObject gameMgr;
    void Start()
    {
        if(PlayerController.instance == null)
        {
            PlayerController clone = Instantiate(player).GetComponent<PlayerController>();
            PlayerController.instance = clone;
        }
        if(UIFade.instance == null)
        {
            UIFade clone = Instantiate(UIScreen).GetComponent<UIFade>();
            UIFade.instance = clone;
        }
        if(GameManager.instance == null)
        {
            Instantiate(gameMgr);
        }    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
