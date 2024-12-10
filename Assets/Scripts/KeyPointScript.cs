using UnityEngine;

public class KeyPointScript : MonoBehaviour
{
    [SerializeField]
    private string keyPointName = "1";
    [SerializeField]
    private int room = -1;
    [SerializeField]
    private float timeout = 5.0f;
    private float leftTime;
    private AudioSource pickedInTime;
    private AudioSource pickedLate;

    public float part;

    void Start()
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        pickedInTime = audioSources[0];
        pickedLate = audioSources[1];
        leftTime = timeout;
        part = 1.0f;
    }

    void Update()
    {
        if (leftTime > 0 && GameState.room==room)
        {
            leftTime -= Time.deltaTime;
            if (leftTime < 0) leftTime = 0;
            part = leftTime / timeout;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameState.collectedItems.Add("Key" + keyPointName, part);
            GameState.TriggerGameEvent("KeyPoint", new GameEvents.MessageEvent { message="Знайдено ключ "+ keyPointName,data=part});
            if(leftTime > 0)
            {
                pickedInTime.Play();
                Debug.Log(pickedInTime);
                Destroy(gameObject, pickedInTime.clip.length);
            }
            else
            {
                pickedLate.Play();
                Debug.Log(pickedLate);
                Destroy(gameObject, pickedLate.clip.length);
            }
        }
    }
}