using UnityEngine;

public class BatteryScript : MonoBehaviour
{
    FlashlightScript flashLightScript;

    void Start()
    {
        GameObject flashlight = GameObject.Find("Flashlight");
        if (flashlight != null)
        {
            flashLightScript = flashlight.GetComponent<FlashlightScript>();
        }
    }

    void Update()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.Equals(GameObject.Find("Character")))
        {
            flashLightScript.RechargeFlashlight();

            GameObject.Destroy(this.gameObject);
        }
    }
}