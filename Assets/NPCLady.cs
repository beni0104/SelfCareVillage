using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCLady : MonoBehaviour
{
    bool player_detection = false;
    int set_player_detection = 0;

    bool interactive_text_display = false;
    int set_interactive_text_display = 0;

    int pressed_F = 0;
    bool show_dialogue = false;

    private HappinessManager manager;
    private FirstPersonController player;

    static int dialogue_number = 0;

    [SerializeField] private TextMeshProUGUI NPCText;
    [SerializeField] private GameObject TextBackground;
    [SerializeField] private TextMeshProUGUI ContinueText;

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
        player.changeDialogueState(false, "LADY");
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
            player.changeDialogueState(true, "LADY");
            TextBackground.SetActive(true);
            pressed_F = 1;
        }

        if(show_dialogue == true)
        {
            if (Input.GetKeyDown(KeyCode.F))
                dialogue_number = dialogue_number + 1;

            switch (dialogue_number)
            {
                case 1:
                    NewDialogue("[Janie]: Hello!");
                    break;
                case 2:
                    NewDialogue("[Janie]: How are you feeling today?");
                    break;
                case 3:
                    NewDialogue("[Janie]: Lovely seeing you! Bye!");
                    break;
                default:
                    ConversationEnded();
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
        player.changeDialogueState(false, "LADY");
        pressed_F = 0;

        TextBackground.SetActive(false);
        NPCText.SetText("");

        ContinueText.SetText("");
        dialogue_number = 0;

    }
}
