using Unity.VisualScripting;
using UnityEngine;

public class TowerAI : MonoBehaviour
{
    public enum TowerState {Patrol, Attack, Die}
    public TowerState currentState = TowerState.Patrol;
    [Header("Patrol Settings")]
    public Transform turret;
    public float rotationSpeed = 30f;
    public float maxRotationAngle = 90f;
    public float detectionRange = 10f;
    Transform target;
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireRate = 2;
    float fireCooldown = 0f;
    float gunnerRotation = 126f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState) {
            case TowerState.Patrol:
                Patrol();
                break;
            case TowerState.Attack:
                Attack();
                break;
            case TowerState.Die:
                Die();
                break;
            
        }
        LookForEnemies();
    }

    void Patrol() {
        Debug.Log("Patrolling...");
        // 这里要传入自变量x，如果传入deltatime，那么就基本不会变（渲染时间一致）
        float angle = Mathf.PingPong(rotationSpeed * Time.time, maxRotationAngle * 2) - maxRotationAngle;
        // 让炮塔围绕Y轴旋转
        turret.localRotation = Quaternion.Euler(0, angle + gunnerRotation, 0);
        // Time.deltaTime 的关键作用正是将更新从"每帧线性"转换为"对现实时间线性"。避免了高帧率设备上游戏运行更快
        // turret.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        LookForEnemies();
    }

    void Attack() {
        Debug.Log("Attacking..");

        if(target == null || Vector3.Distance(transform.position, target.position) > detectionRange) {
            target = null;
            currentState = TowerState.Patrol;
            return;
        }
        // Facing the target
        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        // Rotate the cannon towards the target
        turret.rotation = Quaternion.Slerp(turret.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        // Cooldown for shooting
        if (fireCooldown <= 0) {
            Shoot();
            fireCooldown = 1f / fireRate;
        }
        fireCooldown -= Time.deltaTime;
    }

    void Shoot() {
        
        var bullet = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        BulletBehavior bulletBehavior = bullet.GetComponent<BulletBehavior>();
        if (bulletBehavior != null) {
            bulletBehavior.SetTarget(target);
        }
    }

    void Die() {
        Debug.Log("Die");
    }

    // Within the detection range, find the nearest enemy
    // 1. Get all colliders within the detection range
    // 2. Iterate, check if the collider is an enemy
    // 3. Find the nearest enemy
    // 4. If an enemy is found, set it as the target and change the state to Attack
    void LookForEnemies() {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRange);
        Transform nearestEnemy = null;
        float nearestDistance = Mathf.Infinity;
        foreach(Collider collider in colliders) {
            if (collider.CompareTag("Enemy")) {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                if (distance < nearestDistance) {
                    nearestEnemy = collider.transform;
                    nearestDistance = distance;
                }
            }
        }
        if(nearestEnemy) {
            target = nearestEnemy;
            Debug.Log("Target Detected: " + target.name);
            currentState = TowerState.Attack;
        }
    }
    void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
