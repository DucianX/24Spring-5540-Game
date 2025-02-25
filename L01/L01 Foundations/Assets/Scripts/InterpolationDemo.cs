using UnityEngine;

public class InterpolationDemo : MonoBehaviour
{
    public Transform player;
    public float speed = 5f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!player) {
            if (GameObject.FindGameObjectWithTag("Player")) {
                player = GameObject.FindGameObjectWithTag("Player").transform;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!player) 
            return;
    
        // deltatime是一个时间间隔，由帧率计算而来。
        // 让物体运动根据帧率->物体运动根据时间（不同帧率下经过相同时间到达相同位置）
        float step = Time.deltaTime * speed; 
        // transform.position = Vector3.Lerp(transform.position, player.position, step);
        transform.position = Vector3.MoveTowards(transform.position, player.position, step);
            
        
        
    }
}
