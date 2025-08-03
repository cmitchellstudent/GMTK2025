using UnityEngine;

public class LogicPuzzle : MonoBehaviour
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
        doorOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (yellow.itemOn && purple.itemOn)
        {
            string x = yellow.itemOn.name;
            if (x != purple.itemOn.name)
            {
                isSolved = false;
            }
            else
            {
                isSolved = true;
            }

            if (isSolved && !doorOpen)
            {
                door.OpenDoor();
                doorOpen = true;
            }
        }
    }
}
