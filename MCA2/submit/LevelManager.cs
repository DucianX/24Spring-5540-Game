using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(AudioSource))]
public class LevelManager : MonoBehaviour
{
    public static bool IsPlaying {get; private set;}
    [Header("UI Elements")]
    public TMP_Text timerText;
    public TMP_Text nextLevelButtonText;
    public TMP_Text scoreText;
    public float levelTime = 60f; // Time in seconds for each level
    private float countdown;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TMP_Text messageText;
    [Header("Sound Effects")]
    public AudioClip winSFX;
    public AudioClip loseSFX;
    public AudioClip BGM;
    AudioSource audioSource;
    [Header("Scene Information")]
    public string currentSceneName;
    private Scene currentScene;
    private int currentSceneIndex;
    public GameObject nextLevelButton; 
    void Awake() {
        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        PlaySoundClip(BGM);
        countdown = levelTime;
        SetScoreText(0);
        IsPlaying = true;
        audioSource = GetComponent<AudioSource>();

        // Get current scene information
        currentScene = SceneManager.GetActiveScene();
        currentSceneName = currentScene.name;
        currentSceneIndex = currentScene.buildIndex;
    }
    public string GetCurrentSceneName()
    {
        return currentSceneName;
    }

    public int GetCurrentSceneIndex()
    {
        return currentSceneIndex;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPlaying) {
            countdownTimer();
            SetTimerText();

            // if (PickupBehavior.totalCount < 1 ) {
            //      // win
            //     LevelBeat();
            // }
            if (countdown <= 0) {
           // lose
                LevelLost();
            }
        }
    
    }

    void countdownTimer() {
        countdown -= Time.deltaTime;
        if (countdown <= 0) {
            // Load the next level
            countdown = 0;
        }
        
    }

    void SetTimerText() {
        timerText.text = countdown.ToString("0.00");
    }

    public void SetScoreText(int currentScore) {
        scoreText.text = "Score: " + currentScore;
    }

    public void LevelBeat() {
        IsPlaying = false;
        PlaySoundClip(winSFX);
        // messageText.text = "You Win~!";
        // DisplayGameMessage(messageText.text);

        
        if (currentSceneName == "level1") 
        {
            messageText.text = "Level Complete!";
            DisplayGameMessage(messageText.text);

            if (nextLevelButton != null) {
                nextLevelButton.SetActive(true);
            }
        } 
        else if (currentSceneName == "level2") 
        {
            messageText.text = "Game Completed~!";
            DisplayGameMessage(messageText.text);
   
            if (nextLevelButton != null) {
                nextLevelButton.SetActive(true);

            }
        }

        // Destory all enemies and pickups
        EnemyBehavior[] enemyBehaviors = FindObjectsByType<EnemyBehavior>(FindObjectsSortMode.None);
        foreach (EnemyBehavior enemyBehavior in enemyBehaviors) {
            enemyBehavior.speed = 0;
        }
        
        PickupBehavior.ResetPickups();
    }

    public void LevelLost() {
        IsPlaying = false;
        PlaySoundClip(loseSFX);
        messageText.text = "Game Over~!";
        DisplayGameMessage(messageText.text);
        PickupBehavior.ResetPickups();
        Invoke("ReloadSameScene", 3);

    }

    void PlaySoundClip(AudioClip clip) {
        audioSource.clip = clip;
        audioSource.Play();
    }

    void DisplayGameMessage(string message) {
        messageText.text = message;
        messageText.enabled = true;
    }

    void LoadSceneByName(string name) {
        SceneManager.LoadScene(name);
    }

    void LoadSceneByIndex(int index) {
        SceneManager.LoadScene(index);
    }

    void ReloadSameScene() {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
    public void LoadNextLevel()
    {
     
        if (currentSceneName == "level1")
        {
            SceneManager.LoadScene("level2");
        }
        else if (currentSceneName == "level2")
        {
            SceneManager.LoadScene("level1");
        }
   
        
    }
}