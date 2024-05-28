using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cupcake : MonoBehaviour
{
    bool player_detection = false;
    int set_player_detection = 0;

    bool interactive_text_display = false;
    int set_interactive_text_display = 0;

    int pressed_F = 0;

    private HappinessManager manager;

    [SerializeField] private GameObject one_Cupcake;

    void Start()
    {
        manager = FindObjectOfType<HappinessManager>();
    }

    private void ConversationEnded()
    {
        set_player_detection = 0;

        interactive_text_display = false;
        set_interactive_text_display = 0;

        manager.resetInteractiveText();
        pressed_F = 0;
    }

    void Update()
    {
        if (player_detection == true && set_player_detection == 0)
        {
            interactive_text_display = true;
            set_player_detection = 1;
        }
        else if (interactive_text_display == true && set_interactive_text_display == 0)
        {
            manager.changeInteractiveText("Press F to pickup.");
            set_interactive_text_display = 1;
        }
        else if (player_detection == true && Input.GetKeyDown(KeyCode.F) && pressed_F == 0)
        {
            //manager.changeInteractiveText("You have picked a cupcake. \nPress F to continue...");
            manager.pickCupcake();
            pressed_F = 1;

            one_Cupcake.SetActive(false);
            ConversationEnded();
        }

        /*
        if (pressed_F == 1 && Input.GetKeyDown(KeyCode.F))
        {
            ConversationEnded();
        }
        */
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
        pressed_F = 0;
    }
}
