using UnityEngine;

public class DementorBehavior : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 5;
    public bool randomSpeed = false;
    public float minDistance = 1;
    public GameObject particleEffect;
    public int damageValue = 10;
    public GameObject lootPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (randomSpeed) {
            moveSpeed = Random.Range(2.0f, 5.0f);
        }
        if(!player) {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!player) return;
        float step = moveSpeed * Time.deltaTime;
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance > minDistance) {
            if (PlayerHealth.IsAlive) {
            
                transform.LookAt(player);
                transform.position = Vector3.MoveTowards(transform.position, player.position, 0.01f);
            }
            else {
                Invoke("DestroyDementor", 2);
            }
            }
        
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Projectile") || other.CompareTag("Protego")) {
            DestroyDementor();
        } else if (other.CompareTag("Player")) {
            var PlayerHealth = other.GetComponent<PlayerHealth>();

            if (PlayerHealth) {
                PlayerHealth.TakeDamage(damageValue);
              
            }
        }
    }

    void DestroyDementor() {
        if (particleEffect) {
            Instantiate(particleEffect, transform.position, transform.rotation);
        }
        
        if (lootPrefab) {
            Instantiate(lootPrefab, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }
}
