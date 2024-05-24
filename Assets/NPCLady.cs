using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;
using System.Runtime.ConstrainedExecution;
using System.Security.Claims;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using Unity.VisualScripting;
using UnityEngine.SocialPlatforms;
using UnityEngine.UIElements.Experimental;
using System;
using UnityEngine.XR;

public class NPCLady : MonoBehaviour
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

    private static bool onQuest = false;
    private static int set_quest = 0;

    private int lady_dialogue = 0;
    private bool options_display = false;
    private int set_options_display = 0;

    [SerializeField] private TextMeshProUGUI NPCText;
    [SerializeField] private TextMeshProUGUI ContinueText;

    [SerializeField] private GameObject TextBackground;

    [SerializeField] private Button Option1 = null;
    [SerializeField] private Button Option2 = null;
    [SerializeField] private Button Option3 = null;
    private int buttonClicked = 0;

    private int gave_cupcakes = 0;
    private bool gave_reward = false;

    void Start()
    {
        manager = FindObjectOfType<HappinessManager>();
        player = FindObjectOfType<FirstPersonController>();

        // adding a delegate with parameters
        Option1.onClick.AddListener(delegate { Option1_OnClick(); });
        Option2.onClick.AddListener(delegate { Option2_OnClick(); });
        Option3.onClick.AddListener(delegate { Option3_OnClick(); });
    }

    private void Option1_OnClick()
    {
        buttonClicked = 1;
    }

    private void Option2_OnClick()
    {
        buttonClicked = 2;
    }

    private void Option3_OnClick()
    {
        buttonClicked = 3;
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
            player.changeDialogueState(true, "LADY");
            TextBackground.SetActive(true);
            pressed_F = 1;
        }

        if (player.getDialogueNPC() == "LADY")
        {

            if (onQuest == true)
            {

                if (options_display == true)
                {
                    if (buttonClicked == 1)
                    {
                        lady_dialogue = lady_dialogue + 1;
                        options_display = false;
                    }
                    else if (buttonClicked == 2)
                    {
                        lady_dialogue = lady_dialogue + 2;
                        options_display = false;
                    }
                    else if (buttonClicked == 3)
                    {
                        lady_dialogue = lady_dialogue + 2;
                        options_display = false;
                    }
                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.F))
                        if (gave_reward == false)
                            lady_dialogue = lady_dialogue + 1;
                        else
                        {
                            lady_dialogue = lady_dialogue + 5;
                            onQuest = false;
                            set_quest = 0;
                        }

                    switch (lady_dialogue)
                    {
                        case 1:
                            Option1.gameObject.SetActive(true);
                            Option2.gameObject.SetActive(true);
                            options_display = true;

                            Cursor.lockState = CursorLockMode.None;
                            Cursor.visible = true;
                            break;
                        case 2:
                            // when first button = YES is pressed
                            Option1.gameObject.SetActive(false);
                            Option2.gameObject.SetActive(false);
                            Cursor.lockState = CursorLockMode.Locked;
                            Cursor.visible = false;

                            if (gave_reward == false)
                            {
                                manager.updateLevel(10);
                                dialogue_number += 2;
                                gave_reward = true;
                                onQuest = false;
                                set_quest = 0;
                            }
                            break;
                        case 3:
                            // when second button = NO is pressed
                            Option1.gameObject.SetActive(false);
                            Option2.gameObject.SetActive(false);
                            Cursor.lockState = CursorLockMode.Locked;
                            Cursor.visible = false;

                            if (gave_reward == false)
                            {
                                dialogue_number += 1;
                                gave_reward = true;
                                onQuest = false;
                                set_quest = 0;
                            }
                            break;
                        default:
                            //conversation_number = conversation_number + 1;
                            onQuest = false;
                            gave_reward = false;
                            //ConversationEnded();
                            break;
                    }
                }

            }
            else if (onQuest == false || set_quest == 0)
            {
                //print(player.getDialogueNPC());
                if (show_dialogue == true)
                {
                    if (Input.GetKeyDown(KeyCode.F))
                        dialogue_number = dialogue_number + 1;

                    switch (conversation_number)
                    {
                        case 0:
                            switch (dialogue_number)
                            {
                                case 1:
                                    NewDialogue("[Janie]: Hello! My name is Janet, but everyone calls me Janie.");
                                    break;
                                case 2:
                                    NewDialogue("[Janie]: I've heard we would have a new guest around, but I never expected you to be such an interesting character!");
                                    break;
                                case 3:
                                    NewDialogue("[Janie]: My-my-my, I am already blabbering. Please don't mind me!");
                                    break;
                                default:
                                    conversation_number = conversation_number + 1;
                                    ConversationEnded();
                                    break;
                            }
                            break;
                        case 1:
                            switch (dialogue_number)
                            {
                                case 1:
                                    NewDialogue("[Janie]: I know a lot of stories, do you want to hear one?");
                                    onQuest = true;
                                    lady_dialogue = 1;
                                    buttonClicked = 0;
                                    gave_reward = false;
                                    break;
                                case 2:
                                    NewDialogue("[Janie]: That's such a pity...");
                                    dialogue_number = 1;
                                    ConversationEnded();
                                    break;
                                case 3:
                                    NewDialogue("[Janie]: Did you know our village has a bit of a legend?");
                                    break;
                                case 4:
                                    NewDialogue("[Janie]: They say that if you visit the old oak tree at the edge of the forest during a full moon, you might catch a glimpse of the forest spirits.");
                                    break;
                                case 5:
                                    NewDialogue("[Janie]: It’s probably just a tale, but it’s fun to think about, isn’t it?");
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
                                    NewDialogue("[Janie]: Want to hear another story?");
                                    onQuest = true;
                                    lady_dialogue = 1;
                                    buttonClicked = 0;
                                    gave_reward = false;
                                    break;
                                case 2:
                                    NewDialogue("[Janie]: That's such a pity...");
                                    dialogue_number = 0;
                                    ConversationEnded();
                                    break;
                                case 3:
                                    NewDialogue("[Janie]: Long ago, there was an old inn, run by a kind old woman named Agatha.");
                                    break;
                                case 4:
                                    NewDialogue("[Janie]: She took care of travelers and locals alike, and her inn was known for its warmth and hospitality.");
                                    break;
                                case 5:
                                    NewDialogue("[Janie]: After she passed away, people started saying they could still feel her presence.");
                                    break;
                                case 6:
                                    NewDialogue("[Janie]: Some even claim to have seen her ghost, especially on cold, stormy nights.");
                                    break;
                                case 7:
                                    NewDialogue("[Janie]: But don’t worry, Agatha’s ghost is friendly .. or at least so are the rumors.");
                                    break;
                                default:
                                    conversation_number = conversation_number + 1;
                                    ConversationEnded();
                                    break;
                            }
                            break;
                        case 3:
                            switch (dialogue_number)
                            {
                                case 1:
                                    NewDialogue("[Janie]: Oh, do you want to hear about the Whispering Well?");
                                    onQuest = true;
                                    lady_dialogue = 1;
                                    buttonClicked = 0;
                                    gave_reward = false;
                                    break;
                                case 2:
                                    NewDialogue("[Janie]: That's such a pity...");
                                    dialogue_number = 0;
                                    ConversationEnded();
                                    break;
                                case 3:
                                    NewDialogue("[Janie]: So there is this old well and people say that lots of spirits live inside it.");
                                    break;
                                case 4:
                                    NewDialogue("[Janie]: If you whisper your deepest wish into the well at midnight, those spirits will grant it.");
                                    break;
                                case 5:
                                    NewDialogue("[Janie]: It's probably just a fun legend tho...");
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
                                    NewDialogue("[Janie]: ...");
                                    break;
                                case 2:
                                    NewDialogue("[Janie]: .....");
                                    break;
                                case 3:
                                    NewDialogue("(Janie finally has nothing to say.)");
                                    break;
                                default:
                                    ConversationEnded();
                                    break;
                            }
                            break;
                    }
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
