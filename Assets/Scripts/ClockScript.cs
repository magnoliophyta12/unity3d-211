using UnityEngine;

public class ClockScript : MonoBehaviour
{
    private TMPro.TextMeshProUGUI clock;
    private float gameTime;

    void Start()
    {
        clock = GetComponent<TMPro.TextMeshProUGUI>();
        gameTime = 0.0f;
    }

    void Update()
    {
        gameTime += Time.deltaTime;

        int hours = (int)(gameTime / 3600);
        int minutes = (int)((gameTime % 3600) / 60);
        int seconds = (int)(gameTime % 60);

        clock.text = $"{hours:00}:{minutes:00}:{seconds:00}";
    }
}