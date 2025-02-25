using UnityEngine;

[RequireComponent(typeof(AudioSource))]
// 多个金币共享一个class，绑在每个金币上
public class PickupBehavior : MonoBehaviour
{
    // ！！static决定了变量是从属一个instance还是所有instance共享一个变量，static是所有instance共享一个变量
    // private只是控制访问权限
    public static int totalCount = 0;
    public static int totalScore = 0;
    private int scoreVal = 5;
    public float rotationSpeed = 50;
    LevelManager levelManager;
    private AudioSource _audio;
    private FlagBehavior flagBehavior;
    private bool isScoreCounted = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // each coin corresponds to a start
    void Start()
    {
        totalCount += 1;
        _audio = GetComponent<AudioSource>();
        levelManager = FindAnyObjectByType<LevelManager>();
        flagBehavior = FindAnyObjectByType<FlagBehavior>();

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        // Quick test to destroy all pickups
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            DestroyAllPickups();
        }
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
    void DestroyAllPickups()
    {
        // 找到所有带有 PickupBehavior 组件的游戏物体
        PickupBehavior[] allPickups = FindObjectsByType<PickupBehavior>(FindObjectsSortMode.None);
        
        // 遍历并销毁每个金币
        foreach (PickupBehavior pickup in allPickups)
        {
            pickup.DestoryPickup(); // 使用已有的销毁方法
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

    public void DestoryPickup() {
        if (!isScoreCounted) {
            isScoreCounted = true;
            totalScore += scoreVal;
            // Debug.Log("current total score: " + totalScore);
            if(levelManager) {
                levelManager.SetScoreText(totalScore);
            }
            totalCount--;
        }
        
        PlayAudioEffect();
        Animator animator = GetComponent<Animator>();
        animator.SetTrigger("pickupDestroyed");

        // 检查是否所有金币都被收集
        CheckAllPickupsCollected();
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

    private void CheckAllPickupsCollected()
    {
        if (totalCount <= 0 && flagBehavior != null)
        {
            // 通知旗子所有金币都被收集了
            flagBehavior.OnAllPickupsCollected();
            Debug.Log("All pickups collected!");
        }
        if (totalCount <= 0)
        {
            LevelManager levelManager = FindAnyObjectByType<LevelManager>();
            string currentSceneName = levelManager.GetCurrentSceneName();
            Debug.Log("Current scene: " + currentSceneName);
            if (currentSceneName == "level1")
            {
                levelManager.LevelBeat();
            }
        }
    }
}
