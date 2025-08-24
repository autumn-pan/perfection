using System.Collections;
using UnityEngine;

public class PressureHandler : MonoBehaviour
{
    public float pressure = 50;
    public float maxPressure = 100; // Player has a breakdown after 100
    public float lowestPressure = 50; // Decreases as player starts to heal
    public float pressureDifficultyScale;
    public GameObject dripper;
    public GameObject inker;
    public Transform player;

    public AudioSource heartbeat;
    public bool canBeat;
    public float heartbeatCooldown;
    public float maxHeartbeatCooldown = 0.6f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public float interval;
    enum MonsterDifficultyScale
    {
        Inker = 1,
        Dripper = 3,
        Rebar = 5,
        Silkworm = 7

    }

    void Start()
    {
        StartCoroutine(pressureSpawner());
        heartbeatCooldown = maxHeartbeatCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        interval = (100 - Mathf.Sqrt(pressure * pressure + 2 * pressure)) / 3;
        if (interval < 5)
        {
            interval = 5;
        }
        
        if (!canBeat)
            heartbeatCooldown -= Time.deltaTime;
        if (heartbeatCooldown <= 0 && pressure >= 75)
        {
            heartbeat.Play();
            heartbeatCooldown = maxHeartbeatCooldown;
            canBeat = false;
        }

    }

    public void AddPressure(float num)
    {
        pressure += num;
        if (pressure > maxPressure)
            pressure = maxPressure;
    }

    public void subtractPressure(float num)
    {
        pressure -= num;
        if (pressure < lowestPressure)
            pressure = lowestPressure;
    }

    IEnumerator pressureSpawner()
    {
        while (true)
        {
             Instantiate(dripper, player.position + new Vector3(Random.Range(5, 20), Random.Range(5, 20), 0), player.rotation);
            Instantiate(inker, player.position + new Vector3(Random.Range(5, 20), Random.Range(5, 20), 0), player.rotation);
            if (pressure >= 80)
            {
                Instantiate(dripper, player.position + new Vector3(Random.Range(5, 20), Random.Range(5, 20), 0), player.rotation);
            }
            if (pressure >= 95)
            {
                Instantiate(inker, player.position + new Vector3(Random.Range(5, 20), Random.Range(5, 20), 0), player.rotation);
            }

            yield return new WaitForSeconds(interval);       
        }

    }
}

