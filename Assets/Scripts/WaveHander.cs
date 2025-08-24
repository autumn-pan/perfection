using System.Collections;
using UnityEngine;

public class WaveHander : MonoBehaviour
{
    public Transform player;
    public GameObject inker;
    public GameObject dripper;

    public TextHandler text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(waveHandler());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator waveHandler()
    {
        yield return new WaitForSeconds(0.5f);
        text.Say("I hate this.");

        for (int i = 0; i < 6; i++)
        {
            Instantiate(dripper, player.position + new Vector3(Random.Range(5, 20), Random.Range(5, 20), 0), player.rotation);
            Instantiate(inker, player.position + new Vector3(Random.Range(5, 20), Random.Range(5, 20), 0), player.rotation);
            Instantiate(dripper, player.position + new Vector3(Random.Range(5, 20), Random.Range(5, 20), 0), player.rotation);
            Instantiate(inker, player.position + new Vector3(Random.Range(5, 20), Random.Range(5, 20), 0), player.rotation);

            yield return new WaitForSeconds(20);
        }

        text.Say("I think that was the last of them... I guess I can go back east now.");
    }
}
