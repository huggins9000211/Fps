using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SniperNPC : BaseNPC
{

    [SerializeField] Animator anim;//djadd
    [SerializeField] int animSpeedTrans;//djadd
   


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
        float animSpeed = agent.velocity.normalized.magnitude;//djadd
        anim.SetFloat("Speed", Mathf.Lerp(anim.GetFloat("Speed"), animSpeed, Time.deltaTime * animSpeedTrans));//djadd


        base.Update();
        targetDir = base.target.transform.position - transform.position;
        agent.SetDestination(base.target.transform.position);

        if (agent.remainingDistance <= agent.stoppingDistance)
        {

            FaceTarget();
        }

        if (agent.remainingDistance <= agent.stoppingDistance)
        {

            if (isShooting == false)
                StartCoroutine(Shoot1());
            if (mineOnCD == false)
            {
                StartCoroutine(Shoot2());
            }
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
        Vector3 newGunRotation = target.transform.position - shootPos.position;
        newGunRotation.y = 0;
       // shootPos.rotation = Quaternion.LookRotation(newGunRotation);


        anim.SetTrigger("Shoot");//djadd


       // Instantiate(bullet, shootPos.position, shootPos.rotation);
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
    public void CreateBullet()//djadd
    {
        Instantiate(bullet, shootPos.position, shootPos.rotation);//djadd
    }

}
