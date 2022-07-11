using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guide : MonoBehaviour
{
    // Go to position and notify
    public Transform target;
    [SerializeField] private float speed;
    [SerializeField] float distDelta = 0.1f;

    private float currentTargetDistance = 0f;
    private float previousTargetDistance = 0f;
    
    void Update()
    {
        if (target == null) return;
        currentTargetDistance = Vector3.Distance(transform.position, target.position);
        
        float maxDistDelta = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, maxDistDelta);

        if (currentTargetDistance < distDelta && previousTargetDistance >= distDelta)
        {
            // Activate
            Tutorial.instance.OnGuideReachedTarget();
        }
        
        previousTargetDistance = currentTargetDistance;
    }
}
