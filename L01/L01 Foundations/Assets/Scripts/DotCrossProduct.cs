using UnityEngine;

public class MathDemo2 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject player;
    void Start()
    {
        Debug.Log("Enemy pos:" + transform.position);
        Debug.Log("Player pos:" + player.transform.position);

        var direction = transform.position - player.transform.position;
        Debug.Log("Direction vector: " + direction);
        var distance = direction.magnitude;
        Debug.Log("Distance between: " + distance);

        var distance2 = Vector3.Distance(transform.position, player.transform.position);
        Debug.Log("Distance between: " + distance2);
    }

    // Update is called once per frame
    void Update()
    {
        float dotProduct = Vector3.Dot(transform.position, player.transform.position);
        Vector3 crossProduct = Vector3.Cross(transform.position, player.transform.position);
        float angle = Vector3.Angle(transform.position, player.transform.position);
        Debug.Log("Dot product: " + dotProduct);
        Debug.Log("Cross Product: " + crossProduct);
        Debug.Log("Angle: " + angle);
    }
}
