using UnityEngine;

public class ChatTrigger : MonoBehaviour
{
    public bool triggered = false;
    public ConversationHandler handler;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (Time.timeSinceLevelLoad < 0.1f)
            return;
            
        if (!triggered)
        {
            StartCoroutine(handler.Play());
            triggered = true;
        }
    }
}
