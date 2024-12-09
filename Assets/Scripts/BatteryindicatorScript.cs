using UnityEngine;
using UnityEngine.UI;

public class BatteryindicatorScript : MonoBehaviour
{
    private Image image;
    private FlashlightScript FlashlightScript;
    void Start()
    {
        image=GetComponent<Image>();
        FlashlightScript=GameObject.Find("Flashlight").GetComponent<FlashlightScript>();
    }

    void Update()
    {
        //Debug.Log("flashlight:"+FlashlightScript.chargeLevel);
        //Debug.Log("fillamount:" +image.fillAmount);
        image.fillAmount = FlashlightScript.chargeLevel;
        image.color = new Color(
            1f-image.fillAmount,
            image.fillAmount,
            0.2f
        );
    }
}
