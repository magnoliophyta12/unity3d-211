using UnityEngine;

public class FlashlightScript : MonoBehaviour
{
    private Transform parentTransform;
    private Light light;
    private float charge;
    private float worktime = 10.0f;
    private AudioSource chargeBelow30;
    private AudioSource chargeBelow10;
    private bool warnedBelow30 = false;
    private bool warnedBelow10 = false;

    void Start()
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        chargeBelow30 = audioSources[0];
        chargeBelow10 = audioSources[1];
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
            float charge30Percent = charge * 0.3f;
            float charge10Percent = charge * 0.1f;

            if (charge <= 0.3f && !warnedBelow30)
            {
                chargeBelow30.Play();
                Debug.Log("Warning: charge below 30%");
                warnedBelow30 = true; 
            }

            if (charge <= 0.1f && !warnedBelow10)
            {
                chargeBelow10.Play();
                Debug.Log("Warning: charge below 10%");
                warnedBelow10 = true; 
            }
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
         this.charge+=charge;
        Debug.Log(charge);
     }
    public float chargeLevel => charge;
}
