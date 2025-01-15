using UnityEngine;

public class YaapingToggleRef : MonoBehaviour
{
    private YappingMouth YappingMouth;

    private void Awake()
    {
        YappingMouth = FindAnyObjectByType<YappingMouth>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (YappingMouth != null)
        {
            YappingMouth.ToggleAnimation();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
