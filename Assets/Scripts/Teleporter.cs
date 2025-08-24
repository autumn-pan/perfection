using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleporter : MonoBehaviour
{
    public float cooldown = 0;
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
        if (Time.timeSinceLevelLoad > 0.1)
        {
            StartCoroutine(tp());
        }
    }

    IEnumerator tp()
    {
        yield return new WaitForSeconds(cooldown);
        if (Time.timeSinceLevelLoad > 0.1)
        {
            int index = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(index + 1);
        }
    }

}
