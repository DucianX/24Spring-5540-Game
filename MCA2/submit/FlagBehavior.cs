using System;
using UnityEngine;

public class FlagBehavior : MonoBehaviour
{
    [SerializeField] private Material marioFlagMaterial;    
    [SerializeField] private Material enemyFlagMaterial;  
    [SerializeField] private GameObject flagObject;
    [SerializeField] private SkinnedMeshRenderer flagSkinnedMeshRenderer;      
    [SerializeField] private Boolean test;
    LevelManager levelManager;
    private bool isAllPickupsCollected = false;         
    private MeshRenderer flagRenderer;
    Boolean tested = false;
    private void Start()
    {
        
        // flagRenderer = flagObject.GetComponent<MeshRenderer>();
        levelManager = FindAnyObjectByType<LevelManager>();
        flagSkinnedMeshRenderer.material = enemyFlagMaterial;
    }

    private void Update()
    {   

        if (test && !tested)
        {
            flagSkinnedMeshRenderer.material = marioFlagMaterial;
            levelManager.LevelBeat();
            tested = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player reached the flag");
            if (isAllPickupsCollected)
            {
                Debug.Log("All pickups collected");
                flagSkinnedMeshRenderer.material = marioFlagMaterial;
                levelManager.LevelBeat();
            }

        }
    }

    public void OnAllPickupsCollected()
    {
        isAllPickupsCollected = true;
    }
}