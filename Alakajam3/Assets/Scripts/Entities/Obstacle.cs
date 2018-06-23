using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    [SerializeField]
    private ScriptableObstacle obstacleValues;
    [SerializeField]
    private Vector3 originalScale;

    private void Reset()
    {
        originalScale = transform.localScale;
    }

    // Use this for initialization
    void Start()
    {
        Debug.Log(obstacleValues.requiredScale);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDrawGizmos()
    {
        transform.localScale = originalScale * obstacleValues.scale;
    }

    public float GetRequiredScale()
    {
        return obstacleValues.requiredScale;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
