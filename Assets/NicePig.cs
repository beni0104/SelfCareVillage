using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NicePig : MonoBehaviour
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
            player.changeDialogueState(true, "NICEPIG");
            TextBackground.SetActive(true);
            pressed_F = 1;
        }

        if (player.getDialogueNPC() == "NICEPIG" && show_dialogue == true)
        {

            if (Input.GetKeyDown(KeyCode.F))
                dialogue_number = dialogue_number + 1;

            if (conversation_number != 10)
            {
                switch (dialogue_number)
                {
                    case 1:
                        NewDialogue("[A pig]: Oink oink ... ");
                        break;
                    default:
                        conversation_number = conversation_number + 1;
                        ConversationEnded();
                        break;
                }
            } else if (conversation_number == 10)
            {
                switch (dialogue_number)
                {
                    case 1:
                        NewDialogue("[A pig]: I am just a pig, dude. Let me be");
                        break;
                    default:
                        conversation_number = conversation_number + 1;
                        manager.updateLevel(5);
                        ConversationEnded();
                        break;
                }
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
