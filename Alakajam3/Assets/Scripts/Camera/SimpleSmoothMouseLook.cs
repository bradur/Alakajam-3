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

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        snowball = GetComponent<Snowball>();
        // Set target direction for the character body to its inital state.
        rb = GetComponent<Rigidbody>();
        targetCharacterDirection = rb.transform.localRotation.eulerAngles;
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
            if (mDelta.x > 0)
            {
                force = transform.right * speed * mDelta.x;
                rb.AddForce(force, ForceMode.Acceleration);
            }
            else
            {
                force = transform.right * speed * mDelta.x;
                rb.AddForce(force, ForceMode.Acceleration);
            }
            // limit max velocity
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocityMagnitude);
        }
    }
    
}