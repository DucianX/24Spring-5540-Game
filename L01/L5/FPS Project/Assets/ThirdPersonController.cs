using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    public float speed;
    public float jumpHeight;
    
    private Vector3 input;
    private Vector3 moveDirection;
    private CharacterController controller;
    Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float airControl = 10;
    int animState;
    private bool isAttacking = false;
    public Transform cameraTransform;
    public float rotationSpeed = 5;
    float currentVelocity;
    public float smoothSpeed = 0.1f;


    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        if (!cameraTransform) cameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
     // TODO: the lag between states; not transition into 2~4 properly
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        
        // input = transform.right * moveHorizontal + transform.forward * moveVertical;
        input = new Vector3(moveHorizontal, 0f, moveVertical);
        input.Normalize();
        input *= speed;



        if (controller.isGrounded)
        {
            moveDirection = input;
            animState = 0;
            if(input.magnitude >= 0.1f) {
                animState = 1;
                float rotationAngle = Mathf.Atan2(input.x, input.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
                // Quaternion smoothAngle = Quaternion.Euler(0f, rotationAngle, 0f);
                // transform.rotation = Quaternion.Slerp(transform.rotation, smoothRotation, Time.deltaTime * rotationSpeed);

                float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationAngle, ref currentVelocity, smoothSpeed);
                transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, rotationAngle, 0) * Vector3.forward;
                moveDirection = moveDir.normalized * rotationSpeed;
                if (Input.GetButtonDown("Fire1")) {
                    animState = 3;
                    isAttacking = true;
                    Debug.Log("Fire1按下，动画状态：" + animState);

                }
            }
            else {
                // animator = 0;
                if (Input.GetButtonDown("Fire1")) {
                    animState = 4;
                    isAttacking = true;
                    Debug.Log("Fire1按下，动画状态：" + animState);

                }
            }


            if (Input.GetButton("Jump"))
            {
                // moveDirection.y = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
                animState = 2;
            } else 
            {
                moveDirection.y = 0.0f;
            }

        }
        else
        {
            // moveDirection.y = 0.0f;
            // input.y = moveDirection.y;
            // moveDirection = Vector3.Lerp(moveDirection, input, airControl * Time.deltaTime);
            // Keep jump animation while in air
            if (moveDirection.y > 0)
            {
                animState = 2; // Jump animation
            }
        }
        animator.SetInteger("animState", animState);
        moveDirection.y += Physics.gravity.y * Time.deltaTime;
        controller.Move(Time.deltaTime * moveDirection);
    }
}
