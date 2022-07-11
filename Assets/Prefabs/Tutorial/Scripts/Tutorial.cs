using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Tutorial : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] private AudioClip confirmationClip;
    
    private Guide guide;
    Waypoint[] _waypoints;
    [FormerlySerializedAs("currentWaypoint")] [SerializeField] int currentWaypointIdx = 0;

    Waypoint CurrentWaypoint
    {
        get
        {
            return _waypoints[currentWaypointIdx];
        }
    }

    public static Tutorial instance;
    
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        _waypoints = GetComponentsInChildren<Waypoint>();
        guide = GetComponentInChildren<Guide>();
        _audioSource = GetComponent<AudioSource>();
    }

    public void NextWaypoint()
    {
        _audioSource.PlayOneShot(confirmationClip);
        Debug.Log("Next waypoint");
        currentWaypointIdx = (currentWaypointIdx + 1) % _waypoints.Length;
        guide.target = CurrentWaypoint.transform;
    }

    public void PreviousWaypoint()
    {
        currentWaypointIdx = Math.Max(currentWaypointIdx - 1, 0);
        guide.target = CurrentWaypoint.transform;
    }

    public void RepeatWaypoint()
    {
        CurrentWaypoint.PlayWaypoint();
    }
    
    public void OnGuideReachedTarget()
    {
        CurrentWaypoint.PlayWaypoint();
    }
    
}
