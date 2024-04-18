using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperPlayer : BasePlayer
{
    // Start is called before the first frame update
    [SerializeField] Transform shootPos;
    [SerializeField] float shootRate;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject mine;
    [SerializeField] float mineVelocity;
    [SerializeField] float mineCD;
    [SerializeField] Vector3 mineDirection;
    bool isShooting;
    bool mineOnCD;

    Camera mainCamera;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        base.jumpsAllowed = 1;
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        base.Movement();

        if (Input.GetButton("Fire1") & !isShooting)
        {
            StartCoroutine(Shoot1());
        }
        else if (Input.GetButton("Fire2") & !mineOnCD)
        {
            StartCoroutine(Shoot2());
        }
    }

    IEnumerator Shoot1()
    {
        isShooting = true;

        Instantiate(bullet, shootPos.position, shootPos.rotation);
        yield return new WaitForSeconds(shootRate);
        isShooting = false;
    }
    IEnumerator Shoot2()
    {

        mineOnCD = true;
        Vector3 spawnPosition = shootPos.position + mainCamera.transform.forward;
        GameObject spawnedMine = Instantiate(mine, spawnPosition, mainCamera.transform.rotation);
        Rigidbody rb = spawnedMine.GetComponent<Rigidbody>();
        Vector3 finalThrowDirection = (mainCamera.transform.forward + mineDirection).normalized;
        rb.AddForce(finalThrowDirection * mineVelocity, ForceMode.VelocityChange);
        yield return new WaitForSeconds(mineCD);
        mineOnCD = false;
    }
}
