using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool died = false;

    private float xMovement = 0;
    private float zMovement = 0;

    void Update()
    {
        if (died)
        {
            Vector3 angles = transform.rotation.eulerAngles;
            Vector3 pos = transform.position;
            transform.rotation = Quaternion.Euler( new Vector3(angles.x, angles.y, angles.z + Time.deltaTime*60) );
            transform.position = new Vector3(pos.x, pos.y - Time.deltaTime*4, pos.z);
        }
        else
        {
            transform.rotation = Quaternion.LookRotation(GameObject.FindGameObjectWithTag("Player").transform.position - transform.position, Vector3.up);
            xMovement = Mathf.Sin(Mathf.Deg2Rad * transform.rotation.eulerAngles.y);
            zMovement = Mathf.Cos(Mathf.Deg2Rad * transform.rotation.eulerAngles.y);
            transform.position += new Vector3(xMovement * Time.deltaTime * 4, 0, zMovement * Time.deltaTime * 6);
        }
    }
}
