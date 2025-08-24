using UnityEngine;

public class VultureHeadHandler : MonoBehaviour
{
    public TentacleHandler tentacle;
    public int zPos;
    protected Vector3 vector;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        vector = tentacle.segments[tentacle.length - 1];
        transform.position = new Vector3(vector.x, vector.y, zPos);
        
    }
}
