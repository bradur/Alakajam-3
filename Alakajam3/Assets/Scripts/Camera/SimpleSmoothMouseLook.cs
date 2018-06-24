using UnityEngine;

[AddComponentMenu("Camera/Simple Smooth Mouse Look ")]
public class SimpleSmoothMouseLook : MonoBehaviour
{
    Vector2 _mouseAbsolute;
    Vector2 _smoothMouse;

    public Vector2 clampInDegrees = new Vector2(360, 180);
    public bool lockCursor;
    public Vector2 sensitivity = new Vector2(2, 2);
    public Vector2 smoothing = new Vector2(3, 3);
    public Vector2 targetDirection;
    public Vector2 targetCharacterDirection;
    [SerializeField]
    private float speed = 50;

    // Assign this if there's a parent object controlling motion, such as a Character Controller.
    // Yaw rotation will affect this object instead of the camera if set.
    private Rigidbody rb;

    [SerializeField]
    private float maxVelocityMagnitude = 6;

    [SerializeField]
    private Transform rotationFollower;
    private Snowball snowball;

    private Vector3 forward = new Vector3(0, 0, 1);
    private float mousex = 0.0f;

    private Vector3 desiredDirection = new Vector3(0, 0, 0);

    private Vector3 rotateAxis = Vector3.right;
    private float rotateSpeed = 0.0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        snowball = GetComponent<Snowball>();
        // Set target direction for the character body to its inital state.
        rb = GetComponent<Rigidbody>();
        targetCharacterDirection = rb.transform.localRotation.eulerAngles;
    }

    void Update()
    {
        mousex = mousex * (1.0f - Time.deltaTime*0.5f);
        desiredDirection = desiredDirection * (1.0f - Time.deltaTime * 0.5f);

        if (snowball.IsGrounded())
        {
            mousex += Input.GetAxisRaw("Mouse X") * 0.5f;

            Vector3 fwd = Camera.main.transform.forward;
            Vector3 right = Camera.main.transform.right;
            fwd = new Vector3(fwd.x, 0, fwd.z);
            right = new Vector3(right.x, 0, right.z);
            desiredDirection = desiredDirection + fwd*Input.GetAxisRaw("Mouse Y")*0.25f + right*Input.GetAxisRaw("Mouse X") * 0.25f;

            rotateAxis = Vector3.Cross(snowball.contactNormal, rb.velocity);
            rotateSpeed = rb.velocity.magnitude / (Mathf.PI * snowball.getRadius()) * 180;
        }

        if (mousex < -1.0) mousex = -1.0f;
        if (mousex > 1.0) mousex = 1.0f;
        if (desiredDirection.magnitude > 1.0f)
        {
            desiredDirection = desiredDirection.normalized;
        }
        
        transform.RotateAround(transform.position, rotateAxis, Time.deltaTime * rotateSpeed);
    }


    void FixedUpdate()
    {
        Cursor.lockState = CursorLockMode.Locked;
        // Ensure the cursor is always locked when set
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        // Allow the script to clamp based on a desired target value.
        var targetCharacterOrientation = Quaternion.Euler(targetCharacterDirection);

        // Get raw mouse input for a cleaner reading on more sensitive mice.
        var mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        mouseDelta = Vector2.Scale(mouseDelta, new Vector2(sensitivity.x * smoothing.x, sensitivity.y * smoothing.y));

        // Interpolate mouse movement over time to apply smoothing delta.
        _smoothMouse.x = Mathf.Lerp(_smoothMouse.x, mouseDelta.x, 1f / smoothing.x);
        _smoothMouse.y = Mathf.Lerp(_smoothMouse.y, mouseDelta.y, 1f / smoothing.y);

        // Find the absolute mouse movement value from point zero.
        _mouseAbsolute += _smoothMouse;

        // Clamp and apply the local x value first, so as not to be affected by world transforms.
        if (clampInDegrees.x < 360)
            _mouseAbsolute.x = Mathf.Clamp(_mouseAbsolute.x, -clampInDegrees.x * 0.5f, clampInDegrees.x * 0.5f);

        // Then clamp and apply the global y value.
        if (clampInDegrees.y < 360)
            _mouseAbsolute.y = Mathf.Clamp(_mouseAbsolute.y, -clampInDegrees.y * 0.5f, clampInDegrees.y * 0.5f);

        // show wanted rotation (arrow)
        if (rotationFollower != null)
        {
            var yRotation = Quaternion.AngleAxis(_mouseAbsolute.x, Vector3.up);
            rotationFollower.transform.localRotation = yRotation * targetCharacterOrientation;
        }
        // dont turn ball if mouse isnt moving
        //Debug.Log(snowball.IsGrounded());
        Vector2 mDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        if (snowball.IsGrounded() && Mathf.Abs(mDelta.x) > 0.02f)
        {
            Vector3 force;
            var right = Vector3.Cross(Vector3.up, rb.velocity);
            if (mDelta.x > 0)
            {
                force = right * speed * mDelta.x;
                //rb.AddForce(force, ForceMode.Acceleration);
            }
            else
            {
                force = right * speed * mDelta.x;
                //rb.AddForce(force, ForceMode.Acceleration);
            }
            //rb.velocity = (rb.velocity.normalized + forward.normalized).normalized;
        }

        if (snowball.IsGrounded())
        {
            var angle = Vector3.Angle(snowball.contactNormal, snowball.lastContactNormal);
            if (angle > 0.1f && angle < 90f)
            {
                //var vel_right = Quaternion.Euler(0, -90, 0) * rb.velocity.normalized;
                //rb.velocity = rb.velocity.magnitude * Vector3.Cross(snowball.contactNormal, vel_right).normalized;
                //rb.AddForce(snowball.contactNormal.normalized * -1.0f);
            }
            //rb.velocity = Quaternion.Euler(0, mousex * 2.0f, 0) * rb.velocity;
            Vector3 dir = new Vector3(desiredDirection.normalized.x, rb.velocity.normalized.y, desiredDirection.normalized.z);

            Debug.DrawLine(transform.position, transform.position + dir * 10, Color.red);
            rb.velocity = Vector3.RotateTowards(rb.velocity, dir, 2 * Time.fixedDeltaTime, 0.0f);
        }

        // limit max velocity
        //rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocityMagnitude);
        //rb.AddForce(Vector3.down * 10.0f);
        
    }

}