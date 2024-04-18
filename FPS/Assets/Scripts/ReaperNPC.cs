using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ReaperNPC : BaseNPC
{
    [SerializeField] GameObject weponPos;
    [SerializeField] float swingRate;
    [SerializeField] float swingTime;
    [SerializeField] MeshRenderer mr;

    bool isStealthed;
    bool isSwinging;
    // Start is called before the first frame update
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
        agent.SetDestination(GameManager.instance.player.transform.position);

        if (agent.remainingDistance <= agent.stoppingDistance)
        {

            FaceTarget();
        }

        if (agent.remainingDistance < 2)
        {
            if (isSwinging == false)
                StartCoroutine(Shoot1());
        }
        if (!isStealthed)
        {

            if (isSwinging == false)
                StartCoroutine(Shoot2());
        }

    }

    void FaceTarget()
    {
        Quaternion rot = Quaternion.LookRotation(targetDir);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * faceTargetSpeed);
    }

    IEnumerator Shoot1()
    {
        if (isSwinging)
        {
            yield break;
        }
        isSwinging = true;

        if (isStealthed)
            Unstealth();
        Vector3 newRot = weponPos.transform.eulerAngles + new Vector3(90, 0, 0);

        Vector3 currentRot = weponPos.transform.eulerAngles;

        float counter = 0;
        while (counter < swingTime)
        {
            counter += Time.deltaTime;
            weponPos.transform.eulerAngles = Vector3.Lerp(currentRot, newRot, counter / swingTime);
            yield return null;
        }
        isSwinging = false;
    }
    IEnumerator Shoot2()
    {
        mr.enabled = false;
        isStealthed = true;

        yield return new WaitForSeconds(.1f);

    }
    public void TakeDamage(int amount, GameObject source)
    {
        base.TakeDamage(amount, source);
        if (isStealthed)
            Unstealth();

    }

    void Unstealth()
    {
        isStealthed = false;
        mr.enabled = true;
    }
}
