using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5;
    public float jumpForce = 5;
    bool isGrounded;
    Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // 每帧调用（用于玩家输入）
    void Update()
    {
        if (!LevelManager.IsPlaying) {
            return;
        }
        Jump();
    }

    // 每时间间隔调用（用于物理）
    private void FixedUpdate() {
        if (LevelManager.IsPlaying) {
            Move();
        } else {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        
    }

    private void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0, vertical).normalized;
        float speedMultiplier = Input.GetKey(KeyCode.LeftShift) ? 3f : 1f;
        rb.AddForce(movement * moveSpeed * speedMultiplier);
    }

    void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];

        if(contact.normal.y > 0.5f) {
            isGrounded = true;
        }

        // Debug.Log("Collided position: " + contact.point);
        // Debug.Log("Collided normal: " + contact.normal);
    }

    void OnCollisionStay(Collision collision)
    {

    }

    void Jump() {
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded) {
            // ForceMode的施加力的方式：可以改为给冲量/加速度/速度变化
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            // 在空中了，isGrounded = false
            isGrounded = false;
        }
    }
}
