using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{

    [SerializeField] int timer;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DoorTimer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DoorTimer()
    {
        if (timer > 0)
        {
            yield return new WaitForSecondsRealtime(1);
            timer--;
            StartCoroutine(DoorTimer());
        }
        else if (timer <= 0)
        {
            DoorOpen();
        }
    }
    void DoorOpen()
    {
        if(gameObject != null)
        {
            Destroy(gameObject);
        }
    }
}
