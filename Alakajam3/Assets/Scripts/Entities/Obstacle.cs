using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    [SerializeField]
    private ScriptableObstacle obstacleValues;
    [SerializeField]
    private Vector3 originalScale;
    [SerializeField]
    private ParticleSystem particleSystem;

    private Vector3 relativePosition;
    private Quaternion relativeRotation;
    private Collider collider;

    private void Reset()
    {
        originalScale = transform.localScale;
    }

    // Use this for initialization
    void Start()
    {
        collider = GetComponent<Collider>();
        if(particleSystem != null)
        {
            particleSystem.Stop();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDrawGizmos()
    {
        if (obstacleValues != null)
        {
            transform.localScale = originalScale * obstacleValues.scale;
        }
    }

    public float GetRequiredScale()
    {
        return obstacleValues.requiredScale;
    }

    public void Destroy()
    {
        if (particleSystem != null)
        {
            particleSystem.Play();
        }
        Destroy(gameObject);
    }

    public bool IsCollectable()
    {
        return obstacleValues.collectable;
    }

    public bool IsCow()
    {
        return obstacleValues.cow;
    }

    public Quaternion GetRelativeRotation()
    {
        return relativeRotation;
    }

    public Vector3 GetRelativePosition()
    {
        return relativePosition;
    }

    public void SetColliderActive(bool active)
    {
        collider.enabled = active;
    }

}
