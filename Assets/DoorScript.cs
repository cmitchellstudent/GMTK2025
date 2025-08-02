using System.Collections;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public GameObject hinge;
    public float duration;
    public GameObject barrier;

    private Quaternion _startingRot;

    private Quaternion _endingRot;
    private float _timer;
    private bool _isOpen;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _isOpen = false;
        barrier.SetActive(false);
        _startingRot = hinge.transform.rotation;
        _endingRot = Quaternion.Euler(_startingRot.x,_startingRot.y+90,_startingRot.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenDoor()
    {
        //changing tag will cease interaction.
        gameObject.tag = "Untagged";
        _isOpen = true;
        StartCoroutine(OpenAnim());
    }

    public void CloseDoor()
    {
        if (_isOpen)
        {
            StartCoroutine(CloseAnim());
            _isOpen = false;
            barrier.SetActive(true);
        }
    }

    IEnumerator OpenAnim()
    {
        _timer = 0;
        while (_timer < duration)
        {
            hinge.transform.rotation = Quaternion.Slerp(_startingRot,_endingRot,_timer);
            _timer += Time.deltaTime;
            yield return null;
        }
        _timer = 0;
        yield return null;
    }
    IEnumerator CloseAnim()
    {
        _timer = 0;
        while (_timer < duration)
        {
            hinge.transform.rotation = Quaternion.Slerp(_endingRot, _startingRot, _timer);
            _timer += Time.deltaTime;
            yield return null;
        }
        _timer = 0;
        yield return null;
    }
}
