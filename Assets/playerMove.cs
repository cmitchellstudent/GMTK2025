
using UnityEngine;
using UnityEngine.InputSystem;

public class playerMove : MonoBehaviour
{
    public GameObject player;
    InputAction moveActions;
    InputAction lookActions;
    public int camSpeed;
    public int moveSpeed;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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

        Vector2 lookInput = lookActions.ReadValue<Vector2>();
        player.transform.Rotate(new Vector3(lookInput.y,lookInput.x,0) * (Time.deltaTime * camSpeed), Space.Self);
        Vector3 angles = player.transform.rotation.eulerAngles;
        angles.z = 0;
        player.transform.rotation = Quaternion.Euler(angles);

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
