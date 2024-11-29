using UnityEngine;

public class FlashlightScript : MonoBehaviour
{
    private Transform parentTransform;

    void Start()
    {
        parentTransform = transform.parent;
        if (parentTransform == null)
        {
            Debug.LogError("FlashlightScript: parentTransform not found.");
        }
    }


    void Update()
    {
        if (parentTransform == null)
        {
            return;
        }
        if (GameState.isFpv)
        {
            transform.forward = Camera.main.transform.forward;
        }
        else
        {
            Vector3 f = Camera.main.transform.forward;
            f.y = 0.0f;
            if (f == Vector3.zero)
            {
                f = Camera.main.transform.up;
                                             
               
            }
            transform.forward = f.normalized;
        }
    }
}
