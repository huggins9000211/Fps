using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultPlayer : BasePlayer
{
    // Start is called before the first frame update
    [SerializeField] Transform shootPos;
    [SerializeField] GameObject gunModel;
    [SerializeField] float shootRate;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject grenade;
    [SerializeField] float grenadeVelocity;
    [SerializeField] float grenadeCD;
    [SerializeField] Vector3 granadeDirection;
    bool isShooting;
    bool granadeOnCD;
    Recoil recoilScript;
    Camera mainCamera;
    List<GunStats> gunList = new List<GunStats>();
    int selectedGun;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        base.jumpsAllowed = 1;
        recoilScript = transform.Find("Recoil").GetComponent<Recoil>();
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        mainCamera = Camera.main;
        base.Movement();
        SelectGun();
        if (Input.GetButton("Fire1") && !isShooting && gunList.Count > 0)
        {
            if (gunList[selectedGun].ammoCur > 0)
            {
                StartCoroutine(Shoot1());
                gunList[selectedGun].ammoCur--;
                UpdateAmmoUI();
            }
            else
            {
                StartCoroutine(Reload());
            }
        }
        else if (Input.GetButton("Fire2") && !granadeOnCD)
        {
            StartCoroutine(Shoot2());
        }
    }

    IEnumerator Reload()
    {
        isShooting = true;
        yield return new WaitForSeconds(gunList[selectedGun].reloadSpeed);
        gunList[selectedGun].ammoCur = gunList[selectedGun].ammoMax;
        UpdateAmmoUI();
        isShooting = false;
    }

    IEnumerator Shoot1()
    {
        isShooting = true;

        Instantiate(bullet, shootPos.position, shootPos.rotation);
        recoilScript.RecoilFire();
        yield return new WaitForSeconds(shootRate);
        isShooting = false;
    }
    IEnumerator Shoot2()
    {

        granadeOnCD = true;
        Vector3 spawnPosition = shootPos.position + mainCamera.transform.forward;
        GameObject spawnedGrenade = Instantiate(grenade, spawnPosition, mainCamera.transform.rotation);
        Rigidbody rb = spawnedGrenade.GetComponent<Rigidbody>();
        Vector3 finalThrowDirection = (mainCamera.transform.forward + granadeDirection).normalized;
        rb.AddForce(finalThrowDirection * grenadeVelocity, ForceMode.VelocityChange);
        yield return new WaitForSeconds(grenadeCD);
        granadeOnCD = false;
    }

    public void getGunStats(GunStats gun)
    {
        gunList.Add(gun);
        selectedGun = gunList.Count - 1;
        ChangeGun(gun);
    }
    void SelectGun()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && selectedGun < gunList.Count - 1 && !isShooting)
        {
            selectedGun++;
            ChangeGun(gunList[selectedGun]);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0 && selectedGun > 0 && !isShooting)
        {
            selectedGun--;
            ChangeGun(gunList[selectedGun]);
        }
    }

    void UpdateAmmoUI()
    {
        GameManager.instance.ammoCurText.text = gunList[selectedGun].ammoCur.ToString("F0");
        GameManager.instance.ammoMaxText.text = gunList[selectedGun].ammoMax.ToString("F0");
    }

    void ChangeGun(GunStats gun)
    {
        UpdateAmmoUI();
        bullet = gun.bullet;
        shootRate = gun.shootRate;

        gunModel.GetComponent<MeshFilter>().sharedMesh = gun.gunModel.GetComponent<MeshFilter>().sharedMesh;
        gunModel.GetComponent<MeshRenderer>().sharedMaterial = gun.gunModel.GetComponent<MeshRenderer>().sharedMaterial;
    }
}
