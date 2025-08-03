using UnityEngine;

public class SpecialHitbox : MonoBehaviour
{
    public GameObject[] itemsToRename;
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
            foreach (var thing in itemsToRename)
            {
                thing.name = "Key";
            }
        }
    }
}
