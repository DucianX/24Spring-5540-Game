using UnityEngine;

public class FireChainMovement : MonoBehaviour
{
    [SerializeField] private float swingAngle = 45f;    // 摆动角度范围
    [SerializeField] private float swingSpeed = 2f;     // 摆动速度
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 使用 Time.time 和 sin 函数创建往复摆动
        float angle = swingAngle * (Time.time * swingSpeed);
        
        // 应用旋转到物体
        transform.rotation = Quaternion.Euler(0, 0, -90 + angle);  
    }

    void OnCollisionEnter(Collision collision) {
        
    }

    
}
