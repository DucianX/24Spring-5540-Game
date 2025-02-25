using UnityEngine;

[RequireComponent(typeof(AudioSource))]
// 多个金币共享一个class，绑在每个金币上
public class PickupBehavior : MonoBehaviour
{
    // ！！static决定了变量是从属一个instance还是所有instance共享一个变量，static是所有instance共享一个变量
    // private只是控制访问权限
    public static int totalCount = 0;
    public static int totalScore = 0;
    private int scoreVal = 1;
    public float rotationSpeed = 50;
    LevelManager levelManager;
    private AudioSource _audio;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // each coin corresponds to a start
    void Start()
    {
        totalCount += 1;
        _audio = GetComponent<AudioSource>();
        levelManager = FindAnyObjectByType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    void Rotate() {
       
    }

    void OnTriggerEnter(Collider collider) {
        // Debug.Log("Score: " + totalScore);
        // OnDestory();
        if (collider.CompareTag("Player")) {
            DestoryPickup();
        }
   
    }

    void OnDestory() {
        // totalCount--;
        // // Debug.Log("Remaining pickups: " + totalCount);
        // PlayAudioEffect();
        // if (totalCount <= 0) {
        //     // Debug.Log("You win!");
        //     // Debug.Log("Final total score: " + totalScore);
        // }
    }

    void DestoryPickup() {
        totalScore += scoreVal;
        // Debug.Log("current total score: " + totalScore);
        if(levelManager) {
            levelManager.SetScoreText(totalScore);
        }
        PlayAudioEffect();
        Animator animator = GetComponent<Animator>();
        animator.SetTrigger("pickupDestroyed");
        totalCount--;
        Destroy(gameObject, 2);
    }

    void PlayAudioEffect() {
        AudioClip clipToPlay = _audio.clip;
        // 播放音效，播放来源
        AudioSource.PlayClipAtPoint(clipToPlay, Camera.main.transform.position);
    }

    public static void ResetPickups() {
        totalCount = 0;
        totalScore = 0;
    }
}
