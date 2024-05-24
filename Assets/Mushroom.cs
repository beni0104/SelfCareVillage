using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    bool player_detection = false;
    int set_player_detection = 0;

    bool interactive_text_display = false;
    int set_interactive_text_display = 0;

    int pressed_F = 0;

    private HappinessManager manager;


    void Start()
    {
        manager = FindObjectOfType<HappinessManager>();
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
            manager.changeInteractiveText("You can not eat the mushroom.");
            set_interactive_text_display = 1;
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
    }
}
