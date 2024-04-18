using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseNPC : MonoBehaviour, IDamage
{
    [SerializeField] Renderer model;

    [SerializeField] public int hP;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int amount)
    {
        hP -= amount;
        StartCoroutine(FlashRed());
        if (hP <= 0)
        {
            //GameManager.instance.EnimyCountUpdate(-1);
            Destroy(gameObject);
        }
    }


    IEnumerator FlashRed()
    {
        model.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        model.material.color = Color.white;
    }

    public void Stun(float duration)
    {
        ApplyStun(duration);
    }

    IEnumerator ApplyStun(float duration)
    {
        //







        //
        model.material.color = Color.red;
        yield return new WaitForSeconds(duration);
    }
}
