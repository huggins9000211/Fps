using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseNPC : MonoBehaviour, IDamage
{
    [SerializeField] Renderer model;

    [SerializeField] public int hP;

    protected GameObject target = null;
    // Start is called before the first frame update
    void Start()
    {
        //while (target == null)
        //{
        //    int randomNum = Random.Range(0, 3);
        //    if (randomNum == 0)
        //    {
        //        target = GameManager.instance.player;
        //    }
        //    else if(randomNum == 1 && GameManager.instance.npc1 != gameObject) 
        //    {
        //        target = GameManager.instance.npc1;
        //    }
        //    else if (randomNum == 2 && GameManager.instance.npc2 != gameObject)
        //    {
        //        target = GameManager.instance.npc2;
        //    }
        //    else if (randomNum == 3 && GameManager.instance.npc3 != gameObject)
        //    {
        //        target = GameManager.instance.npc3;
        //    }
        //}
    }

    // Update is called once per frame


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
