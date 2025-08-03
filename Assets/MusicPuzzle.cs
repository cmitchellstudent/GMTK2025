using System.Collections;
using UnityEngine;

public class MusicPuzzle : MonoBehaviour
{
    public AudioSource clip;
    public BoxCollider doorBox;
    public GameObject barrier;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        clip.Play();
        StartCoroutine(WaitForEnd(clip.clip.length));
        doorBox.center = (Vector3.up * 10);
        barrier.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    IEnumerator Lull()
    {
        doorBox.center = Vector3.zero;
        yield return new WaitForSeconds(2f);
        clip.Play();
        StartCoroutine(WaitForEnd(clip.clip.length));
        doorBox.center = (Vector3.up * 10);
    }

    IEnumerator WaitForEnd(float length)
    {
        yield return new WaitForSeconds(length);
        StartCoroutine(Lull());
    }
}
