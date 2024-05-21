using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartGame : MonoBehaviour
{
    private HappinessManager manager;
    private FirstPersonController player;

    [SerializeField] private TextMeshProUGUI NPCText;
    [SerializeField] private GameObject TextBackground;
    [SerializeField] private TextMeshProUGUI ContinueText;

    bool finished_intro = false;
    bool displayed_once = false;
    int text_number = 1;

    public void Start()
    {
        player = FindObjectOfType<FirstPersonController>();
    }

    void NewDialogue(string text)
    {
        NPCText.SetText(text);
    }

    private void ConversationEnded()
    {
        player.changeDialogueState(false, "NONE");

        TextBackground.SetActive(false);
        NPCText.SetText("");
        ContinueText.SetText("");

        finished_intro = true;
    }

    public void Update()
    {
        if (finished_intro == false)
        {
            if (displayed_once == false)
            {
                player.changeDialogueState(true, "NONE");

                ContinueText.SetText("Press F to continue");
                TextBackground.SetActive(true);

                displayed_once = true;
            }

            if (Input.GetKeyDown(KeyCode.F))
                text_number = text_number + 1;

            switch (text_number)
            {
                case 1:
                    NewDialogue("Welcome to SelfCare village - a small and quiet town surrounded by grassy fields and beautiful scenery.");
                    break;
                case 2:
                    NewDialogue("Whether you’re here because of stress, anxiety or fatigue, this place will help you take a break from your worries.");
                    break;
                case 3:
                    NewDialogue("Before you start exploring there are a few things you should keep in mind...");
                    break;
                case 4:
                    NewDialogue("1. Use W-A-S-D to move around. \n2. F to interact with people and objects. \n3. P to pause the game");
                    break;
                case 5:
                    NewDialogue("And most important: remember to alwayas be positive and have fun!");
                    break;
                default:
                    ConversationEnded();
                    break;
            }
        }
    }
}
