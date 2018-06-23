using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowball : MonoBehaviour
{
    [SerializeField]
    private ParticleSystemDestroy explosionParticle;
    [SerializeField]
    private Material snowballMaterial;

    private bool exploded = false;

    [SerializeField]
    private float scale = 0.1f;
    [SerializeField]
    private bool grounded = false;
    [SerializeField]
    private float growthRate = 0.1f;
    [SerializeField]
    private float speedCoef = 0.1f;
    private Rigidbody rbody;

    // Use this for initialization
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        snowballMaterial = renderer.sharedMaterials[0];
    }

    // Update is called once per frame
    void Update()
    {
        float speed = rbody.velocity.magnitude;
        this.transform.localScale = Vector3.one * scale;
        if (grounded)
        {
            //TODO: scale according to the material below
            scale = scale + growthRate * (speed * speedCoef) * Time.deltaTime;
        }

        snowballMaterial.mainTextureScale = new Vector2(scale, scale);
    }

    private void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.otherCollider.tag == "Ground")
            {
                grounded = true;
            }
            if (contact.otherCollider.tag == "Wall" && !exploded)
            {
                ParticleSystemDestroy explosion = Instantiate<ParticleSystemDestroy>(explosionParticle);
                explosion.SetParticleSize(scale * 0.4f);
                explosion.transform.position = transform.position;
                explosion.gameObject.SetActive(true);
                exploded = true;
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.otherCollider.tag == "Ground")
            {
                grounded = false;
            }
        }
    }
}
