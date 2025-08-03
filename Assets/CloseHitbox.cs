using System;
using UnityEngine;

public class CloseHitbox : MonoBehaviour
{
    public DoorScript door;
    
    public GameObject[] roomsToDisable;

    public GameObject[] roomsToEnable;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            door.CloseDoor();
            foreach (var roomToDisable in roomsToDisable)
            {
                if (roomToDisable)
                {
                    roomToDisable.SetActive(false);
                }
            }
            foreach (var roomToEnable in roomsToEnable)
            {
                if (roomToEnable)
                {
                    roomToEnable.SetActive(true);
                }
            }
        }
    }
}
