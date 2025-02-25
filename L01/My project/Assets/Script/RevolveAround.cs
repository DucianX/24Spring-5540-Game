using UnityEngine;

public class Revolution : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] private float velocity;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!target) {
            target = GameObject.FindWithTag("Sun");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!target) {
            Debug.Log("You should attempt to either reference the Sun through the inspector or change its tag.");
        }
        transform.RotateAround(target.transform.position, Vector3.up, 1 * velocity * Time.deltaTime);
    }
}
