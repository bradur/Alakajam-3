using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerDownHill : MonoBehaviour
{

    private Rigidbody rb;

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

    }
}
