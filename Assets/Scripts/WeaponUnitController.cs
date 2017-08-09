using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUnitController : MonoBehaviour
{

    private AudioSource audioSource;

    public Transform shotSpawn;
    public GameObject shot;

    private float nextFire;
    public float fireRate;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            //Fire();
        }
    }

    public void Fire()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            if( shotSpawn )
            {
                Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
                audioSource.Play();
            }
        }
    }

}
