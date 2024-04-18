using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class BaseNPC : MonoBehaviour, IDamage
{
    [SerializeField] Renderer model;

    [SerializeField] public int hP;

    public GameObject target;
    // Start is called before the first frame update
    public virtual void Start()
    {
        if (GameManager.instance.npc1 == null)
        {
            GameManager.instance.npc1 = gameObject;
        }
        else if (GameManager.instance.npc2 == null)
        {
            GameManager.instance.npc2 = gameObject;
        }
        else if (GameManager.instance.npc3 == null)
        {
            GameManager.instance.npc3 = gameObject;
        }
   
        GetRandomTarget();
        
    }

    public virtual void Update()
    {
        if (target == null)
        {
            GetRandomTarget();
        }
    }
    void GetRandomTarget()
    {
        while (target == null)
        {
            int randomnum = Random.Range(0, 3);
            if (randomnum == 0)
            {
                target = GameManager.instance.player;
            }
            else if (randomnum == 1 && GameManager.instance.npc1 != gameObject && GameManager.instance.npc1 != null)
            {
                target = GameManager.instance.npc1;
            }
            else if (randomnum == 2 && GameManager.instance.npc2 != gameObject && GameManager.instance.npc2 != null)
            {
                target = GameManager.instance.npc2;
            }
            else if (randomnum == 3 && GameManager.instance.npc3 != gameObject && GameManager.instance.npc3 != null)
            {
                target = GameManager.instance.npc3;
            }
        }

    }



    public void TakeDamage(int amount, GameObject sourse)
    {
        hP -= amount;
        StartCoroutine(FlashRed());
        if (hP <= 0)
        {
            string sourceTag = sourse.tag;
            if (sourceTag == "Assult")
            {
                GameManager.instance.AddScore(0, 1);
            }
            else if (sourceTag == "Ninja")
            {
                GameManager.instance.AddScore(1, 1);

            }
            else if (sourceTag == "Sniper")
            {
                GameManager.instance.AddScore(2, 1);

            }
            else if (sourceTag == "Reaper")
            {
                GameManager.instance.AddScore(3, 1);

            }


            string thisTag = gameObject.tag;
            Destroy(gameObject);

            if (thisTag == "Assult")
            {
                //Destroy(gameObject);
                GameManager.instance.SpawnAssault(false);
            }
            else if (thisTag == "Ninja")
            {
                Destroy(gameObject);
                GameManager.instance.SpawnNinja(false);
            }
            else if (thisTag == "Sniper")
            {
                Destroy(gameObject);
                GameManager.instance.SpawnSniper(false);
            }
            else if (thisTag == "Reaper")
            {
                Destroy(gameObject);
                GameManager.instance.SpawnReaper(false);
            }

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
