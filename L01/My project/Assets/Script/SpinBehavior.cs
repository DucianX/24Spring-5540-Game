using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] private float rotationVelo;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        // Rotate第一个是旋转轴，第二个是转速
        transform.Rotate(new Vector3(0, 1, 0), rotationVelo * Time.deltaTime);
    }
}
