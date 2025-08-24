using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class TextHandler : MonoBehaviour
{
    public bool speaking;
    public bool ready;
    public String message;
    public String written;
    public TMP_Text text;
    public GameObject panel;
    public bool halting = false;
    public PlayerController controller;

    public bool triggered = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created 
    void Start()
    {
        text.enabled = false;
        panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Say(String str)
    {
        if (Time.timeSinceLevelLoad < 0.1 && !triggered)
            return;

        speaking = true;
        panel.SetActive(true);
        text.enabled = true;
        
        message = str;

        Debug.Log("test");

        if (halting == true)
        {
            Debug.Log("halted");
            controller.canMove = false;
        }

        StartCoroutine(Speak());
    }

    IEnumerator Speak()
    {
        triggered = true;


        int i = 0;
        written = "";
        while (i < message.Length)
        {
            written += message[i];
            i++;
            text.text = written;
            yield return new WaitForSeconds(0.04f);
            if (message[i - 1] == '.')
            {
                yield return new WaitForSeconds(0.4f);
            }
        }

        yield return new WaitForSeconds(message.Length * 0.025f + 1f);
        text.text = "";

        text.enabled = false;
        panel.SetActive(false);

        controller.canMove = true;
        halting = false;
        speaking = false;
    }
}
