using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnHarbinger : MonoBehaviour
{
    public float seconds = 30;
    public GameObject harbinger;
    public PlayerDamageController damager;
    public PressureHandler pressureController;
    public bool triggered = false;
    public EnemySpawnScript[] spawner;

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
        if (Time.timeSinceLevelLoad > 0.1 && !triggered)
            StartCoroutine(Spawn());
    }
    IEnumerator Spawn()
    {
        damager.maxHealth = 1000;
        damager.health = 1000;
        pressureController.lowestPressure = 100;
        pressureController.pressure = 100;
        triggered = true;
        yield return new WaitForSeconds(seconds);
        Instantiate(harbinger, transform.position, Quaternion.Euler(0, 0, 270));

        while (true)
        {
            for (int i = 0; i < spawner.Length; i++)
            {
                spawner[i].Spawn();
            }
            if (damager.health < 100)
            {   
                int index = SceneManager.GetActiveScene().buildIndex;
                SceneManager.LoadScene(index + 1);
            }
            yield return new WaitForSeconds(15);
        }

    }
}
