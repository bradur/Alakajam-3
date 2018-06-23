using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowballParent : MonoBehaviour
{
    [SerializeField]
    private Snowball snowball;
    private List<Obstacle> collectedObstacles;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        foreach(Obstacle ob in collectedObstacles)
        {
            //ob.transform.localRotation = ob.GetRelativeRotation() + snowball.transform.localRotation;
        }
    }

    public void AddObstacle(Obstacle item)
    {
        collectedObstacles.Add(item);
        item.transform.parent = transform;
        
    }
}
