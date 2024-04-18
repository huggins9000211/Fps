using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayer : MonoBehaviour, IDamage
{
    [SerializeField] CharacterController characterController;

    [SerializeField] public int hP;
    [SerializeField] float speed;
    [SerializeField] int gravity;

    [SerializeField] int jumpSpeed;



    Vector3 moveDir;
    Vector3 playerVel;
    protected int jumpsAllowed;
    int jumpedTimes;
    int hPOrg;

    // Start is called before the first frame update
     public virtual void Start()
    {
        hPOrg = hP;
        updatePlayerUI();
        GameManager.instance.playerDamageScreen.SetActive(false);
        //characterController = GetComponent<CharacterController>();
        //jumpsAllowed = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * shootDistance, Color.green);
        Debug.Log("test");
        Movement();
    }

    void updatePlayerUI()
    {
        GameManager.instance.hpBar.fillAmount = (float)hP / hPOrg;
    }

    public void TakeDamage(int amount, GameObject source)
    {
        hP -= amount;
        StartCoroutine(FlashDamage());
        updatePlayerUI();
        if (hP <= 0)
        {
            string sourceTag = source.tag;
            if (sourceTag == "Assult")
            {
                GameManager.instance.AddScore(0, 1);
            }
            else if (sourceTag == "Ninja")
            {
                GameManager.instance.AddScore(1, 1);

            }
            else if (sourceTag == "Sniper")
            {
                GameManager.instance.AddScore(2, 1);

            }
            else if (sourceTag == "Reaper")
            {
                GameManager.instance.AddScore(3, 1);

            }

            string thisTag = gameObject.tag;
            if (thisTag == "Assult")
            {
                Destroy(gameObject);
                GameManager.instance.SpawnAssault(true);
            }
            else if (thisTag == "Ninja")
            {
                Destroy(gameObject);
                GameManager.instance.SpawnNinja(true);
            }
            else if (thisTag == "Sniper")
            {
                Destroy(gameObject);
                GameManager.instance.SpawnSniper(true);
            }
            else if (thisTag == "Reaper")
            {
                Destroy(gameObject);
                GameManager.instance.SpawnReaper(true);
            }

        }

    }

    IEnumerator FlashDamage()
    {
        GameManager.instance.playerDamageScreen.SetActive(true);
        yield return new WaitForSeconds(.1f);
        GameManager.instance.playerDamageScreen.SetActive(false);
    }

    protected void Movement()
    {
        if (characterController.isGrounded)
        {
            jumpedTimes = 0;
            playerVel = Vector3.zero;
        }


        moveDir = Input.GetAxis("Horizontal") * transform.right + Input.GetAxis("Vertical") * transform.forward;
        characterController.Move(moveDir * speed * Time.deltaTime);


        if (Input.GetButtonDown("Jump") && jumpedTimes < jumpsAllowed)
        {
            jumpedTimes++;
            playerVel.y = jumpSpeed;
        }


        playerVel.y -= gravity * Time.deltaTime;
        characterController.Move(playerVel * Time.deltaTime);
    }

    public void Stun(float duration)
    {
        StartCoroutine(ApplyStun(duration));
    }

    IEnumerator ApplyStun(float duration)
    {
        Camera mainCamera = Camera.main;
        CameraController characterController = mainCamera.GetComponent<CameraController>();
        float baseSens = characterController.GetSens();
        float baseSpeed = speed;

        characterController.SetSens(baseSens * .5f);
        speed *= .5f;
        yield return new WaitForSeconds(duration);
        characterController.SetSens(baseSens);
        speed = baseSpeed;
    }
}
