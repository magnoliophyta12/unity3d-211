using System.Linq;
using UnityEngine;

public class Door1Script : MonoBehaviour
{
    [SerializeField]
    private string doorName = "Двері 1";
    [SerializeField]
    private string requiredKey = "1";
    private float openingTime = 3.0f;
    private float timeout = 0f;
    private float openedPart = 0.5f; //частина, при відкритті якої перемикається room
    private AudioSource closedSound;
    private AudioSource openedSound;
    private bool isClosed = true;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Character" && isClosed)
        {
            if (GameState.collectedItems.ContainsKey("Key"+requiredKey))
            {
                GameState.TriggerGameEvent("Door1", new GameEvents.MessageEvent { message = "Двері відчиняються", author=doorName, data = requiredKey });
                //ToastScript.ShowToast("Двері будуть відчинені");
                timeout = openingTime;
                openedSound.Play();
            }
            else
            {
                GameState.TriggerGameEvent("Door1", new GameEvents.MessageEvent { message = "Для відкривання двері вам необхідно знайти ключ "+requiredKey, author = doorName, data = requiredKey });
                //ToastScript.ShowToast("Для відкривання двері вам необхідно знайти ключ №1");
                closedSound.Play();
            }
            
        }
    }
    void Start()
    {
        AudioSource[] audioSources=GetComponents<AudioSource>();
        closedSound = audioSources[0];
        openedSound = audioSources[1];
    }


    void Update()
    {
        if(timeout > 0f)
        {
            float t = Time.deltaTime / openingTime;
            transform.Translate(t, 0, 0);
            if(timeout >= 0.5f && timeout - t < 0.5f)
            {
                GameState.room += 1;
            }
            timeout -= t;
            /*transform.Translate(Time.deltaTime, 0, 0);
            timeout-=Time.deltaTime;*/
        }
    }
}
