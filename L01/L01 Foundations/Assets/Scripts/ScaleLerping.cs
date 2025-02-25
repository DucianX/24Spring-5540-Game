using UnityEngine;
using UnityEngine.InputSystem;

public class ScaleLerping : MonoBehaviour
{
    public int stepVersion;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float step = 1;
        if (stepVersion == 0) {
             step = Time.deltaTime * 5;
        }
        else if (stepVersion == 1) {
             step = Time.time * 5;
        }
        else if (stepVersion == 2) {
            step = Mathf.PingPong(Time.time, 1);
            Debug.Log("step " + step);
        }
        
        else if (stepVersion == 3) {
            step = (Mathf.Sin(Time.time) + 1) / 2;
        }
        
        // (1,1,1)
        transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one * 3, step);
    }
}
