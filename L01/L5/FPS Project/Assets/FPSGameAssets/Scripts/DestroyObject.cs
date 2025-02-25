using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public float duration = 3f;
    void Start()
    {
        Destroy(gameObject, duration);
    }

}
