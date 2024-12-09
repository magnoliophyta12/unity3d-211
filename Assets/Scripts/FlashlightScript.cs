using UnityEngine;

public class FlashlightScript : MonoBehaviour
{
    private Transform parentTransform;
    private Light light;
    private float charge;
    private float worktime = 10.0f;

    void Start()
    {
        parentTransform = transform.parent;
        if (parentTransform == null)
        {
            Debug.LogError("FlashlightScript: parentTransform not found.");
        }
        light = GetComponent<Light>();
        charge = 1.0f;
    }


    void Update()
    {
        if (parentTransform == null)
        {
            return;
        }
        if (charge > 0 && !GameState.isDay)
        {
            light.intensity = charge;
            charge -= Time.deltaTime / worktime;
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
     public void RechargeFlashlight(float charge)
     {
         this.charge= charge;
     }
    public float chargeLevel => charge;
}
