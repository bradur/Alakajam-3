using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerDownHill : MonoBehaviour
{

    private Rigidbody rb;

    private float timer = 0f;

    [SerializeField]
    private float turnSpeed = 10f;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        float horizontalAxis = Input.GetAxis("Horizontal");
        float margin = 0.01f;
        if (horizontalAxis > margin)
        {
            rb.AddTorque(0, 0, horizontalAxis * -turnSpeed, ForceMode.Force);
        }
        else if (horizontalAxis < -margin)
        {
            rb.AddTorque(0, 0, horizontalAxis * -turnSpeed, ForceMode.Force);
        }

    }

    private void LateUpdate()
    {
        //Debug.Log(rb.velocity.magnitude);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            
            Debug.Log(timer);
            Destroy(collision.gameObject);
        }
    }
}
