using Unity.Mathematics;
using UnityEngine;

public class WingHandler : MonoBehaviour
{
    public int zPos = 0;
    public Transform reference;
    public float offsetX;
    public float offsetY;
    public float speed;
    public float range;
    private float angle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3
        (
            reference.position.x + offsetX,
            reference.position.y + offsetY + range * Mathf.Sin(Mathf.Rad2Deg * angle),
            zPos
        );
        
        angle += speed * Time.deltaTime;
    }
}
