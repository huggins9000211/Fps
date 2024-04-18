using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaperPlayer : BasePlayer
{
    [SerializeField] GameObject weponPos;
    [SerializeField] float swingRate;
    [SerializeField] float swingTime;
    [SerializeField] MeshRenderer mr;

    bool isStealthed;
    bool isSwinging;
    // Start is called before the first frame update
    void Start()
    {
        base.jumpsAllowed = 2;
    }

    // Update is called once per frame
    void Update()
    {
        base.Movement();

        if (Input.GetButton("Fire1"))
        {
            StartCoroutine(Shoot1());
        }
        else if (Input.GetButton("Fire2"))
        {
            StartCoroutine(Shoot2());
        }
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
    public void TakeDamage(int amount)
    {
        base.TakeDamage(amount);
        if (isStealthed)
            Unstealth();

    }

    void Unstealth() 
    {
        isStealthed = false;
        mr.enabled = true;
    }
}
