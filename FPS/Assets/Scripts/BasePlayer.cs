using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayer : MonoBehaviour, IDamage
{

    [SerializeField]
    CharacterController characterController;

    [SerializeField]
    int hP;
    [SerializeField]
    int speed;
    [SerializeField]
    int gravity;

    [SerializeField]
    int jumpSpeed;



    Vector3 moveDir;
    Vector3 playerVel;

    int jumpedTimes;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * shootDistance, Color.green);

        Movement();
    }

    public void TakeDamage(int amount)
    {
        hP -= amount;

    }

    void Movement()
    {
        if (characterController.isGrounded)
        {
            jumpedTimes = 0;
            playerVel = Vector3.zero;
        }


        moveDir = Input.GetAxis("Horizontal") * transform.right + Input.GetAxis("Vertical") * transform.forward;
        characterController.Move(moveDir * speed * Time.deltaTime);


        if (Input.GetButtonDown("Jump") && jumpedTimes < 1)
        {
            jumpedTimes++;
            playerVel.y = jumpSpeed;
        }


        playerVel.y -= gravity * Time.deltaTime;
        characterController.Move(playerVel * Time.deltaTime);
    }
}
