using UnityEngine;

public class DoctorPuzzle : MonoBehaviour
{
    private bool isSolved;
    private bool doorOpen;

    public Pedestal yellow;
    public Pedestal purple;

    public DoorScript door;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isSolved = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (yellow.itemOn && purple.itemOn)
        {
            if (yellow.itemOn.name.Contains("Cheese") && purple.itemOn.name.Contains("Door"))
            {
                isSolved = true;
            }
        }

        if (isSolved && !doorOpen)
        {
            door.OpenDoor();
            doorOpen = true;
        }
    }
}
