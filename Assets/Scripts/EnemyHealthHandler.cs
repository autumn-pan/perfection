using System.Collections;
using UnityEngine;

public class EnemyHealthHandler : MonoBehaviour
{
    public GameObject deathEffect;
    public float health;
    public Follow followScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (health < 0)
        {
            StartCoroutine(Die());
        }
    }

    public void Damage(float damage)
    {
        health -= damage;
    }

    IEnumerator Die()
    {
        if (followScript)
        {
            followScript.enabled = false;
        }
        
        deathEffect.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(2.5f);
        Destroy(gameObject);

    } 
}
