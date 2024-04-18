using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SniperNPC : BaseNPC
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

    [SerializeField]
    NavMeshAgent agent;
    [SerializeField]
    int faceTargetSpeed;
    bool targetInRange;
    Vector3 targetDir;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        targetDir = base.target.transform.position - transform.position;
        agent.SetDestination(base.target.transform.position);

        if (agent.remainingDistance <= agent.stoppingDistance)
        {

            FaceTarget();
        }

        if (agent.remainingDistance <= agent.stoppingDistance + 50)
        {

            if (isShooting == false)
                StartCoroutine(Shoot1());
            //if(granadeOnCD == false)
            //{
            //    StartCoroutine(Shoot2());
            //}
        }
    }
    void FaceTarget()
    {
        Quaternion rot = Quaternion.LookRotation(targetDir);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * faceTargetSpeed);
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
        Vector3 spawnPosition = shootPos.position + gameObject.transform.forward;
        GameObject spawnedMine = Instantiate(mine, spawnPosition, gameObject.transform.rotation);
        Rigidbody rb = spawnedMine.GetComponent<Rigidbody>();
        Vector3 finalThrowDirection = (gameObject.transform.forward + mineDirection).normalized;
        rb.AddForce(finalThrowDirection * mineVelocity, ForceMode.VelocityChange);
        yield return new WaitForSeconds(mineCD);
        mineOnCD = false;
    }
}
