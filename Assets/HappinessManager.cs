using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class HappinessManager : MonoBehaviour
{
    //private HappinessManager manager;
    private static int happinessLevel = 0;

    [SerializeField] private TextMeshProUGUI level;
    [SerializeField] private TextMeshProUGUI InteractiveTextDisplay;
    
    [SerializeField] private TextMeshProUGUI cupcakesFoundCounter;

    [SerializeField] private RawImage toggleImage;

    static int cupcakes = 0;
    static bool pickedCupcake = false;
    static int game_end = 0;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        string text = "Happiness level: " + happinessLevel + "%";
        level.SetText(text);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        /*if(pickedCupcake == true && set_picked_cupcake == 0)
        {
            //changeInteractiveText("You have picked a cupcake. \nPress F to continue...");
            pickedCupcake = false;
            set_picked_cupcake = 1;
        }

        if (Input.GetKeyDown(KeyCode.F) && set_picked_cupcake == 1)
        {
            resetInteractiveText();
            set_picked_cupcake = 0;
        }*/

        if (pickedCupcake)
        {
            toggleImage.enabled = true;
            cupcakesFoundCounter.enabled = true;
        }
        else
        {
            toggleImage.enabled = false;
            cupcakesFoundCounter.enabled = false;
        }

        if(happinessLevel > 100)
        {
            game_end = 2;
            SceneManager.LoadScene("EndGame");
        }
    }

    // functions for "press F to interact"
    public void updateInteractiveText()
    {
        string text = "Press F to interact...";
        InteractiveTextDisplay.SetText(text);
    }

    public void changeInteractiveText(string text)
    {
        InteractiveTextDisplay.SetText(text);
    }

    public void resetInteractiveText()
    {
        string text = "";
        InteractiveTextDisplay.SetText(text);
    }

    // -------------------------------------------------------------------------------------

    // functions for the happiness level
    public void updateLevelText()
    {
        string text = "Happiness level: " + happinessLevel + "%";
        level.SetText(text);
        print(happinessLevel);
    }

    public void updateLevel(int value)
    {
        happinessLevel += value;
        //print(happinessLevel);
        updateLevelText();
    }

    public void resetLevel()
    {
        happinessLevel = 0;
        //print(happinessLevel);
    }

    public int getLevel()
    {
        return happinessLevel;
    }

    // -------------------------------------------------------------------------------------

    // functions for cupcakes
    public void pickCupcake()
    {
        cupcakes = cupcakes + 1;
        updateAmountDisplayed();
        pickedCupcake = true;
    }

    public int getCupcakes()
    {
        return cupcakes;
    }

    public void giveCupcakes()
    {
        cupcakes = 0;
    }

    public void updateAmountDisplayed()
    {
        string text = "" + cupcakes + "/7";
        cupcakesFoundCounter.SetText(text);
        print(cupcakesFoundCounter);
    }

    // -------------------------------------------------------------------------------------

    // functions to end game
    // 1 - player chose to end game
    // 2 - player died from happiness
    // 3 - player went out of map

    public void end_game(int code)
    {
        game_end = code;
    }

    public int get_code()
    {
        return game_end;
    }

    public void reset_everything()
    {
        game_end = 0;
        cupcakes = 0;
        pickedCupcake = false;
        happinessLevel = 0;
    }
}
