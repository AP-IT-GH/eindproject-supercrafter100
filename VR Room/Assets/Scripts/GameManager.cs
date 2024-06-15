using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    // Game object references
    public GameObject enteringFence;
    public TMP_Text scoreText;
    public List<GameObject> spawningBarrels = new();
    public AudioSource playerAudioSource;
    public AudioSource fenceAudioSource;
    
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
    
    // Game values
    public int lives = 3;
    public List<GameObject> livesImages = new();
    public int score = 0;
    
    // Private class references
    private PowerupManager _powerupManager;
    
    // Audio clips
    public AudioClip startGameSound;
    
    // Start is called before the first frame update
    void Start()
    {
        this.selectedDifficulty = this.defaultDifficulty;
        this.selectedSpeed = this.defaultSpeed;
        this.UpdateButtonSelectionState();
        
        this.enteringFence.GetComponent<Animator>().SetBool("isOpen", !this.isGameActive);
        this._powerupManager = GetComponent<PowerupManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        this.isGameActive = true;
        this.enteringFence.GetComponent<Animator>().SetBool("isOpen", !this.isGameActive);
        this.lives = 3;
        this.score = 0;

        UpdateLives();
        PlayPlayerAudio(startGameSound);

        StartCoroutine(DelayedStartLaunch());
    }

    public IEnumerator DelayedStartLaunch()
    {
        // After 2 seconds, start spawning
        yield return new WaitForSeconds(2);
        
        foreach (GameObject spawningBarrel in spawningBarrels)
        {
            spawningBarrel.GetComponent<fruitlauncher>().StartLaunching(this.selectedSpeed);
        }
    }

    public void EndGame()
    {
        // End game
        this.isGameActive = false;
        this.enteringFence.GetComponent<Animator>().SetBool("isOpen", !this.isGameActive);
        
        // Stop all barrels from spawning objects
        foreach (GameObject spawningBarrel in spawningBarrels)
        {
            spawningBarrel.GetComponent<fruitlauncher>().StopLaunching();
        }
    }

    public void CatchFruit(CatchType type)
    {
        if (type == CatchType.NORMAL || type == CatchType.SHINY)
        {
            this.score++;
        }
        
        // If shiny, add powerup
        if (type == CatchType.SHINY)
        {
            this._powerupManager.ReceivePowerup();
        }
        
        // If rotten, remove live and activate bad first queued powerup
        if (type == CatchType.ROTTEN)
        {
            this.lives--;
            
            // Make sure there are enough lives
            // and end the game if ther aren't 
            if (this.lives <= 0)
            {
                this.EndGame();
            }
            this._powerupManager.UseRottenPowerup();
        }
        
        this.UpdateScoreboard();
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

    void UpdateScoreboard()
    {
        this.scoreText.text = score.ToString();
    }

    void UpdateLives()
    {
        for (int i = 0; i < livesImages.Count; i++)
        {
            if (lives > i) livesImages[i].SetActive(true);
            else livesImages[i].SetActive(false);
        }
    }

    void PlayPlayerAudio(AudioClip clip)
    {
        playerAudioSource.clip = clip;
        playerAudioSource.Play();
    }
}
