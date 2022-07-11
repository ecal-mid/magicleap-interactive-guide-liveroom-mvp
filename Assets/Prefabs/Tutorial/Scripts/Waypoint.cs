using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    private AudioSource audioSource;
    
    void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Gizmos.DrawCube(transform.position, new Vector3(0.3f, 0.3f, 0.3f));
    }
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    public void PlayWaypoint()
    {
        audioSource.Play();
    }

    public void StopWaypoint()
    {
        audioSource.Stop();
    } 
    
}
