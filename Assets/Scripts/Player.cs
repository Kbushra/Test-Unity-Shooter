using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject pivot;
    public Animator gunAnimator;
    public Image death;
    public Text ammoCount;

    private float xMovement = 0;
    private float zMovement = 0;
    private int ammo = 5;
    private float time = 0;
    private bool died = false;

    void Start()
    {
        Application.targetFrameRate = 60;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        time += Time.deltaTime;

        if (died)
        {
            death.color += new Color(0, 0, 0, 0.01f);
            if (death.color.a >= 1) { SceneManager.LoadScene("SampleScene"); }
        }

        Animate();

        Vector3 pivot_angle = pivot.transform.rotation.eulerAngles;
        Rotate(pivot_angle);
        CalculateMovement(pivot_angle);
        
        GetCollision();

        ammoCount.text = $"Alive for {(int)Mathf.Floor(time)}s\nAmmo: {ammo} - R to reload";
    }

    void Animate()
    {
        if (gunAnimator.GetInteger("state") != 0 && gunAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            gunAnimator.SetInteger("state", 0);
        }

        if (ammo > 0 && gunAnimator.GetInteger("state") != 2 && Input.GetMouseButtonDown(0))
        {
            gunAnimator.SetInteger("state", 1);
            ammo--;

            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.CompareTag("Respawn"))
            {
                hit.collider.gameObject.GetComponentInParent<Enemy>().died = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            gunAnimator.SetInteger("state", 2);
            ammo = 5;
        }
    }

    void Rotate(Vector3 pivot_angle)
    {
        float clamped_xrot = 0;
        if (pivot_angle.x - Input.GetAxisRaw("Mouse Y") > 180) { clamped_xrot = pivot_angle.x - Input.GetAxisRaw("Mouse Y") - 360; }
        else { clamped_xrot = pivot_angle.x - Input.GetAxisRaw("Mouse Y"); }

        clamped_xrot = Mathf.Clamp(clamped_xrot, -90f, 90f);

        pivot.transform.rotation = Quaternion.Euler(new Vector3(clamped_xrot, pivot_angle.y + Input.GetAxisRaw("Mouse X"), pivot_angle.z));
    }

    void CalculateMovement(Vector3 pivot_angle)
    {
        xMovement = 0;
        zMovement = 0;

        if (Input.GetKey(KeyCode.W))
        {
            xMovement += Mathf.Sin(Mathf.Deg2Rad * pivot_angle.y);
            zMovement += Mathf.Cos(Mathf.Deg2Rad * pivot_angle.y);
        }

        if (Input.GetKey(KeyCode.A))
        {
            xMovement += Mathf.Sin(Mathf.Deg2Rad * (pivot_angle.y - 90));
            zMovement += Mathf.Cos(Mathf.Deg2Rad * (pivot_angle.y - 90));
        }

        if (Input.GetKey(KeyCode.S))
        {
            xMovement += Mathf.Sin(Mathf.Deg2Rad * (pivot_angle.y - 180));
            zMovement += Mathf.Cos(Mathf.Deg2Rad * (pivot_angle.y - 180));
        }

        if (Input.GetKey(KeyCode.D))
        {
            xMovement += Mathf.Sin(Mathf.Deg2Rad * (pivot_angle.y - 270));
            zMovement += Mathf.Cos(Mathf.Deg2Rad * (pivot_angle.y - 270));
        }

        xMovement *= 5;
        zMovement *= 5;
    }

    void GetCollision()
    {
        Vector3 movement = new Vector3(xMovement * Time.deltaTime, 0, zMovement * Time.deltaTime);
        Vector3 position = transform.position + movement;

        Collider[] collisions = Physics.OverlapCapsule(new Vector3(position.x, position.y + 0.5f, position.z),
        new Vector3(position.x, position.y - 0.5f, position.z), 0.5f);

        foreach (Collider collision in collisions)
        {
            if (collision.gameObject.CompareTag("Respawn")) { died = true; }
            else if (collision.gameObject.CompareTag("Solid"))
            {
                if (xMovement > 0.001)
                {
                    xMovement *= 0.8f;
                    zMovement *= 0.8f;
                    GetCollision();
                }

                return;
            }
        }

        transform.position += movement;
    }
}
