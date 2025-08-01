using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject InventoryHand;
    private BoxCollider _hitbox;

    private InputAction _interactAction;
    
    private GameObject _pickUpBuffer;
    public GameObject interactIcon;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _pickUpBuffer = null;
        interactIcon.SetActive(false);
        //_hitbox = GetComponent<BoxCollider>();
        _interactAction = InputSystem.actions.FindAction("Interact");
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(_pickUpBuffer.Equals(null));
        if (_interactAction.WasPressedThisFrame() && !_pickUpBuffer.Equals(null))
        {
            GameObject clone = _pickUpBuffer;
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            _pickUpBuffer = other.gameObject;
            interactIcon.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.Equals(_pickUpBuffer))
        {
            _pickUpBuffer = null;
            interactIcon.SetActive(false);
        }
    }
}
