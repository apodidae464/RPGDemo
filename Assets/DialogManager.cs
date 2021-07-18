using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogManager : MonoBehaviour
{

    public static DialogManager instance;

    public Text dialogTex;
    public Text nameText;

    public GameObject dialogBox;
    public GameObject nameBox;

    public string[] dialogLines;
    public int currentLine;

    private bool justStarted;

    private string questToMark;
    private bool markQuestComplete;
    private bool shouldMarkQuest;
    // Start is called before the first frame update
    void Start()
    {
        instance = this; 
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogBox.activeInHierarchy)
        {
            if (Input.GetButtonUp("Fire1"))
            {
                AudioManager.instance.playSFX(4);

                if (!justStarted)
                {
                    currentLine++;
                    if (currentLine >= dialogLines.Length)
                    {
                        dialogBox.SetActive(false);
                        if (PlayerController.instance)
                            GameManager.instance.dialogActive = false;
                        if(shouldMarkQuest)
                        {
                            shouldMarkQuest = false;
                            if(markQuestComplete)
                            {
                                QuestManager.instance.markAsCompleted(questToMark);
                            }
                            else
                            {
                                QuestManager.instance.markAsInCompleted(questToMark);
                            }
                        }
                    }
                    else
                    {
                        CheckNPCName();
                        dialogTex.text = dialogLines[currentLine];
                    }
                }
                else
                {
                    justStarted = false;
                }
            }
        }
    }

    public void ShowDialog(string[] newLine, bool isPerson)
    {
        dialogLines = newLine;
        currentLine = 0;
        CheckNPCName();
        dialogTex.text = dialogLines[currentLine];
        dialogBox.SetActive(true);
        justStarted = true;

        nameBox.SetActive(isPerson);
        if(PlayerController.instance)
        {
            GameManager.instance.dialogActive = true;
        }
    }

    public void CheckNPCName()
    {
        if(!dialogLines[currentLine].StartsWith("-"))
        {
            nameText.text = dialogLines[currentLine];
            currentLine++;
        }
    }

    public void ShouldActivateQuestAtEnd(string quest, bool markComplete)
    {
        questToMark = quest;
        markQuestComplete = markComplete;
        shouldMarkQuest = true;
    }
}

