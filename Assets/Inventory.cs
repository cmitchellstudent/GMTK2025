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
    private GameObject _doorBuffer;

    private bool _isLookingAtDoorWithKey;
    public GameObject iconObj;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _pickUpBuffer = null;
        _isLookingAtDoorWithKey = false;
        //_hitbox = GetComponent<BoxCollider>();
        _interactAction = InputSystem.actions.FindAction("Interact");
        ClearIcon();
    }

    // Update is called once per frame
    void Update()
    {
        if (_interactAction.WasPressedThisFrame())
        {
            if (_isLookingAtDoorWithKey)
            {
                Destroy(InventoryHand.transform.GetChild(0).gameObject);
                _doorBuffer.gameObject.GetComponent<DoorScript>().OpenDoor();
                OnTriggerExit(_doorBuffer.gameObject.GetComponent<Collider>());
            }
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
        if (_pickUpBuffer.name.Contains("Key") || true)
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
        Debug.Log(other.name);
        if (other.gameObject.CompareTag("Item"))
        {
            _pickUpBuffer = other.gameObject;
            SetIcon(Color.blue);
        }
        else if (other.gameObject.CompareTag("Door"))
        {
            _doorBuffer = other.gameObject;
            try 
            {
                if (InventoryHand.transform.GetChild(0).name.Contains("Key"))
                {
                    SetIcon(Color.green);
                    _isLookingAtDoorWithKey = true;
                }
            }
            catch
            {
                SetIcon(Color.red);
            }
                
        }
        {
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _isLookingAtDoorWithKey = false;
        if (other.gameObject.Equals(_pickUpBuffer))
        {
            _pickUpBuffer = null;
        }
        ClearIcon();
    }

    void SetIcon(Color temp)
    {
        iconObj.SetActive(true);
        Image img = iconObj.GetComponent<Image>();
        img.color = temp;
    }

    void ClearIcon()
    {
        iconObj.SetActive(false);
    }
}
