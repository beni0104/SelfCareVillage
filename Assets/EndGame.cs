using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEditor.Progress;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    private HappinessManager manager;
    private FirstPersonController player;

    [SerializeField] private Button end_game;
    int buttonClicked = 0;

    bool button_show = false;

    public void Start()
    {
        player = FindObjectOfType<FirstPersonController>();
        manager = FindAnyObjectByType<HappinessManager>();

        end_game.onClick.AddListener(delegate { end_game_OnClick(); });
    }

    private void end_game_OnClick()
    {
        buttonClicked = 1;
    }


    public void Update()
    {
        if (button_show == false)
        {
            if (manager.getLevel() >= 100)
            {
                button_show = true;
                end_game.gameObject.SetActive(true);
            }
        }
        else if (buttonClicked == 1)
        {
            button_show = false;
            end_game.gameObject.SetActive(false);
            buttonClicked = 0;

            manager.end_game(1);
            SceneManager.LoadScene("EndGame");
        }
    }
}
