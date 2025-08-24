using System.Collections;
using UnityEngine;

public class MushroomHandler : MonoBehaviour
{
    public SpriteRenderer sprite;
    public Sprite[] sprites = new Sprite[5];
    public bool isBouncing = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Bounce());
    }
    IEnumerator Bounce()
    {
        if (!isBouncing)
        {
            isBouncing = true;
            for (int i = 0; i < sprites.Length; i++)
            {
                sprite.sprite = sprites[i];
                yield return new WaitForSeconds(0.05f);
            }
            isBouncing = false;
        }

    }
}
