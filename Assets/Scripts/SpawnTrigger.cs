using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    public EnemySpawnScript[] spawner;
    private bool fired = false;
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
        if (Time.timeSinceLevelLoad < 3)
            return;
            
        if (fired)
            return;

        for (int i = 0; i < spawner.Length; i++)
        {
            spawner[i].Spawn();
        }
        fired = true;
    }
}
