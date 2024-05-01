using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GunStats : ScriptableObject
{
    public GameObject gunModel;
    public float shootRate;
    public GameObject bullet;
    public int ammoMax;
    public int ammoCur;
    public float reloadSpeed;
}
