using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private Vector3 offset;
    public Transform target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         offset = transform.position - target.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate() {
        if (target) {
            transform.position = target.position + offset;
            transform.LookAt(target.position);
        }
    }
}
