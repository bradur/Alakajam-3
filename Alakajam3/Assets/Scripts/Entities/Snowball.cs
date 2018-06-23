using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Snowball : MonoBehaviour
{
    [SerializeField]
    private ParticleSystemDestroy explosionParticle;
    [SerializeField]
    private Material snowballMaterial;
    [SerializeField]
    private Transform childBall;
    [SerializeField]
    private ScriptableScale scales;

    private bool exploded = false;

    [SerializeField]
    private float scale = 0.1f;
    [SerializeField]
    private bool grounded = false;
    [SerializeField]
    private float growthRate = 0.1f;
    [SerializeField]
    private float speedCoef = 0.1f;
    [SerializeField]
    [Tooltip("Percentage of obstacle scale at which the ball explodes.")]
    private float destroyExplodeMargin = 0.9f;

    private Rigidbody rbody;

    [SerializeField]
    private MeshRenderer meshRenderer;

    private Cinemachine.CinemachineTransposer cameraTransposer;

    [SerializeField]
    private Cinemachine.CinemachineVirtualCamera virtualCamera;

    private SphereCollider ballCollider;

    [SerializeField]
    private LayerMask snowLayer;

    // Use this for initialization
    void Start()
    {
        cameraTransposer = virtualCamera.GetCinemachineComponent<Cinemachine.CinemachineTransposer>();
        ballCollider = GetComponent<SphereCollider>();
        rbody = GetComponent<Rigidbody>();

        snowballMaterial = meshRenderer.sharedMaterials[0];
    }

    public bool IsGrounded()
    {
        //RaycastHit hit;
        //return Physics.Raycast(transform.position, Vector3.down, out hit, transform.localScale.y + 0.1f);
        return Physics.CheckSphere(
            transform.position,
            ballCollider.radius * transform.localScale.y + 0.5f,
            snowLayer
        );
        //return Physics.SphereCast(transform.position, ballCollider.radius, Vector3.down, out hit, transform.localScale.y + 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        float speed = rbody.velocity.magnitude;
        //Only scale collider radius of _this_ object and scale the child that holds the mesh 
        ballCollider.radius = scale;
        this.childBall.localScale = Vector3.one * scale;

        snowballMaterial.mainTextureScale = new Vector2(scale, scale);

        grounded = IsGrounded();
        int playerLayer = 10;
        int layerMask = ~(1 << playerLayer); // When checking for ground type, ignore player itself just in case

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 100f, layerMask))
        {
            if(hit.collider.gameObject.tag == "Snow")
            {
                if (grounded)
                {
                    //TODO: scale according to the material below
                    scale = scale + growthRate * (speed * speedCoef) * Time.deltaTime;

                    // distance of camera (z distance) gets bigger when scale gets bigger
                    cameraTransposer.m_FollowOffset.z = -(5f + (scale / 0.25f));

                    // angle of camera (y distance)
                    cameraTransposer.m_FollowOffset.y = (5f + (scale / 0.25f));
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.otherCollider.tag == "Obstacle" && !exploded)
            {
                Obstacle obstacle = contact.otherCollider.gameObject.GetComponent<Obstacle>();
                if (obstacle != null)
                {
                    if (scale > destroyExplodeMargin * obstacle.GetRequiredScale())
                    {
                        //eep, nothing happens
                    }
                    else
                    {
                        ParticleSystemDestroy explosion = Instantiate<ParticleSystemDestroy>(explosionParticle);
                        explosion.SetParticleSize(scale * 0.4f);
                        explosion.transform.position = transform.position;
                        explosion.gameObject.SetActive(true);
                        exploded = true;
                        Destroy(gameObject);
                    }
                }
                else
                {
                    Debug.Log("Obstacle was null, but obstacle collision was recorded");
                }
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle" && !exploded)
        {
            ObstacleTrigger trigger = other.gameObject.GetComponent<ObstacleTrigger>();
            if (trigger != null)
            {
                Obstacle obstacle = trigger.GetParent();
                
                if (!obstacle.IsCollectable())
                {
                    obstacle.Destroy();
                }
                else
                {
                    obstacle.transform.parent = transform;
                    obstacle.SetColliderActive(false);

                    string layerName = LayerMask.LayerToName(obstacle.gameObject.layer);
                    StringFloat x = scales.growAmounts.Where(s => s.key == layerName).SingleOrDefault();
                    if (x != null)
                    {
                        scale += x.value;
                    }
                }
            }
        }
    }
}
