using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ConversationHandler : MonoBehaviour
{
    public GameObject panel;
    public TextHandler handler;
    public String[] messages;
    public bool[] saidByPlayer;
    public UnityEngine.UI.Image img;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator Play()
    {
        Color color;
        for (int i = 0; i < messages.Length; i++)
        {
            if (saidByPlayer[i])
            {
                ColorUtility.TryParseHtmlString("#23192E", out color);
                panel.GetComponent<UnityEngine.UI.Image>().color = color;
            }
            else
            {
                ColorUtility.TryParseHtmlString("#650C10", out color);
                panel.GetComponent<UnityEngine.UI.Image>().color = color;
            }


            handler.Say(messages[i]);

            while (handler.speaking)
            {
                yield return null;
            }
        }

        yield return true;
    }
}
