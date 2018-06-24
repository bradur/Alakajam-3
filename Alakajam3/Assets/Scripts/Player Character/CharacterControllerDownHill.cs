using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerDownHill : MonoBehaviour
{

    private Rigidbody rb;

    [SerializeField]
    private float turnSpeed = 250f;

    float horizontalAxis, verticalAxis;

    private SphereCollider ballCollider;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = 100f;
        ballCollider = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalAxis = Input.GetAxis("Horizontal");
        verticalAxis = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {

        Vector3 fwd = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;
        fwd = new Vector3(fwd.x, 0, fwd.z);
        right = new Vector3(right.x, 0, right.z);
        rb.AddTorque(right * verticalAxis * turnSpeed * ballCollider.radius, ForceMode.Force);
        rb.AddTorque(-fwd * horizontalAxis * turnSpeed * ballCollider.radius, ForceMode.Force);
        //rb.AddTorque(0, 0, horizontalAxis * turnSpeed, ForceMode.Force);
    }

    private void LateUpdate()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
    }
}
