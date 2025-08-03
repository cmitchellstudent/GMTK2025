using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Inventory : MonoBehaviour
{
    public GameObject InventoryHand;
    public TaxPuzzle TP;

    public AudioSource pickUpSound;
    public AudioSource dropSound;
    
    private BoxCollider _hitbox;

    private InputAction _interactAction;
    
    private GameObject _pickUpBuffer;
    private GameObject _doorBuffer;

    private bool _isLookingAtDoorWithKey;
    private bool _isLookingAtJob;
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
            if (_isLookingAtJob)
            {
                TP.IncrementJob();
            }
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
        pickUpSound.pitch = Random.Range(90, 110) / 100f;
        pickUpSound.Play();
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
        dropSound.pitch = Random.Range(90, 110) / 100f;
        dropSound.Play();
        Transform held = InventoryHand.transform.GetChild(0);
        held.localPosition += (Vector3.left * 0.23f) ;
        held.parent = null;
        held.gameObject.GetComponent<Rigidbody>().isKinematic = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);
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
                else
                {
                    SetIcon(Color.red);
                }
            }
            catch
            {
                SetIcon(Color.red);
            }
                
        }
        else if (other.gameObject.CompareTag("Job"))
        {
            _isLookingAtJob = true;
            SetIcon(Color.blue);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _isLookingAtDoorWithKey = false;
        _isLookingAtJob = false;
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
