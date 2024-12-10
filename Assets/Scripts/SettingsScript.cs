using UnityEngine;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{
    private GameObject content;
    private Slider effectsSlider;
    private Slider ambientSlider;
    private Slider musicSlider;
    private Slider sensitivityXSlider;
    private Slider sensitivityYSlider;
    private Slider fpvSlider;
    private Toggle linkToggle;
    private Toggle muteAllToggle;
    void Start()
    {
        Transform contentTransform = transform.Find("Content");
        content = contentTransform.gameObject;
        if (content.activeInHierarchy)
        {
            Time.timeScale = 0.0f;
        }
        muteAllToggle = contentTransform.Find("Sound/MuteAllToggle").GetComponent<Toggle>();
        effectsSlider = contentTransform.Find("Sound/EffectsSlider").GetComponent<Slider>();
        OnEffectsSliderChanged(effectsSlider.value);
        ambientSlider = contentTransform.Find("Sound/AmbientSlider").GetComponent<Slider>();
        OnAmbientSliderChanged(ambientSlider.value);

        musicSlider = contentTransform.Find("Sound/MusicSlider").GetComponent<Slider>();
        OnMusicSliderChanged(musicSlider.value);

        sensitivityXSlider = contentTransform.Find("Controls/xSensitivitySlider").GetComponent<Slider>();
        sensitivityYSlider = contentTransform.Find("Controls/ySensitivitySlider").GetComponent<Slider>();
        fpvSlider = contentTransform.Find("Controls/FpvSlider").GetComponent<Slider>();
        linkToggle = contentTransform.Find("Controls/LinkToggle").GetComponent<Toggle>();
        OnFpvSliderChanged(fpvSlider.value);

        OnSensitivityXSliderChanged(sensitivityXSlider.value);
        if(!linkToggle.isOn) OnSensitivityYSliderChanged(sensitivityYSlider.value);
        LoadSettings();
    }

    private void LoadSettings()
    {
        if (PlayerPrefs.HasKey(nameof(sensitivityXSlider))){
            OnSensitivityXSliderChanged(PlayerPrefs.GetFloat(nameof(sensitivityXSlider)));
        }
        if (PlayerPrefs.HasKey(nameof(sensitivityYSlider)))
        {
            OnSensitivityYSliderChanged(PlayerPrefs.GetFloat(nameof(sensitivityYSlider)));
        }
        if (PlayerPrefs.HasKey(nameof(linkToggle)))
        {
            linkToggle.isOn=PlayerPrefs.GetInt(nameof(linkToggle))>0;
        }
        if (PlayerPrefs.HasKey(nameof(musicSlider)))
        {
            OnMusicSliderChanged(PlayerPrefs.GetFloat(nameof(musicSlider)));
            musicSlider.value=PlayerPrefs.GetFloat(nameof(musicSlider));
        }
        if (PlayerPrefs.HasKey(nameof(effectsSlider)))
        {
            OnEffectsSliderChanged(PlayerPrefs.GetFloat(nameof(effectsSlider)));
            effectsSlider.value= PlayerPrefs.GetFloat(nameof(effectsSlider));
        }
        if (PlayerPrefs.HasKey(nameof(ambientSlider)))
        {
            OnAmbientSliderChanged(PlayerPrefs.GetFloat(nameof(ambientSlider)));
            ambientSlider.value=PlayerPrefs.GetFloat(nameof(ambientSlider));
        }
        if (PlayerPrefs.HasKey(nameof(muteAllToggle)))
        {
            muteAllToggle.isOn = PlayerPrefs.GetInt(nameof(muteAllToggle)) > 0;
        }
    }
    public void OnSaveButtonClick()
    {
        PlayerPrefs.SetFloat(nameof(effectsSlider), GameState.effectsVolume);
        PlayerPrefs.SetFloat(nameof(ambientSlider), GameState.ambientVolume);
        PlayerPrefs.SetFloat(nameof(musicSlider), GameState.musicVolume);
        PlayerPrefs.SetFloat(nameof(sensitivityXSlider), sensitivityXSlider.value);
        PlayerPrefs.SetFloat(nameof(sensitivityYSlider), sensitivityYSlider.value);
        PlayerPrefs.SetInt(nameof(linkToggle), linkToggle.isOn ? 1 : 0);
        PlayerPrefs.SetInt(nameof(muteAllToggle), muteAllToggle.isOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            Time.timeScale=content.activeInHierarchy? 1.0f : 0.0f;
            content.SetActive(!content.activeInHierarchy);
        }
    }
    public void OnMuteAllToggleChanged()
    {
        if (muteAllToggle.isOn)
        {
            effectsSlider.value=0;
            effectsSlider.enabled=false;
            ambientSlider.value=0;
            ambientSlider.enabled=false;
            musicSlider.value=0;
            musicSlider.enabled=false;
        }
        else
        {
            effectsSlider.value=GameState.effectsVolume;
            effectsSlider.enabled = true;
            ambientSlider.value=GameState.ambientVolume;
            ambientSlider.enabled=true;
            musicSlider.value=GameState.musicVolume;
            musicSlider.enabled = true;
        }
    }

    public void OnFpvSliderChanged(float value)
    {
        GameState.minFpvDistance = Mathf.Lerp(0.5f, 1.5f, value);
    }

    public void OnSensitivityXSliderChanged(float value)
    {
        float sens=Mathf.Lerp(1,10,value);
        GameState.lookSensitivityX=sens;
        if (linkToggle.isOn)
        {
            sensitivityYSlider.value = value;
            GameState.lookSensitivityY = -sens;
        }
    }
    public void OnSensitivityYSliderChanged(float value)
    {
        float sens = Mathf.Lerp(1, 10, value);
        GameState.lookSensitivityY = -sens;
        if (linkToggle.isOn)
        {
            sensitivityXSlider.value = value;
            GameState.lookSensitivityX = sens;
        }
    }
    public void OnEffectsSliderChanged(System.Single value)
    {
        if (!muteAllToggle.isOn) { GameState.effectsVolume = value; }
        GameState.TriggerGameEvent("EffectsVolume", value);
        Debug.Log(GameState.effectsVolume);
    }
    public void OnAmbientSliderChanged(System.Single value)
    {
        if (!muteAllToggle.isOn) { GameState.ambientVolume = value; }
        GameState.TriggerGameEvent("AmbientVolume", value);
        Debug.Log(GameState.ambientVolume);
    }

    public void OnMusicSliderChanged(System.Single value)
    {
        if(!muteAllToggle.isOn) { GameState.musicVolume = value; }
        GameState.TriggerGameEvent("MusicVolume", value);
        Debug.Log(GameState.musicVolume);
    }
}
