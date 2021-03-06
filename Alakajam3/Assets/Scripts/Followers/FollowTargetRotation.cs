using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTargetRotation : MonoBehaviour {

    [SerializeField]
    private Transform target;

    [SerializeField]
    private bool local = true;


    [SerializeField]
    private bool followX = true;
    [SerializeField]
    private bool followY = true;
    [SerializeField]
    private bool followZ = true;


    // Use this for initialization
    void Start () {
        
    }
    
    // Update is called once per frame
    void Update () {
        Vector3 rotation = transform.localEulerAngles;
        Vector3 targetRotation = target.transform.localEulerAngles;
        if (!local)
        {
            rotation = transform.eulerAngles;
            targetRotation = target.transform.eulerAngles;
        }
        if (followX)
        {
            rotation.x = target.transform.eulerAngles.x;
        }
        if (followY)
        {
            rotation.y = target.transform.eulerAngles.y;
        }
        if (followZ)
        {
            rotation.z = target.transform.eulerAngles.z;
        }
        if (local)
        {
            transform.localEulerAngles = rotation;
        }
        else
        {
            transform.eulerAngles = rotation;
        }
    }
}
