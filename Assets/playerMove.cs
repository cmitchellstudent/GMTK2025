
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerMove : MonoBehaviour
{
    public GameObject player;
    public GameObject lookAt;
    InputAction moveActions;
    InputAction lookActions;
    public float camSpeed;
    public float moveSpeed;

    private float targetPitch;
    private float rotateVelocity;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        moveActions = InputSystem.actions.FindAction("Move");
        lookActions = InputSystem.actions.FindAction("Look");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveInput = moveActions.ReadValue<Vector2>();
        var forward = player.transform.forward;
        var left = Vector3.Cross(Vector3.up, forward);
        var position = player.transform.position;
        position += new Vector3(forward.x, 0, forward.z) * (moveInput.y * (Time.deltaTime * moveSpeed));
        position += new Vector3(left.x, 0, left.z) * (moveInput.x * (Time.deltaTime * moveSpeed));
        player.transform.position = position;
    }

    private void LateUpdate()
    {
        CameraRotation();
    }

    private void CameraRotation()
    {
        //TODO: MAKE THIS BETTER/CONTROLLER COMPATIBLE
        Vector2 lookInput = lookActions.ReadValue<Vector2>();
        Debug.Log(lookInput);
        if (lookInput.sqrMagnitude > 0)
        {
            //targetPitch += lookInput.y * camSpeed;
            rotateVelocity = lookInput.x * camSpeed;
            //targetPitch = ClampAngle(targetPitch, -90, 90);
            //transform.rotation = Quaternion.Euler(targetPitch,0 ,0);
            transform.Rotate(Vector3.up * (rotateVelocity * 5f));

            if (Math.Abs(lookInput.y) > 1f){
                Vector3 lookAtposition = lookAt.transform.position;
                lookAtposition.y += lookInput.y * camSpeed;
                lookAtposition.y = Mathf.Clamp(lookAtposition.y, -15f, 15f);
                lookAt.transform.position = lookAtposition;
            }
        }
    }
    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }
    /*
    void OnDrawGizmos()
    {
        Vector3 direction = Vector3.forward;
        Gizmos.color = Color.red;

        Vector3 start = transform.position;
        Vector3 end = start + direction.normalized * 3f;

        // Draw main line (shaft)
        Gizmos.DrawLine(start, end);
        // Draw arrowhead
        Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 150, 0) * Vector3.forward;
        Vector3 left  = Quaternion.LookRotation(direction) * Quaternion.Euler(0, -150, 0) * Vector3.forward;
        float headLength = 3f * 0.25f;
        Gizmos.DrawLine(end, end + right * headLength);
        Gizmos.DrawLine(end, end + left * headLength);
    }
    */
}
