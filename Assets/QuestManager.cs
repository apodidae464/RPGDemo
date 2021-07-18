using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;

    public string[] questMakerNames;
    public bool[] questMakersComplete;



    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        questMakersComplete = new bool[questMakerNames.Length];
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            saveQuestData();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            loadQuestData();
        }
    }

    public int getQuestNumber(string quest)
    {
        for(int i = 0; i < questMakerNames.Length; i++)
        {
            if(questMakerNames[i] == quest)
            {
                return i;
            }
        }

        Debug.LogError("There is no quest - " + quest);
        return 0;
    }

    public bool checkIfCompleted(string quest)
    {
        if(getQuestNumber(quest) != 0)
        {
            return questMakersComplete[getQuestNumber(quest)];
        }
        return false;
    }
    public void markAsCompleted(string quest)
    {
        questMakersComplete[getQuestNumber(quest)] = true;
        updateLocalQuestObj();

    }

    public void markAsInCompleted(string quest)
    {
        questMakersComplete[getQuestNumber(quest)] = false;
        updateLocalQuestObj();
    }

    public void updateLocalQuestObj()
    {
        QuestObjectActivator[] questObjects = FindObjectsOfType<QuestObjectActivator>();
        if(questObjects.Length > 0)
        {
            for(int i = 0; i < questObjects.Length; i++)
            {
                questObjects[i].checkCompletion();
            }
        }
    }

    public void saveQuestData()
    {
        for(int i = 0; i < questMakerNames.Length; i++)
        {
            if (questMakersComplete[i])
            {
                PlayerPrefs.SetInt("QuestMaker_" + questMakerNames[i], 1);
            } else
            {
                PlayerPrefs.SetInt("QuestMaker_" + questMakerNames[i], 0);
            }

        }
    }

    public void loadQuestData()
    {
        for(int i = 0; i < questMakerNames.Length; i++)
        {
            int valueToSet = 0;
            if(PlayerPrefs.HasKey("QuestMaker_" + questMakerNames[i]))
            {
                valueToSet = PlayerPrefs.GetInt("QuestMaker_" + questMakerNames[i]); 
            }
            if(valueToSet == 0)
            {
                questMakersComplete[i] = false;
            }
            else
            {
                questMakersComplete[i] = true;
            }
        }

        
    }
}
