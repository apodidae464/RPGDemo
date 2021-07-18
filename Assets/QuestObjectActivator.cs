using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObjectActivator : MonoBehaviour
{
    public GameObject objectToActivate;
    public string questToCheck;

    public bool activeIfComplete;

    private bool initialCheckDone;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!initialCheckDone)
        {
            initialCheckDone = true;
            checkCompletion();
        }
    }

    public void checkCompletion()
    {
        if (!QuestManager.instance)
            return;
        if(QuestManager.instance.checkIfCompleted(questToCheck))
        {
            objectToActivate.SetActive(activeIfComplete);
        }
    }
}
