using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;
using MagicLeap.Core.StarterKit;

public enum Direction {
    Left,
    Right
}

public class MLHandControl : MonoBehaviour
{
    
    
    
    [SerializeField] float confidenceTreshold = 0.9f;
    private bool rightThumbUp = false;
    private bool pRightThumbUp = false;
    
    private bool leftThumbUp = false;
    private bool pLeftThumbUp = false;

    [SerializeField] private float waitTime = 2f;
    private Coroutine triggerCoroutine;
    
    /*
        private MLHandKeyPose[] gestures; // Holds the different hand poses we will look for
    */
    
    private void Update()
    {
#if PLATFORM_LUMIN

        HandleRightHand();
        HandleLeftHand();

#endif
    }
    
    
    private void HandleLeftHand()
    {
        var leftHand = MLHandTracking.Left;
        if (leftHand != null)
        {
            leftThumbUp = leftHand.KeyPose == MLHandTracking.HandKeyPose.Thumb &&
                           leftHand.HandKeyPoseConfidence > confidenceTreshold;
        }
        else
        {
            leftThumbUp = false;
        }

        if (!pLeftThumbUp && leftThumbUp)
        {
            OnThumbUp(Direction.Left);
        }

        if (pLeftThumbUp && !leftThumbUp)
        {
            OnThumbUpStopped(Direction.Right);
        }

        pLeftThumbUp = leftThumbUp;
    }

    private void HandleRightHand()
    {
        var rightHand = MLHandTracking.Right;
        if (rightHand != null)
        {
            /*
            Debug.Log("Key pose " + rightHand.KeyPose);
            Debug.Log("confidence");
            Debug.Log(rightHand.HandKeyPoseConfidence);
            Debug.Log("----------");*/

            rightThumbUp = rightHand.KeyPose == MLHandTracking.HandKeyPose.Thumb &&
                           rightHand.HandKeyPoseConfidence > confidenceTreshold;
        }
        else
        {
            rightThumbUp = false;
        }

        if (!pRightThumbUp && rightThumbUp)
        {
            OnThumbUp(Direction.Right);
        }

        if (pRightThumbUp && !rightThumbUp)
        {
            OnThumbUpStopped(Direction.Right);
        }

        pRightThumbUp = rightThumbUp;
    }

    void OnThumbUp(Direction dir)
    {
        Debug.Log("On Thumb Up");
        triggerCoroutine = StartCoroutine(_TriggerAction());
    }
    
    void OnThumbUpStopped(Direction dir)
    {
        Debug.Log("On stopped thumb up");
        if (triggerCoroutine != null)
        {
            StopCoroutine(triggerCoroutine);
            triggerCoroutine = null;
        }
    }

    IEnumerator _TriggerAction()
    {
        yield return new WaitForSeconds(waitTime);
        yield return null;
        Debug.Log("Triggered action");

        if (leftThumbUp && rightThumbUp)
        {
            Tutorial.instance.RepeatWaypoint();

        } else if (leftThumbUp)
        {
            Tutorial.instance.PreviousWaypoint();

        } else if (rightThumbUp)
        {
            Tutorial.instance.NextWaypoint();
        }

    }

    void Start()
    {
        /*
        new MLHandTracking.HandKeyPose[4];
        MLHandTracking.HandKeyPose
        */
        /*
        gestures = new MLHandTracking.HandKeyPose[4]; //Assign the gestures we will look for.
        gestures[0] = MLHandTracking.HandKeyPose.Ok;
        gestures[1] = MLHandTracking.HandKeyPose.Fist;
        gestures[2] = MLHandTracking.HandKeyPose.OpenHand;
        gestures[3] = MLHandTracking.HandKeyPose.Finger;
        */
        // Enable the hand poses.
        //MLHandTracking.KeyPoseManager.EnableKeyPoses(gestures, true, false);

    }

    void OnDestroy()
    {
        
    }
    
    }


