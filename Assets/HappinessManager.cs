using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HappinessManager : MonoBehaviour
{
    private static int happinessLevel = 0;

    [SerializeField] private TextMeshProUGUI level;
    [SerializeField] private TextMeshProUGUI InteractiveTextDisplay;

    static int cupcakes = 0;
    static bool pickedCupcake = false;
    static int set_picked_cupcake = 0;

    // Start is called before the first frame update
    void Start()
    {
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
}
