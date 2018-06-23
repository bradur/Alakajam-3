using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTargetPosition : MonoBehaviour
{

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
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.localPosition;
        Vector3 targetPosition = target.transform.localPosition;
        if (!local)
        {
            position = transform.position;
            targetPosition = target.transform.position;
        }
        if (followX)
        {
            position.x = targetPosition.x;
        }
        if (followY)
        {
            position.y = targetPosition.y;
        }
        if (followZ)
        {
            position.z = targetPosition.z;
        }
        if (local)
        {
            transform.localPosition = position;
        }
        else
        {
            transform.position = position;
        }
    }
}
