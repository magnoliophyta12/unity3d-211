using System.Collections.Generic;
using UnityEngine;

public class ToastScript : MonoBehaviour
{
    private static ToastScript instance;
    private TMPro.TextMeshProUGUI toastTMP;
    private float timeout = 5.0f;
    private float leftTime;
    private GameObject content;
    private readonly Queue<ToastMessage> messages = new Queue<ToastMessage>();

    public static void ShowToast(string message, string author = null, float? timeout = null)
    {
        if (instance.messages.Count > 0 &&
            instance.messages.Peek().message == message && instance.messages.Peek().author == author)
        {
            return;
        }

        instance.messages.Enqueue(new ToastMessage
        {
            message = message,
            timeout = timeout ?? instance.timeout,
            author = author
        });
    }

    private void OnGameEvent(string eventName, object data)
    {
       
        if(data is GameEvents.INotifier n)
        {
            ShowToast(n.message,n.author);
        }
    }

    void Start()
    {
        instance = this;
        content = transform.Find("Content").gameObject;
        toastTMP = transform.Find("Content/ToastTMP").GetComponent<TMPro.TextMeshProUGUI>();
        content.SetActive(false);
        //GameState.Subscribe(OnGameEvent, "KeyPoint");
        //GameState.Subscribe(OnGameEvent, "Door1");
        GameState.Subscribe(OnGameEvent);
    }

    void Update()
    {
        if (leftTime > 0)
        {
            leftTime -= Time.deltaTime;
            if (leftTime <= 0)
            {
                messages.Dequeue();
                content.SetActive(false);
            }
        }
        else
        {
            if (messages.Count > 0)
            {
                var m = messages.Peek();
                toastTMP.text = !string.IsNullOrEmpty(m.author) ? $"{m.author}: {m.message}" : m.message;
                leftTime = m.timeout;
                content.SetActive(true);
            }
        }
    }

    private void OnDestroy()
    {
        GameState.Unsubscribe(OnGameEvent);
    }

    private class ToastMessage
    {
        public string message { get; set; }
        public string author { get; set; }
        public float timeout { get; set; }
    }
}