using UnityEngine;

public class ColorLerper : MonoBehaviour
{
    public Color initialColor = Color.red;
    public Color targetColor = Color.green;
    private Renderer renderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        renderer = GetComponent<Renderer>();
        Debug.Log(renderer.material.color);
        renderer.material.color = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        float step = Mathf.PingPong(Time.time, 1);
        renderer.material.color = Color.Lerp(initialColor, targetColor, step);
    }
}
