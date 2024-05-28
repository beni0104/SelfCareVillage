using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NPCLlama : MonoBehaviour
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
            player.changeDialogueState(true, "LAMA");
            TextBackground.SetActive(true);
            pressed_F = 1;
        }

        if (player.getDialogueNPC() == "LAMA" && show_dialogue == true)
        {

            if (Input.GetKeyDown(KeyCode.F))
                dialogue_number = dialogue_number + 1;

            switch (conversation_number)
            {
                case 0:
                    switch (dialogue_number)
                    {
                        case 1:
                            NewDialogue("[A random llama]: These mushrooms are so good...");
                            break;
                        case 2:
                            NewDialogue("[A random llama]: Nom nom nom ...");
                            break;
                        default:
                            conversation_number = conversation_number + 1;
                            manager.updateLevel(10);
                            ConversationEnded();
                            break;
                    }
                    break;
                case 1:
                    switch (dialogue_number)
                    {
                        case 1:
                            NewDialogue("[A random llama]: Don't walk the grass...");
                            break;
                        case 2:
                            NewDialogue("[A random llama]: Nom nom nom ...");
                            break;
                        default:
                            conversation_number = conversation_number + 1;
                            ConversationEnded();
                            break;
                    }
                    break;
                case 2:
                    switch (dialogue_number)
                    {
                        case 1:
                            NewDialogue("[A random llama]: Wow! The sky is pink...");
                            break;
                        case 2:
                            NewDialogue("[A random llama]: Nom nom nom ...");
                            break;
                        default:
                            conversation_number = conversation_number + 1;
                            ConversationEnded();
                            break;
                    }
                    break;
                default:
                    switch (dialogue_number)
                    {
                        case 1:
                            NewDialogue("[A random llama]: Nom nom nom ...");
                            break;
                        default:
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
