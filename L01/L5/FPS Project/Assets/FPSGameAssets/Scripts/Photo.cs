using UnityEngine;

public class PhotoDisplaySystem : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] private Camera mainCamera;  // 主相机
    [SerializeField] private Camera photoCamera; // 用于拍照的相机
    [SerializeField] private KeyCode captureKey = KeyCode.R;
    
    [Header("Display Settings")]
    [SerializeField] private float displayDuration = 2f;  // 照片显示时长
    [SerializeField] private Vector3 displayOffset = new Vector3(0, 0, 2f);  // 照片显示位置偏移
    [SerializeField] private Vector2 photoSize = new Vector2(1.6f, 0.9f);    // 照片尺寸（16:9）
    [SerializeField] private float appearSpeed = 2f;  // 照片出现的速度
    
    [Header("Photo Frame")]
    [SerializeField] private Material photoMaterial;  // 照片材质
    [SerializeField] private float frameWidth = 0.1f; // 相框宽度
    [SerializeField] private Color frameColor = Color.white; // 相框颜色
    
    private GameObject photoDisplay;  // 显示照片的游戏物体
    private RenderTexture photoTexture;  // 渲染照片的纹理
    private float displayTimer;
    private bool isDisplaying;
    
    void Start()
    {
        // 初始化相机
        if (mainCamera == null) mainCamera = Camera.main;
        if (photoCamera == null)
        {
            // 创建拍照相机
            GameObject photoCamObj = new GameObject("PhotoCamera");
            photoCamera = photoCamObj.AddComponent<Camera>();
            photoCamera.CopyFrom(mainCamera);
            photoCamera.enabled = false;  // 只在拍照时启用
        }
        
        // 创建渲染纹理
        photoTexture = new RenderTexture(1920, 1080, 24);
        photoCamera.targetTexture = photoTexture;
        
        // 初始化照片材质
        if (photoMaterial == null)
        {
            photoMaterial = new Material(Shader.Find("Standard"));
            photoMaterial.SetFloat("_Glossiness", 0.0f);  // 无反光
        }
    }
    
    void Update()
    {
        if (Input.GetKeyDown(captureKey))
        {
            TakePhoto();
        }
        
        if (isDisplaying)
        {
            UpdatePhotoDisplay();
        }
    }
    
    void TakePhoto()
    {
        // 拍照音效
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null) audioSource.Play();
        
        // 拍照
        photoCamera.Render();
        
        // 创建照片展示物体
        CreatePhotoDisplay();
        
        // 重置显示计时器
        displayTimer = displayDuration;
        isDisplaying = true;
    }
    
    void CreatePhotoDisplay()
    {
        // 如果已存在旧的展示物体，先销毁
        if (photoDisplay != null)
        {
            Destroy(photoDisplay);
        }
        
        // 创建新的展示物体
        photoDisplay = new GameObject("PhotoDisplay");
        
        // 创建相框
        CreatePhotoFrame();
        
        // 创建照片平面
        GameObject photoPlane = GameObject.CreatePrimitive(PrimitiveType.Quad);
        photoPlane.transform.SetParent(photoDisplay.transform);
        photoPlane.transform.localScale = new Vector3(photoSize.x, photoSize.y, 1);
        
        // 设置材质
        Material photoPlaneMaterial = new Material(photoMaterial);
        photoPlaneMaterial.mainTexture = photoTexture;
        photoPlane.GetComponent<MeshRenderer>().material = photoPlaneMaterial;
        
        // 设置初始位置和旋转
        photoDisplay.transform.position = mainCamera.transform.position + mainCamera.transform.rotation * displayOffset;
        photoDisplay.transform.rotation = mainCamera.transform.rotation;
        
        // 设置初始缩放为0
        photoDisplay.transform.localScale = Vector3.zero;
    }
    
    void CreatePhotoFrame()
    {
        // 创建相框的四条边
        CreateFrameEdge(new Vector3(0, photoSize.y/2 + frameWidth/2, 0), new Vector3(photoSize.x + frameWidth*2, frameWidth, 0.1f)); // 上边
        CreateFrameEdge(new Vector3(0, -photoSize.y/2 - frameWidth/2, 0), new Vector3(photoSize.x + frameWidth*2, frameWidth, 0.1f)); // 下边
        CreateFrameEdge(new Vector3(-photoSize.x/2 - frameWidth/2, 0, 0), new Vector3(frameWidth, photoSize.y, 0.1f)); // 左边
        CreateFrameEdge(new Vector3(photoSize.x/2 + frameWidth/2, 0, 0), new Vector3(frameWidth, photoSize.y, 0.1f)); // 右边
    }
    
    void CreateFrameEdge(Vector3 position, Vector3 scale)
    {
        GameObject edge = GameObject.CreatePrimitive(PrimitiveType.Cube);
        edge.transform.SetParent(photoDisplay.transform);
        edge.transform.localPosition = position;
        edge.transform.localScale = scale;
        
        Material frameMaterial = new Material(Shader.Find("Standard"));
        frameMaterial.color = frameColor;
        edge.GetComponent<MeshRenderer>().material = frameMaterial;
    }
    
    void UpdatePhotoDisplay()
    {
        if (displayTimer > 0)
        {
            // 逐渐显示照片
            float targetScale = Mathf.Min(1, (displayDuration - displayTimer) * appearSpeed);
            photoDisplay.transform.localScale = Vector3.one * targetScale;
            
            displayTimer -= Time.deltaTime;
        }
        else
        {
            // 显示时间结束，销毁照片
            if (photoDisplay != null)
            {
                Destroy(photoDisplay);
            }
            isDisplaying = false;
        }
    }
    
    void OnDestroy()
    {
        // 清理资源
        if (photoTexture != null)
        {
            photoTexture.Release();
            Destroy(photoTexture);
        }
    }
}