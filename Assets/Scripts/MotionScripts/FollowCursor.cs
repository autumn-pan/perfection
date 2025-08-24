using UnityEngine;

public class FollowCursor : MonoBehaviour
{
    public float rotationSpeed = 5f;
    private Vector2 dir;
    public bool rotationLock = false;
    public int zPos = 0;
    void Update()
    {
        if (!rotationLock)
        {
             dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        Vector2 cameraPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(cameraPos.x, cameraPos.y, zPos), 5f * Time.deltaTime);
    }
}
