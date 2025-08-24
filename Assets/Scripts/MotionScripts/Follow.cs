using UnityEngine;

public class Follow : MonoBehaviour
{
    public float rotationSpeed = 5f;
    public Transform target;
    private Vector2 dir;
    public bool followPlayer = false; 
    public float speed = 3.8f;

    public float yOffset = 0;
    void Awake()
    {
        if (followPlayer)
        {
            target = GameObject.Find("Player").transform;
        }
    }

    void Update()
    {
        dir = new Vector3(target.position.x, target.position.y + yOffset, target.position.z) - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);


        transform.position = Vector2.MoveTowards(transform.position, new Vector3(target.position.x, target.position.y + yOffset, target.position.z), speed * Time.deltaTime);
    }
}
