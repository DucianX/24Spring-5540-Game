using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Rigidbody))]
public class EnemyBehavior : MonoBehaviour
{
    public float speed;
    public Transform target;
    public AudioClip deathSFX;
    
    private Rigidbody _rb;
    PlayerBehavior playerBehavior; 
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    
    void Start()
    {
        if (!target)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
        playerBehavior = FindAnyObjectByType<PlayerBehavior>();
    }
    
    void FixedUpdate()
    {
        if (!LevelManager.IsPlaying)
        {
            _rb.linearVelocity = Vector3.zero;
            _rb.isKinematic = false;
        }
        
        if (target)
        {
            FollowTarget();
        }
    }

    public void DestoryAllEnemies() {
            // find all enemies
            EnemyBehavior[] allEnemies = FindObjectsByType<EnemyBehavior>(FindObjectsSortMode.None);
            
         
            foreach (EnemyBehavior enemy in allEnemies)
            {
                enemy.StartCoroutine(enemy.ShrinkAndDestroy());
            }
    }
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            DestoryAllEnemies();
        }
    }
    void FollowTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        
        // make sure the model stays upright
        Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
        targetRotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);
        transform.rotation = targetRotation;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy")) 
        {
            StartCoroutine(ShrinkAndDestroy());
        }
    }
    
    public IEnumerator ShrinkAndDestroy()
    {
        float duration = 0.1f;
        float elapsed = 0f;
        Vector3 originalScale = transform.localScale;
        
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float progress = elapsed / duration;
            transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, progress);
            yield return null;
        }
        
        destroySelf();
    }
    
    void destroySelf()
    {
        AudioSource.PlayClipAtPoint(deathSFX, transform.position);
        Destroy(gameObject);
    } 
}