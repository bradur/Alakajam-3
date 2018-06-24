using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleTrigger : MonoBehaviour
{
    Obstacle parentObstacle;

    [SerializeField]
    private GameObject startThisObjectAfterCollision;

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

    // ends level if not null
    public void CollideWithPlayer()
    {
        if (startThisObjectAfterCollision != null)
        {
            SoundManager.main.PlaySound(SoundType.TightCrash);
            startThisObjectAfterCollision.SetActive(true);
        }
    }
}
