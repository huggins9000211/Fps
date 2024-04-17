using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{

    [SerializeField] int timer;

    //[SerializeField] Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        //anim = gameObject.GetComponent<Animator>();
        //anim.speed = 0;
        GameManager.instance.SpawnDoorCount(1);
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
            GameManager.instance.SpawnDoorCount(-1);
            DoorOpen();
        }
    }
    void DoorOpen()
    {
        if(gameObject != null)
        {
            Destroy(gameObject);
            //anim.Play("Scene");
            //anim.speed = 1;
        }
    }
}
