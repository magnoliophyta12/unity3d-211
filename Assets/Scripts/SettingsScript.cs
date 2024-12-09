using UnityEngine;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{
    private GameObject content;
    private Slider effectsSlider;
    private Slider ambientSlider;
    void Start()
    {
        Transform contentTransform = transform.Find("Content");
        content = contentTransform.gameObject;
        if (content.activeInHierarchy)
        {
            Time.timeScale = 0.0f;
        }
        effectsSlider = contentTransform.Find("Sound/EffectsSlider").GetComponent<Slider>();
        OnEffectsSliderChanged(effectsSlider.value);
        ambientSlider = contentTransform.Find("Sound/AmbientSlider").GetComponent<Slider>();
        OnAmbientSliderChanged(ambientSlider.value);
       
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            Time.timeScale=content.activeInHierarchy? 1.0f : 0.0f;
            content.SetActive(!content.activeInHierarchy);
        }
    }
    public void OnEffectsSliderChanged(System.Single value)
    {
        //Debug.Log(value);
        GameState.effectsVolume = value;
        GameState.TriggerGameEvent("EffectsVolume", value);
    }
    public void OnAmbientSliderChanged(System.Single value)
    {
        //Debug.Log(value);
        GameState.ambientVolume = value;
        GameState.TriggerGameEvent("AmbientVolume", value);
    }
}
