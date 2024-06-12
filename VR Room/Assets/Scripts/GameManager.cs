using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    // Game object references
    public GameObject enteringFence;
    
    // Settings
    public List<GameObject> difficultyButtons = new();
    public List<GameObject> speedButtons = new();

    // Defaults
    public Difficulty defaultDifficulty = Difficulty.EASY;
    public Speed defaultSpeed = Speed.SLOW;

    // Active settings
    public Difficulty selectedDifficulty;
    public Speed selectedSpeed;
    public bool isGameActive = false;
    
    
    // Start is called before the first frame update
    void Start()
    {
        this.selectedDifficulty = this.defaultDifficulty;
        this.selectedSpeed = this.defaultSpeed;
        this.UpdateButtonSelectionState();
        
        this.enteringFence.GetComponent<Animator>().SetBool("isOpen", !this.isGameActive);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        this.isGameActive = true;
        this.enteringFence.GetComponent<Animator>().SetBool("isOpen", !this.isGameActive);
    }

    public void UpdateDifficulty(String inputDifficulty)
    {
        Enum.TryParse(inputDifficulty.ToUpper(), out Difficulty difficulty);
        this.selectedDifficulty = difficulty;
        this.UpdateButtonSelectionState();
    }

    public void UpdateSpeed(String inputSpeed)
    {
        Enum.TryParse(inputSpeed.ToUpper(), out Speed speed);
        this.selectedSpeed = speed;
        this.UpdateButtonSelectionState();
    }

    void UpdateButtonSelectionState()
    {
        // Difficulty buttons
        foreach (GameObject button in difficultyButtons)
        {
            // Get the text field attached to the button
            TMP_Text textMeshPro = button.transform.GetChild(0).gameObject.GetComponent<TMP_Text>();
            Enum.TryParse(textMeshPro.text.ToUpper(), out Difficulty parsedDifficulty);
            
            Image img = button.GetComponent<Image>();
            if (selectedDifficulty == parsedDifficulty)
                img.color = Color.blue;
            else
                img.color = Color.white;
        }
        
        // Speed buttons
        foreach (GameObject button in speedButtons)
        {
            // Get the text field attached to the button
            TMP_Text textMeshPro = button.transform.GetChild(0).gameObject.GetComponent<TMP_Text>();
            Enum.TryParse(textMeshPro.text.ToUpper(), out Speed parsedSpeed);
            
            Image img = button.GetComponent<Image>();
            if (selectedSpeed == parsedSpeed)
                img.color = Color.blue;
            else
                img.color = Color.white;
        }
    }
}
