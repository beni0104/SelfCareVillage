using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Pig_Wisdom : MonoBehaviour
{
    private bool player_detection = false;
    private int set_player_detection = 0;

    private bool interactive_text_display = false;
    private int set_interactive_text_display = 0;

    private int pressed_F = 0;
    private bool show_dialogue = false;

    private HappinessManager manager;
    private FirstPersonController player;

    // a conversation can have multiple dialogues
    private static int dialogue_number = 0;
    // a character can have multiple conversations
    private static int conversation_number = 0;


    [SerializeField] private TextMeshProUGUI NPCText;
    [SerializeField] private TextMeshProUGUI ContinueText;

    [SerializeField] private GameObject TextBackground;


    void Start()
    {
        manager = FindObjectOfType<HappinessManager>();
        player = FindObjectOfType<FirstPersonController>();
    }

    private void ConversationEnded()
    {
        //player_detection = false;
        set_player_detection = 0;

        interactive_text_display = false;
        set_interactive_text_display = 0;

        manager.resetInteractiveText();

        show_dialogue = false;
        player.changeDialogueState(false, "NONE");
        pressed_F = 0;

        TextBackground.SetActive(false);
        NPCText.SetText("");

        ContinueText.SetText("");

        dialogue_number = 0;
    }

    void NewDialogue(string text)
    {
        NPCText.SetText(text);
    }

    void Update()
    {
        if (player_detection == true && set_player_detection == 0)
        {
            interactive_text_display = true;
            set_player_detection = 1;
        }

        if (interactive_text_display == true && set_interactive_text_display == 0)
        {
            manager.updateInteractiveText();
            set_interactive_text_display = 1;
        }

        if (player_detection == true && Input.GetKeyDown(KeyCode.F) && pressed_F == 0)
        {
            manager.resetInteractiveText();
            show_dialogue = true;
            ContinueText.SetText("Press F to continue");
            player.changeDialogueState(true, "W_PIG");
            TextBackground.SetActive(true);
            pressed_F = 1;
        }

        if (player.getDialogueNPC() == "W_PIG" && show_dialogue == true)
        {

            if (Input.GetKeyDown(KeyCode.F))
                dialogue_number = dialogue_number + 1;

            switch (conversation_number)
            {
                case 0:
                    switch (dialogue_number)
                    {
                        case 1:
                            NewDialogue("[The Wisdom Pig]: Oink oink ... Patience is a virtue, my friend.");
                            break;
                        default:
                            conversation_number = conversation_number + 1;
                            manager.updateLevel(5);
                            ConversationEnded();
                            break;
                    }
                    break;
                case 1:
                    switch (dialogue_number)
                    {
                        case 1:
                            NewDialogue("[The Wisdom Pig]: Oink oink ... We pigs stick together, and that's how we thrive. ");
                            break;
                        default:
                            conversation_number = conversation_number + 1;
                            manager.updateLevel(5);
                            ConversationEnded();
                            break;
                    }
                    break;
                case 2:
                    switch (dialogue_number)
                    {
                        case 1:
                            NewDialogue("[The Wisdom Pig]: Remember, no one is an island. Helping others and working as a team makes us all stronger. Oink!");
                            break;
                        default:
                            conversation_number = conversation_number + 1;
                            manager.updateLevel(5);
                            ConversationEnded();
                            break;
                    }
                    break;
                case 3:
                    switch (dialogue_number)
                    {
                        case 1:
                            NewDialogue("[The Wisdom Pig]: Oink! Life is full of simple pleasures – a cool mud bath on a hot day, a juicy apple, the warmth of the sun.");
                            break;
                        case 2:
                            NewDialogue("Don’t overlook these little joys. They make life sweet. Oink oink!");
                            break;
                        default:
                            conversation_number = conversation_number + 1;
                            manager.updateLevel(5);
                            ConversationEnded();
                            break;
                    }
                    break;
                case 4:
                    switch (dialogue_number)
                    {
                        case 1:
                            NewDialogue("[The Wisdom Pig]: Oink oink! Be like the pig – adaptable and resourceful. When challenges come your way, find clever solutions. ");
                            break;
                        default:
                            conversation_number = conversation_number + 1;
                            manager.updateLevel(5);
                            ConversationEnded();
                            break;
                    }
                    break;
                default:
                    switch (dialogue_number)
                    {
                        case 1:
                            NewDialogue("(The pig is happy you have listened to him, but has no more advices to give.)");
                            break;
                        default:
                            conversation_number = conversation_number + 1;
                            ConversationEnded();
                            break;
                    }
                    break;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "CharacterController")
        {
            player_detection = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        player_detection = false;
        set_player_detection = 0;

        interactive_text_display = false;
        set_interactive_text_display = 0;

        manager.resetInteractiveText();

        show_dialogue = false;
        player.changeDialogueState(false, "NONE");
        pressed_F = 0;

        TextBackground.SetActive(false);
        NPCText.SetText("");

        ContinueText.SetText("");
        dialogue_number = 0;

    }
}
