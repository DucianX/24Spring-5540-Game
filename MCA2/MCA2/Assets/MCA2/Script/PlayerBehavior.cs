using System;
using System.Collections;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{   
    Boolean touchedHead = false;
    GameObject player;
    [SerializeField] private float jumpKillHeight = 0.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y < 0) {
            LevelManager levelManager = FindAnyObjectByType<LevelManager>();
            levelManager.LevelLost();
            Destroy(gameObject);
        }
    }

    public void OnCollisionEnter(Collision collision) {
        
        if (collision.gameObject.CompareTag("Enemy") ) {
            float heightDifference = player.transform.position.y - collision.gameObject.transform.position.y;
            if (heightDifference < jumpKillHeight && !touchedHead) {
                LevelManager levelManager = FindAnyObjectByType<LevelManager>();
                levelManager.LevelLost();
                Destroy(gameObject);
            } else {
                touchedHead = true;
                Debug.Log("Enemy killed");
                // Get the EnemyBehavior component and call its ShrinkAndDestroy method
                EnemyBehavior enemyBehavior = collision.gameObject.GetComponent<EnemyBehavior>();
                if (enemyBehavior != null) {
                    enemyBehavior.StartCoroutine(enemyBehavior.ShrinkAndDestroy());
                }
                // StartCoroutine(ShrinkAndDestroy(collision.gameObject));
                // collision.gameObject.transform.localScale = Vector3.one * 0.1f;
                // Destroy(collision.gameObject);
            }
            touchedHead = false;

        }
        
    }}
//     public IEnumerator ShrinkAndDestroy(GameObject target)
//     {
//         float duration = 0.1f; 
//         float elapsed = 0f;
//         Vector3 originalScale = target.transform.localScale;
        
//         while (elapsed < duration)
//         {
//             elapsed += Time.deltaTime;
//             float progress = elapsed / duration;
   
//             target.transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, progress);
//             yield return null;
//         }
        
//         Destroy(target);
//     }
// }
