// using System.Numerics;
using UnityEngine;

public class MathDemo : MonoBehaviour
{
    private Vector3 position;
    private Vector3 direction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Vector3 up = Vector3.up; // 010
        Vector3 down = Vector3.down;

        var forward = Vector3.forward;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            transform.Rotate(Vector3.up * 90 * Time.deltaTime);
        }
        transform.Rotate(Vector3.up * 90 * Time.deltaTime);
    }
}
