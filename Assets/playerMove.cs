
using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerMove : MonoBehaviour
{
    public PauseManager PM;
    private CharacterController player;
    public CinemachineCamera cam;
    InputAction moveActions;
    InputAction lookActions;
    private float camSpeed;
    public float moveSpeed;

    private float xRotation;
    private float yRotation;
    public Transform orientation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GetComponent<CharacterController>();
        moveActions = InputSystem.actions.FindAction("Move");
        lookActions = InputSystem.actions.FindAction("Look");
    }

    // Update is called once per frame
    void Update()
    {
        camSpeed = PlayerPrefs.GetFloat("Mouse Sens");
        
        Vector2 moveInput = moveActions.ReadValue<Vector2>();
        var forward = player.transform.forward;
        var left = Vector3.Cross(Vector3.up, forward);
        
        Vector3 move = new Vector3(left.x, 0, left.z) * moveInput.x +
                       new Vector3(forward.x, 0, forward.z) * moveInput.y;
        player.Move(move * (moveSpeed * Time.deltaTime));
        //le gravity
        player.Move(Vector3.down * (10 * Time.deltaTime));
    }

    private void LateUpdate()
    {
        CameraRotation();
    }

    private void CameraRotation()
    {
        //TODO: MAKE THIS CONTROLLER COMPATIBLE
        Vector2 lookInput = lookActions.ReadValue<Vector2>();
        float mouseX = Mathf.Clamp(lookInput.x * Time.deltaTime, -1, 1) * camSpeed;
        float mouseY = Mathf.Clamp(lookInput.y * Time.deltaTime, -1, 1) * camSpeed;

        //Debug.Log(mouseX + " " + mouseY);
        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        
        transform.rotation = Quaternion.Euler(xRotation,yRotation,0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
    
}
