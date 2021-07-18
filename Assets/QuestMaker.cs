using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestMaker : MonoBehaviour
{
    public string questToMark;
    public bool markComplete;

    public bool markOnEnter;
    private bool canMark;

    public bool deactiveOnMarking;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(canMark && Input.GetButtonDown("Fire1"))
        {
            canMark = false;
            markQuest();
        }
    }
    public void markQuest()
    {
        if (!QuestManager.instance)
            return;
        if(markComplete)
        {
            QuestManager.instance.markAsCompleted(questToMark);
        }
        else
        {
            QuestManager.instance.markAsInCompleted(questToMark);
        }

        gameObject.SetActive(!deactiveOnMarking);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {


        if(collision.tag == "Player")
        {
            if (markOnEnter)
            {
                markQuest();
            }
            else
            {
               canMark = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canMark = false;
        }
    }

}
