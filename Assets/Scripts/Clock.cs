using System.Threading;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public float GameTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameTime = Time.time;
    }
}
