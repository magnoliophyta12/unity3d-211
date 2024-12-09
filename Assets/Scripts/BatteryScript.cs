using System.Threading;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BatteryScript : MonoBehaviour
{
    [SerializeField]
    private float charge;

    FlashlightScript flashLightScript;

    private float leftTime;
    public float part;

    private AudioSource collectSound;

    private float destroyTimeout;

    void Start()
    {
        collectSound = GetComponent<AudioSource>();
        destroyTimeout = 0f;
        collectSound.volume = GameState.effectsVolume;
        GameState.Subscribe(OnSoundsVolumeTrigger, "EffectsVolume");
        leftTime = charge;
        part = 1.0f;
        GameObject flashlight = GameObject.Find("Flashlight");
        if (flashlight != null)
        {
            flashLightScript = flashlight.GetComponent<FlashlightScript>();
        }
    }

    void Update()
    {
        if(destroyTimeout > 0f)
        {
            destroyTimeout-=Time.deltaTime;
            if(destroyTimeout <= 0f)
            {
                Destroy(gameObject);
            }
        }
        if (leftTime > 0)
        {
            leftTime -= Time.deltaTime;
            if (leftTime < 0) leftTime = 0;
            part = leftTime / charge;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.Equals(GameObject.Find("Character")))
        {
            collectSound.Play();

            flashLightScript.RechargeFlashlight(charge);

           // GameObject.Destroy(this.gameObject);
            destroyTimeout = 1f;
        }
    }
    private void OnSoundsVolumeTrigger(string eventName, object data)
    {
        if (eventName == "EffectsVolume")
        {
            collectSound.volume=(float)data;
        }
    }
}