using UnityEngine;

public class HelloCube : MonoBehaviour
{
    public float rotationAmount = 5.0f;
    [SerializeField]
    private int health;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Debug.Log("Hello, Cube!");
        transform.Rotate(0, 5, 0);
        // transform.Rotate(xAngle, yAngle, zAngle);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            transform.Rotate(0, rotationAmount, 1);
        }
        if (Input.GetKeyDown(KeyCode.W)) {
            transform.Translate(0, 0, 1);
        }
        else if (Input.GetKeyDown(KeyCode.A)) {
            transform.Translate(-1, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.S)) {
            transform.Translate(0, 0, -1);
        }
        else if (Input.GetKeyDown(KeyCode.D)) {
            transform.Translate(1, 0, 0);
        }
        // Debug.Log("Hello, Cubie!");
    }

    private void OnMouseDown() {
        Debug.Log("Cube Clicked");
        // Destroy(gameObject);
        gameObject.SetActive(false);
    }
}
