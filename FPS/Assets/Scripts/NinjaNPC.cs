using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NinjaNPC : BaseNPC
{
    [SerializeField] Animator anim;//djadd
    [SerializeField] int animSpeedTrans;//djadd

    [SerializeField] Transform shootPos;
    [SerializeField] float shootRate;
    [SerializeField] GameObject bullet;

    bool isShooting;

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

        if (agent.remainingDistance < 12)
        {
            if (isShooting == false)
                StartCoroutine(Shoot2());
        }
        if (agent.remainingDistance <= agent.stoppingDistance + 40)
        {

            if (isShooting == false)
                StartCoroutine(Shoot1());
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
        anim.SetTrigger("Shoot");//djadd

        yield return new WaitForSeconds(0.1f);//djadd

        Instantiate(bullet, shootPos.position, shootPos.rotation);
        yield return new WaitForSeconds(.15f);
        Instantiate(bullet, shootPos.position, shootPos.rotation);
        yield return new WaitForSeconds(.15f);
        Instantiate(bullet, shootPos.position, shootPos.rotation);
        yield return new WaitForSeconds(shootRate);

       
        isShooting = false;
    }
    IEnumerator Shoot2()
    {
        isShooting = true;

        Instantiate(bullet, shootPos.position, shootPos.rotation);

        Instantiate(bullet, shootPos.position, shootPos.rotation).transform.Rotate(0, 20, 0);

        Instantiate(bullet, shootPos.position, shootPos.rotation).transform.Rotate(0, -20, 0);
        yield return new WaitForSeconds(shootRate);
        isShooting = false;
    }

    
}
