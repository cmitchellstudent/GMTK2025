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
        if (_interactAction.WasPressedThisFrame())
        {
            if (InventoryHand.transform.childCount > 0)
            {
                Drop();
            }
            else if (!_pickUpBuffer.Equals(null) &&
                     InventoryHand.transform.childCount == 0)
            {
                Pickup();
            }
            else
            {
                
            }
        }
    }

    void Pickup()
    {
        _pickUpBuffer.GetComponent<Rigidbody>().isKinematic = true;
        _pickUpBuffer.transform.parent = InventoryHand.transform;
        _pickUpBuffer.transform.position = InventoryHand.transform.position;
        if (_pickUpBuffer.name.Contains("Key"))
        {
            _pickUpBuffer.transform.localRotation = Quaternion.Euler(0,90,0);
        }
    }

    void Drop()
    {
        Transform held = InventoryHand.transform.GetChild(0);
        held.parent = null;
        held.gameObject.GetComponent<Rigidbody>().isKinematic = false;
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
