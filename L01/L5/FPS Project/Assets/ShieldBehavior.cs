using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ShieldBehavior : MonoBehaviour
{
    AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        if(audioSource.clip) {
            if (collider.CompareTag("Dementor")) {
                audioSource.Play();
            }
        }
        if(collider.CompareTag("Dementor")) {
            audioSource.Play();
        }
    }
}
