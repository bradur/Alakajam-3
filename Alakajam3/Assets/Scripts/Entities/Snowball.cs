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
    private SphereCollider collider;

    // Use this for initialization
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        snowballMaterial = renderer.sharedMaterials[0];
        collider = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        float speed = rbody.velocity.magnitude;
        //Only scale collider radius of _this_ object and scale the child that holds the mesh 
        collider.radius = scale;
        this.childBall.localScale = Vector3.one * scale;

        snowballMaterial.mainTextureScale = new Vector2(scale, scale);

        RaycastHit hit;
        int playerLayer = 10;
        int layerMask = ~(1 << playerLayer); // When checking for ground type, ignore player itself just in case

        if (Physics.Raycast(transform.position, Vector3.down, out hit, transform.localScale.y + 0.1f))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }

        if (Physics.Raycast(transform.position, Vector3.down, out hit, 100f, layerMask))
        {
            if (hit.collider.gameObject.tag == "Snow")
            {
                if (grounded)
                {
                    //TODO: scale according to the material below
                    scale = scale + growthRate * (speed * speedCoef) * Time.deltaTime;
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
                    Debug.Log(scale + ", " + obstacle.GetRequiredScale() + ", " + destroyExplodeMargin * obstacle.GetRequiredScale());
                    /*if (scale > obstacle.GetRequiredScale())
                    {
                        if (!obstacle.IsCollectable())
                        {
                            obstacle.Destroy();
                        }
                        else
                        {
                            obstacle.transform.parent = transform;
                            obstacle.SetColliderActive(false);
                        }
                    }
                    else */
                    if (scale > destroyExplodeMargin * obstacle.GetRequiredScale())
                    {
                        //eep, nothing happens
                        Debug.Log("eeps");
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
                    Debug.Log("ksdlktöjsfötgyj");
                }
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle" && !exploded)
        {
            Debug.Log("moi");
            ObstacleTrigger trigger = other.gameObject.GetComponent<ObstacleTrigger>();
            if (trigger != null)
            {
                Obstacle obstacle = trigger.GetParent();
                Debug.Log("moimoi");
                if (!obstacle.IsCollectable())
                {
                    obstacle.Destroy();
                }
                else
                {
                    Debug.Log("moimoimoi");
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
