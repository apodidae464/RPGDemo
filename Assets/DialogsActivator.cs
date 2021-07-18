using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogsActivator : MonoBehaviour
{
    public string[] lines;

    public bool isPerson = true;
    private bool canActive;
    public static bool cActive = true;

    public bool shouldActivateQuest;
    public bool markComplete;
    public string questToMask;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(cActive && canActive && Input.GetButtonDown("Fire1")
            && !DialogManager.instance.dialogBox.activeInHierarchy)
        {
            DialogManager.instance.ShowDialog(lines, isPerson);
            DialogManager.instance.ShouldActivateQuestAtEnd(questToMask, markComplete);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            canActive = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canActive = false;
        }
    }

}
