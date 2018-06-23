using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

    [SerializeField]
    private ScriptableObstacle obstacleValues;

    // Use this for initialization
    void Start () {
        Debug.Log(obstacleValues.requiredScale);
    }
    
    // Update is called once per frame
    void Update () {
        
    }
}
