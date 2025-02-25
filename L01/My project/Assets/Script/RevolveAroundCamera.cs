using System;
using UnityEngine;

public class RevolveAroundCamera : MonoBehaviour
{
    Boolean start = false;
    [SerializeField] GameObject target;
    // velocity:绕太阳旋转的速度
    [SerializeField] private float velocity;
    [SerializeField] private float moveSpeed = 0.5f;       
    [SerializeField] private float minDistance = 20f;
    private Vector3 originalPosition;
    void Start()
    {
        originalPosition = transform.position;
        if (!target) {
            target = GameObject.FindWithTag("Sun");
        }
    }

    // Update is called once per frame
    void Update()
    {   
        // transform的理解：包含了scale，rotation，position
        transform.LookAt(target.transform);
        if (Input.GetKeyDown(KeyCode.Space)) {
            start = !start;
            if (!start) {
                MoveAway();
            }
        }
        if (!target) {
            Debug.Log("You should attempt to either reference the Sun through the inspector or change its tag.");
        }
        if (start) {
            transform.RotateAround(target.transform.position, Vector3.up, 1 * velocity * Time.deltaTime);
            float currentDistance = Vector3.Distance(transform.position, target.transform.position);
            // 计算移动的目标位置
            Vector3 directionToTarget = (target.transform.position - transform.position).normalized;
            Vector3 targetPosition = transform.position + directionToTarget * moveSpeed;

            if (currentDistance > minDistance && start) {
                // 确保不会超过最小距离
                if (Vector3.Distance(targetPosition, target.transform.position) >= minDistance) {
                    // 使用MoveTowards实现平滑移动
                    transform.position = Vector3.MoveTowards(
                        transform.position,
                        targetPosition,
                        moveSpeed * Time.deltaTime
                    );
                }
            } 
              
        } else if (!start) {
                MoveAway();
            }
    }

    void MoveAway() {
        // normalized: 将向量转换为单位向量
        Vector3 directionBack = (originalPosition - transform.position).normalized;
        Vector3 nextBackingPosition = transform.position + directionBack * moveSpeed;
        transform.position = Vector3.MoveTowards(
                transform.position,
                originalPosition,
                moveSpeed * Time.deltaTime
            );
    }
}
