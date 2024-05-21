using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NPCCat : MonoBehaviour
{
    bool player_detection = false;
    int set_player_detection = 0;

    bool interactive_text_display = false;
    int set_interactive_text_display = 0;

    int pressed_F = 0;
    bool show_dialogue = false;

    private HappinessManager manager;
    private FirstPersonController player;

    // a conversation can have multiple dialogues
    static int dialogue_number = 0;
    // a character can have multiple conversations
    static int conversation_number = 0;

    static bool onQuest = false;
    static int set_quest = 0;

    int cupcake_dialogue = 0;
    bool options_display = false;
    int set_options_display = 0;

    [SerializeField] private TextMeshProUGUI NPCText;
    [SerializeField] private TextMeshProUGUI ContinueText;

    [SerializeField] private GameObject TextBackground;
    
    [SerializeField] private Button Option1 = null;
    [SerializeField] private Button Option2 = null;
    int buttonClicked = 0;

    int gave_cupcakes = 0;
    bool gave_reward = false;

    void Start()
    {
        manager = FindObjectOfType<HappinessManager>();
        player = FindObjectOfType<FirstPersonController>();

        // adding a delegate with parameters
        Option1.onClick.AddListener(delegate { Option1_OnClick(); });
        Option2.onClick.AddListener(delegate { Option2_OnClick(); });
    }

    private void Option1_OnClick()
    {
        buttonClicked = 1;
    }

    private void Option2_OnClick()
    {
        buttonClicked = 2;
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
            player.changeDialogueState(true, "CAT");
            TextBackground.SetActive(true);
            pressed_F = 1;
        }

        if (manager.getCupcakes() != 0 && player.getDialogueNPC() == "CAT" && conversation_number > 0)
        {
            print(1);
            //print(manager.getCupcakes());
            //print(player.getDialogueNPC());
            //print(conversation_number);

            if (options_display == true)
            {
                if (buttonClicked == 1)
                {
                    cupcake_dialogue = cupcake_dialogue + 1;
                    options_display = false;
                    print("first button");
                }
                else if (buttonClicked == 2)
                {
                    cupcake_dialogue = cupcake_dialogue + 2;
                    options_display = false;
                    print("second button");
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.F))
                    if (gave_cupcakes == 0)
                        cupcake_dialogue = cupcake_dialogue + 1;
                    else
                    {
                        cupcake_dialogue = cupcake_dialogue + 3;
                        manager.giveCupcakes();
                        onQuest = false;
                        set_quest = 0;
                    }

                switch (cupcake_dialogue)
                {
                    case 1:
                        NewDialogue("[Robin]: Oh, my! Some beautiful cupcakes you have there.");
                        break;
                    case 2:
                        NewDialogue("[Robin]: Would you mind sharing some with me?");
                        Option1.gameObject.SetActive(true);
                        Option2.gameObject.SetActive(true);
                        options_display = true;

                        Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true;
                        break;
                    case 3:
                        // when first button is pressed
                        Option1.gameObject.SetActive(false);
                        Option2.gameObject.SetActive(false);
                        Cursor.lockState = CursorLockMode.Locked;
                        Cursor.visible = false;

                        NewDialogue("[Robin]: Are you sure about that? Well... I won't ask you twice! You are such a cutie-pie!!!");
                        if (gave_reward == false)
                        {
                            manager.updateLevel(25);
                            gave_cupcakes = 2;
                            gave_reward = true;
                        }
                        break;
                    case 4:
                        // when second button is pressed
                        Option1.gameObject.SetActive(false);
                        Option2.gameObject.SetActive(false);
                        Cursor.lockState = CursorLockMode.Locked;
                        Cursor.visible = false;

                        NewDialogue("[Robin]: Thank you so much! Your good deed will not go unnoticed.");
                        //cupcake_dialogue += 2;
                        if (gave_reward == false)
                        {
                            manager.updateLevel(20);
                            gave_cupcakes = 2;
                            gave_reward = true;
                        }
                        break;
                    default:
                        conversation_number = conversation_number + 1;
                        onQuest = false;
                        gave_reward = false;
                        ConversationEnded();
                        break;
                }
            }
            
        }
        else if (onQuest == true && player.getDialogueNPC() == "CAT" && manager.getCupcakes() == 0)
        {
            // when the player is on a quest
            //if (player.getDialogueState() == true)
            NewDialogue("New objective: find more cupcakes for Robin!");
            set_quest = 1;
            onQuest = false;
        }
        else if (set_quest == 1)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                ConversationEnded();
                onQuest = true;
            }
        }
        else if(onQuest == false || set_quest == 0)
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
                                NewDialogue("[Robin]: Hi there! I’m Robin, the town mayor.");
                                break;
                            case 2:
                                NewDialogue("[Robin]: We don’t really get that many visitors, so it’s nice to see a new face around.");
                                break;
                            case 3:
                                NewDialogue("[Robin]: You’ll find that the people who live here can be pretty quirky, but I’m sure you’ll get along with everyone.");
                                break;
                            case 4:
                                NewDialogue("[Robin]: Hope you have a good time!");

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
                                NewDialogue("[Robin]: Did you know I really like cupcakes?");
                                break;
                            case 2:
                                NewDialogue("[Robin]: They are so yummy I could eat them all day!");
                                break;
                            case 3:
                                NewDialogue("New objective: find more cupcakes for Robin!");
                                onQuest = true;
                                //conversation_number = conversation_number + 1;
                                //dialogue_number = 0;
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
                                NewDialogue("[Robin]: ...");
                                break;
                            case 2:
                                NewDialogue("[Robin]: .....");
                                break;
                            case 3:
                                NewDialogue("(Robin has nothing to say.)");
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
