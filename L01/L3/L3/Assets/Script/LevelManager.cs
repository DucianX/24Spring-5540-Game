using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(AudioSource))]
public class LevelManager : MonoBehaviour
{
    public static bool IsPlaying {get; private set;}
    public TMP_Text timerText;
    public TMP_Text scoreText;
    public float levelTime = 60f; // Time in seconds for each level
    private float countdown;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TMP_Text messageText;
    public AudioClip winSFX;
    public AudioClip loseSFX;
    AudioSource audioSource;
    public string sceneName;
    void Awake() {
        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        countdown = levelTime;
        SetScoreText(0);
        IsPlaying = true;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPlaying) {
            countdownTimer();
            SetTimerText();

            if (PickupBehavior.totalCount < 1) {
                 // win
                LevelBeat();
            }
            else if (countdown <= 0) {
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
        Debug.Log("Time left: " + countdown.ToString("0.00"));
        
    }

    void SetTimerText() {
        timerText.text = countdown.ToString("0.00");
    }

    public void SetScoreText(int currentScore) {
        scoreText.text = "Score: " + currentScore;
    }

    void LevelBeat() {
        IsPlaying = false;
        PlaySoundClip(winSFX);
        messageText.text = "You Win~!";
        DisplayGameMessage(messageText.text);

        PickupBehavior.ResetPickups();
    }

    public void LevelLost() {
        IsPlaying = false;
        PlaySoundClip(loseSFX);
        messageText.text = "ZakoZako~!";
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
}