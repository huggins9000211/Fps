using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AssultNPC : BaseNPC
{
    [SerializeField] Animator anim;//djadd
    [SerializeField] int animSpeedTrans;//djadd

    // Start is called before the first frame update
    [SerializeField] Transform shootPos;
    [SerializeField] float shootRate;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject grenade;
    [SerializeField] float grenadeVelocity;
    [SerializeField] float grenadeCD;
    [SerializeField] Vector3 granadeDirection;
    bool isShooting;
    bool granadeOnCD;

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
        float animSpeed = agent.velocity.normalized.magnitude;//djadd
        anim.SetFloat("Speed", Mathf.Lerp(anim.GetFloat("Speed"), animSpeed, Time.deltaTime * animSpeedTrans));//djadd

        base.Update();
        targetDir = base.target.transform.position - transform.position;
        agent.SetDestination(base.target.transform.position);



        if (agent.remainingDistance <= agent.stoppingDistance)
        {

            FaceTarget();
        }

        if (agent.remainingDistance <= agent.stoppingDistance + 30)
        {

            if (isShooting == false)
                StartCoroutine(Shoot1());
            //if(granadeOnCD == false)
            //{
            //    StartCoroutine(Shoot2());
            //}
        }


        //}
    }
    void FaceTarget()
    {
        Quaternion rot = Quaternion.LookRotation(targetDir);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * faceTargetSpeed);
    }

    IEnumerator Shoot1()
    {
        isShooting = true;
        anim.SetTrigger("Shoot");//djadd

        Instantiate(bullet, shootPos.position, shootPos.rotation);
        yield return new WaitForSeconds(shootRate);
        isShooting = false;
    }
    IEnumerator Shoot2()
    {

        granadeOnCD = true;
        Vector3 spawnPosition = shootPos.position + gameObject.transform.forward;
        GameObject spawnedGrenade = Instantiate(grenade, spawnPosition, gameObject.transform.rotation);
        Rigidbody rb = spawnedGrenade.GetComponent<Rigidbody>();
        Vector3 finalThrowDirection = (gameObject.transform.forward + granadeDirection).normalized;
        rb.AddForce(finalThrowDirection * grenadeVelocity, ForceMode.VelocityChange);
        yield return new WaitForSeconds(grenadeCD);
        granadeOnCD = false;
    }

    public void CreateBullet()//djadd
    {
        Instantiate(bullet, shootPos.position, transform.rotation);//djadd
    }


}
