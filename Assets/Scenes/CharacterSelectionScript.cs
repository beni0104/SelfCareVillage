using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelectionScript : MonoBehaviour
{
    public GameObject[] characters;
    public int selectedCharacter = 0;

    public void NextCharacter()
    {
        characters[selectedCharacter].SetActive(false);
        selectedCharacter = (selectedCharacter + 1) % characters.Length;
        characters[selectedCharacter].SetActive(true);
        Console.Write(selectedCharacter);
    }

    public void PreviousCharacter()
    {
        characters[selectedCharacter].SetActive(false);
        selectedCharacter--;
        if (selectedCharacter < 0)
        {
            selectedCharacter += characters.Length;
        }
        characters[selectedCharacter].SetActive(true);
        Console.Write(selectedCharacter);
    }

    public void StartGame()
    {
        
    }
    
    // Start is called before the first frame update
    void Start()
    {
        characters[selectedCharacter].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
