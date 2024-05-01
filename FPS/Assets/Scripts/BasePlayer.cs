using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayer : MonoBehaviour, IDamage
{
    [Header("----- Components -----")]
    [SerializeField] CharacterController characterController;
    [SerializeField] AudioSource aud;
    //[SerializeField] AudioManager audMan;

    [Header("----- Stats -----")]
    [SerializeField] public int hP;
    [SerializeField] float speed;
    [SerializeField] int gravity;

    [SerializeField] int jumpSpeed;

    [Header("----- Audio -----")]
    [SerializeField] AudioClip[] audJump;
    [Range(0, 1)][SerializeField] float audJumpVol;

    [SerializeField] AudioClip[] audHurt;
    [Range(0, 1)][SerializeField] float audHurtVol;

    [SerializeField] AudioClip[] audSteps;
    [Range(0, 1)][SerializeField] float audStepsVol;



    public bool playingSteps; ///// Mav
    public bool isSprinting; ///// Mav


    Vector3 moveDir;
    Vector3 playerVel;
    protected int jumpsAllowed;
    int jumpedTimes;
    int hPOrg;
    Vector3 spawnPos;

    // Start is called before the first frame update
     public virtual void Start()
    {
        hPOrg = hP;
        spawnPos = transform.position;
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

        PlayJump(); ///// Mav

        StartCoroutine(FlashDamage());

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


            hP = hPOrg;
            characterController.enabled = false;
            transform.position = spawnPos;
            characterController.enabled = true;

        }
        updatePlayerUI();

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
            PlayJump(); ///// Mav
        }


        playerVel.y -= gravity * Time.deltaTime;
        characterController.Move(playerVel * Time.deltaTime);

        if (characterController.isGrounded && moveDir.normalized.magnitude > 0.3f && !playingSteps)
        {
            StartCoroutine(PlaySteps());
        }
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

    public void PlayJump()
    {
        aud.PlayOneShot(audJump[Random.Range(0, audJump.Length - 1)], audJumpVol);
    }
    public void PlayHurt()
    {
        aud.PlayOneShot(audHurt[Random.Range(0, audHurt.Length - 1)], audHurtVol);
    }
    public IEnumerator PlaySteps()
    {
        GameManager.instance.basePlayer.playingSteps = true;
        aud.PlayOneShot(audSteps[Random.Range(0, audSteps.Length - 1)], audStepsVol);

        if (!GameManager.instance.basePlayer.isSprinting)
            yield return new WaitForSeconds(0.5f);
        else
            yield return new WaitForSeconds(0.3f);

        GameManager.instance.basePlayer.playingSteps = false;
    }
}
