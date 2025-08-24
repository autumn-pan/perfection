using System;
using UnityEngine;

public class TextTrigger : MonoBehaviour
{
    public TextHandler handler;
    public String message;
    public bool triggered = false;
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
        if (Time.timeSinceLevelLoad < 1)
            return;
            
        if (triggered)
            return;

        handler.Say(message);
        triggered = true;
    }
}
