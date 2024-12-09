using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;

public class LightScript : MonoBehaviour
{
    private Light[] dayLights;
    private Light[] nightLights;
    private AudioSource dayAmbient;
    private AudioSource switchLights;
    void Start()
    {
        dayLights = GameObject.FindGameObjectsWithTag("DayLight").Select(g=>g.GetComponent<Light>()).ToArray();
        nightLights = GameObject.FindGameObjectsWithTag("NightLight").Select(g => g.GetComponent<Light>()).ToArray();
        AudioSource[] audioSources = GetComponents<AudioSource>();
        dayAmbient = audioSources[0];
        switchLights = audioSources[1];
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
        dayAmbient.mute = !GameState.isDay;
        switchLights.Play();
    }
}
