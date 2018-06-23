using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleTrigger : MonoBehaviour
{
    Obstacle parentObstacle;

    // Use this for initialization
    void Start()
    {
        parentObstacle = transform.parent.gameObject.GetComponent<Obstacle>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Obstacle GetParent()
    {
        return parentObstacle;
    }
}
