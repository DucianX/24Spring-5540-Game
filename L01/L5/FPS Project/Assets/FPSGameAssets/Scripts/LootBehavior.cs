using UnityEngine;

public class LootBehavior : MonoBehaviour
{
    public int healthAmount = 10;
    public AudioClip lootSFX;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, 90 * Time.deltaTime);
        if(transform.position.y < Random.Range(1.0f, 3.0f)) {
            Destroy(gameObject.GetComponent<Rigidbody>());
        }
    }

    public void TakeHealth(int health) {
        
    }
    void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")) {
            var PlayerHealth = other.GetComponent<PlayerHealth>();

            if (PlayerHealth) {
                if (lootSFX) AudioSource.PlayClipAtPoint(lootSFX, transform.position);
                
                PlayerHealth.TakeHealth(healthAmount);
                Destroy(gameObject);
            }
        }
    }
}
