using UnityEngine;

public class CastShield : MonoBehaviour
{
    public GameObject shield;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!shield) return;
        if (Input.GetKeyDown(KeyCode.Q)) {
            shield.SetActive(true);
        }
    }
}
