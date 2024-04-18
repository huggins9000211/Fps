using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scythe : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other == transform.parent.parent)
        {
            return;
        }
        BaseNPC npc = other.GetComponent<BaseNPC>();
        BasePlayer player = other.GetComponent<BasePlayer>();
        if (npc != null)
        {
            if (npc.hP <= 100)
            {
                npc.TakeDamage(npc.hP + 1);
            }
            else
            {
                npc.TakeDamage(10);
            }
        }
        else if (player != null)
        {
            if (player.hP <= 100)
            {
                player.TakeDamage(player.hP + 1);
            }
            else
            {
                player.TakeDamage(10);
            }
        }
    }
}
