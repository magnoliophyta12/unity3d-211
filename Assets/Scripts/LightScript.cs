using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;

public class LightScript : MonoBehaviour
{
    private Light[] dayLights;
    private Light[] nightLights;
    private AudioSource dayAmbient;
   // private bool isDay;
    void Start()
    {
        dayLights = GameObject.FindGameObjectsWithTag("DayLight").Select(g=>g.GetComponent<Light>()).ToArray();
        nightLights = GameObject.FindGameObjectsWithTag("NightLight").Select(g => g.GetComponent<Light>()).ToArray();
        dayAmbient=GetComponent<AudioSource>();
        dayAmbient.volume = GameState.ambientVolume;
        GameState.Subscribe(OnSoundsVolumeTrigger, "AmbientVolume");
        SwitchLight();
    }

    private void OnSoundsVolumeTrigger(string eventName, object data)
    {
        if (eventName == "AmbientVolume")
        {
            dayAmbient.volume = (float)data;
        }
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.N))
        {
            SwitchLight();
        }
    }
    private void SwitchLight()
    {
        GameState.isDay = !GameState.isDay;
        foreach(Light light in dayLights)
        {
            light.enabled = GameState.isDay;
        }
        foreach (Light light in nightLights)
        {
            light.enabled = !GameState.isDay;
        }
    }
}
