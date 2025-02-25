using UnityEngine;

public class ColorEffect : MonoBehaviour
{
    [SerializeField] private Color colorOne = Color.yellow;
    [SerializeField] private Color colorTwo;
    [SerializeField] private float changeSpeed = 1f;

    // 用于存储材质引用
    private Material sunMaterial;
    // 用于颜色插值的计时器
    private float colorTimer = 0f;
    void Start()
    {
        sunMaterial = GetComponent<Renderer>().material;
        if (sunMaterial == null)
        {
            Debug.LogError("Material NotFound");
            enabled = false;
            return;
        }
    }

    void Update()
    {
        // deltaTime的理解：
        //     把关于帧线性改为关于时间线性。
        //     假设60帧，故意让计算机以秒为单位进行等待，等待之后再渲染，而不是有多快就渲染多快。

        // 第一部分：创建一个关于时间线性变化的参数
       
        colorTimer += Time.deltaTime * changeSpeed;
        
        // Pingpong第一个参数：是从开始执行这个元素到当前的时间。通过对第一个参数取模来让他映射
        float lerpValue = Mathf.PingPong(colorTimer, 1f);
        
        // 使用Color.Lerp在两种颜色之间进行插值
        // lerpValue决定了当前颜色在两个目标颜色之间的位置
        Color currentColor = Color.Lerp(colorOne, colorTwo, lerpValue);
        
        // 将计算出的颜色应用到材质
        sunMaterial.color = currentColor;
    }
}
