using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject pivot;
    public Animator gunAnimator;
    public CapsuleCollider coll;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (gunAnimator.GetInteger("state") != 0 && gunAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1f >= 0.95f)
        {
            gunAnimator.SetInteger("state", 0);
        }

        if (gunAnimator.GetInteger("state") != 2 && Input.GetMouseButtonDown(0))
        {
            gunAnimator.SetInteger("state", 1);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            gunAnimator.SetInteger("state", 2);
        }

        Vector3 pivot_angle = pivot.transform.rotation.eulerAngles;
        pivot.transform.rotation = Quaternion.Euler( new Vector3(pivot_angle.x - Input.GetAxisRaw("Mouse Y"), pivot_angle.y + Input.GetAxisRaw("Mouse X"), pivot_angle.z) );
    }
}
