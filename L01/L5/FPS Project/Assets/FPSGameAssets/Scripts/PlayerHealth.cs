using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public Slider healthSlider;
    public static bool IsAlive{get; private set;}
    private int currentHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        IsAlive = true;
        currentHealth = startingHealth;
        UpdateHealthSlider();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, startingHealth);
        UpdateHealthSlider();
        Debug.Log("Player health: " + currentHealth);
        if (currentHealth <= 0 && IsAlive)
        {
            Debug.Log("Player died");
            Die();
        }
    }

    public void TakeHealth(int health)
    {
        currentHealth += health;
        currentHealth = Mathf.Clamp(currentHealth, 0, 100);
        UpdateHealthSlider();
        Debug.Log("Health: " + currentHealth);
       
    }
    
    void Die()
    {
        var audioSource = GetComponent<AudioSource>();
        Debug.Log("Player died");
        IsAlive = false;
        if (audioSource) {
            audioSource.Play();
        }
        transform.Rotate(-90, 0, 0, Space.Self);
    }

    void UpdateHealthSlider() {
        if (healthSlider) {
            healthSlider.value = currentHealth;
        }
    }
}
