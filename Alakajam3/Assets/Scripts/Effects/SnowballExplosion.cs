using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowballExplosion : MonoBehaviour
{
    [SerializeField]
    private GameObject explosionParticle;
    private bool exploded = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.otherCollider.tag == "Wall" && !exploded)
            {
                Debug.Log("jou");
                GameObject explosion = Instantiate<GameObject>(explosionParticle);
                explosion.transform.position = transform.position;
                explosion.gameObject.SetActive(true);
                exploded = true;
                Destroy(gameObject);
            }
        }
    }
}
