using UnityEngine;

public class KeyPointScript : MonoBehaviour
{
    [SerializeField]
    private string keyPointName = "1";
    [SerializeField]
    private float timeout = 5.0f;
    private float leftTime;
    public float part;
    void Start()
    {
        leftTime = timeout;
        part = 1.0f;
    }

    void Update()
    {
        if (leftTime > 0) 
        { 
            leftTime -= Time.deltaTime; 
            if(leftTime<0)leftTime = 0;
            part=leftTime/timeout;
        }
        
    }
}
