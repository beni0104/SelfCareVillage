using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static System.Net.Mime.MediaTypeNames;
using UnityEngine.SceneManagement;

public class manager : MonoBehaviour
{
    private HappinessManager scene_manager;

    [SerializeField] private Button return_game;
    int buttonClicked = 0;

    [SerializeField] private TextMeshProUGUI GameEnded_Text;

    int reason = 0;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        scene_manager = FindAnyObjectByType<HappinessManager>();
        reason = scene_manager.get_code();

        return_game.onClick.AddListener(delegate { game_OnClick(); });

        if (reason == 1)
        {
            string text = "Yay! You have finished the game. Play again?";
            GameEnded_Text.SetText(text);
        } 
        else if (reason == 2)
        {
            string text = "Ops, looks like you have died from too much happiness.";
            GameEnded_Text.SetText(text);
        }
        else if (reason == 3)
        {
            string text = "Ops, looks like you fell through the map.";
            GameEnded_Text.SetText(text);
        }
    }

    void NewDialogue(string text)
    {
        GameEnded_Text.SetText(text);
    }

    private void game_OnClick()
    {
        buttonClicked = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (buttonClicked == 1)
        {
            scene_manager.reset_everything();
            SceneManager.LoadScene("StartGame");
        }
    }
}
