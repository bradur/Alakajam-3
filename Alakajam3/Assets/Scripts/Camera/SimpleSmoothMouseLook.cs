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


    [SerializeField]
    private float minTorque = -5f;
    [SerializeField]
    private float maxTorque = 5f;

    private float torque = 0f;
    // Assign this if there's a parent object controlling motion, such as a Character Controller.
    // Yaw rotation will affect this object instead of the camera if set.
    private Rigidbody rb;

    [SerializeField]
    private float maxVelocityMagnitude = 6;

    [SerializeField]
    private Transform rotationFollower;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        // Set target direction to the camera's initial orientation.
        //targetDirection = transform.localRotation.eulerAngles;

        // Set target direction for the character body to its inital state.
        rb = GetComponent<Rigidbody>();
        //targetCharacterDirection = rb.transform.localRotation.eulerAngles;

    }


    void LateUpdate()
    {
        Cursor.lockState = CursorLockMode.Locked;
        // Ensure the cursor is always locked when set
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        // Allow the script to clamp based on a desired target value.
        var targetOrientation = Quaternion.Euler(targetDirection);
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

        //transform.localRotation = Quaternion.AngleAxis(-_mouseAbsolute.y, targetOrientation * Vector3.right) * targetOrientation;
        //Quaternion rot = Quaternion.AngleAxis(-_mouseAbsolute.y, targetOrientation * Vector3.right) * targetOrientation;
        //rotationFollower.eulerAngles = -rb.velocity;
        //Debug.Log(_mouseAbsolute.x);
        var yRotation = Quaternion.AngleAxis(_mouseAbsolute.x, Vector3.up);
        rotationFollower.transform.localRotation = yRotation * targetCharacterOrientation;
        Debug.Log(_mouseAbsolute.x);
        if (Mathf.Abs(_mouseAbsolute.x) > 8f)
        {
            //torque = Mathf.Clamp(torque + _mouseAbsolute.x * speed, minTorque, maxTorque);

            if (_mouseAbsolute.x > 0)
            {
                rb.AddForce(Vector3.right * speed, ForceMode.Acceleration);
            }
            else
            {
                rb.AddForce(Vector3.left * speed, ForceMode.Acceleration);
            }
            Debug.Log(rb.velocity.magnitude);
            /*if (rb.velocity.magnitude > maxVelocityMagnitude)
            {

                Vector3 newVelocity = rb.velocity.normalized;
                newVelocity *= maxVelocityMagnitude;
                rb.velocity = newVelocity;
            }*/
            /*Debug.Log(rotationFollower.transform.localRotation.y);
            torque = Mathf.Clamp(rotationFollower.transform.localRotation.y * 20f, minTorque, maxTorque);
            if (Mathf.Abs(rotationFollower.transform.localRotation.y) > 0.05f)
            {
                rb.AddTorque(0, torque, 0, ForceMode.Force);
            }*/
        }
        /*if (rb != null && Mathf.Abs(_mouseAbsolute.x) > 0.05f)
        {*/
        /*torque += mouseDelta.x * speed;
        torque = Mathf.Clamp(torque, minTorque, maxTorque);*/
        /*torque = Mathf.Clamp(-(rotationFollower.transform.eulerAngles.y - clampInDegrees.x), minTorque, maxTorque);
        Debug.Log(torque);*/
        //torque = -rotationFollower.transform.eulerAngles.y;
        //rb.AddTorque(0, torque, 0, ForceMode.Force);
        //}
        //rotationFollower.transform.localRotation = Quaternion.Euler(0, rb.transform.eulerAngles.y, 0);

        //rb.transform.eulerAngles = new Vector3(rb.transform.eulerAngles.x, rb.transform.eulerAngles.y, 0f);
    }
    
}