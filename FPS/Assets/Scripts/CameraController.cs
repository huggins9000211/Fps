using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float sensitivity;
    [SerializeField] int lockVertMin, lockVertMax;
    [SerializeField] bool invertY;
    [SerializeField] GameObject gunOffset;



    float rotX;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //get input

        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;

        // invert ?
        if (invertY)
            rotX += mouseY;
        else
            rotX -= mouseY;

        // clamp the rotX on the x-axis
        rotX = Mathf.Clamp(rotX, lockVertMin, lockVertMax);

        //rotate the cam on the x-axis
        transform.localRotation = Quaternion.Euler(rotX, 0, 0);
        gunOffset.transform.localRotation = transform.localRotation;

        //rotate the player on the y-axis

        CharacterController hasRecoil = transform.parent.GetComponent<CharacterController>();
        if (hasRecoil != null)
        {
            transform.parent.Rotate(Vector3.up * mouseX);
        }
        else
        {
            transform.parent.parent.Rotate(Vector3.up * mouseX);
        }
    }

    public void SetSens(float sens)
    {
        sensitivity = sens;
    }

    public float GetSens()
    {
        return sensitivity;
    }


}
