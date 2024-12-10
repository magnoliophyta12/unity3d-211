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
    private AudioSource nightAmbient;
    private AudioSource nightMusic;
    private AudioSource dayMusic;
    void Start()
    {
        dayLights = GameObject.FindGameObjectsWithTag("DayLight").Select(g=>g.GetComponent<Light>()).ToArray();
        nightLights = GameObject.FindGameObjectsWithTag("NightLight").Select(g => g.GetComponent<Light>()).ToArray();
        AudioSource[] audioSources = GetComponents<AudioSource>();
        dayAmbient = audioSources[0];
        switchLights = audioSources[1];
        nightAmbient = audioSources[2];
        nightMusic = audioSources[3];
        dayMusic = audioSources[4];
        dayAmbient.volume = GameState.ambientVolume;
        dayMusic.volume = GameState.musicVolume;
        switchLights.volume = GameState.effectsVolume;
        GameState.Subscribe(OnSoundsVolumeTrigger, "AmbientVolume");
        GameState.Subscribe(OnSoundsVolumeTrigger, "EffectsVolume");
        GameState.Subscribe(OnSoundsVolumeTrigger, "MusicVolume");
        SwitchLight();
    }

    private void OnSoundsVolumeTrigger(string eventName, object data)
    {
        if (eventName == "AmbientVolume")
        {
            dayAmbient.volume = (float)data;
            nightAmbient.volume = (float)data;
        }
        else if (eventName == "MusicVolume")
        {
            nightMusic.volume = (float)data;
            dayMusic.volume = (float)data;
        }
        else if (eventName == "EffectsVolume")
        {
            switchLights.volume = (float)data;
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
        dayMusic.mute = !GameState.isDay;
        nightAmbient.mute = GameState.isDay;
        nightMusic.mute = GameState.isDay;
        switchLights.Play();
    }
}
