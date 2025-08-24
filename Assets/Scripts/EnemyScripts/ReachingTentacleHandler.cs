using UnityEngine;
using UnityEngine.UIElements;



public class ReachingTentacleHandler : MonoBehaviour
{
    public float bounciness = 0.05f;
    public int length;
    public LineRenderer lineRenderer;
    public Vector3[] segments;
    public Transform target;
    public float dist;
    public float reachDistance = 0.2f;
    public Transform reachTarget;
    private Vector3[] maxVelocity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lineRenderer.positionCount = length;
        segments = new Vector3[length];
        maxVelocity = new Vector3[length];

    }

    // Update is called once per frame
    void Update()
    {
        // First, attach the tentacle to the base
        Vector3 basePosition = target.position;
        segments[0] = basePosition;

        // Move the tentacle segments
        for (int i = 1; i < length; i++)
        {
            segments[i] = Vector3.SmoothDamp(
                segments[i],
                segments[i - 1] + (target.right * dist),
                ref maxVelocity[i],
                bounciness
            );

            // Make the tentacle reach towards the target
            
            Vector3 reachDirection = (reachTarget.position - segments[i]).normalized;
            segments[i] += reachDirection * reachDistance;
            
        }


        lineRenderer.SetPositions(segments);
    } 
    

}
