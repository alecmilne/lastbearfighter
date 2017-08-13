using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {

    private Rigidbody rb;

    public float speed;
    public float tilt;
    public float yaw;

    public Transform weaponHolder;
    //public Transform weaponR1Holder;
    //private GameObject weaponR1Object;
    //private WeaponUnitController R1Script;
    private List<GameObject> weaponObjects;
    private List<WeaponUnitController> weaponScripts;

    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;

    private float nextFire;

    public Boundary boundary;

    private AudioSource audioSource;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        InitialiseWeapons();
    }

    public void InitialiseWeapons()
    {
        weaponObjects = new List<GameObject>();
        weaponScripts = new List<WeaponUnitController>();

        int weaponPositionCount = weaponHolder.childCount;

        for (int i = 0; i < weaponPositionCount; ++i)
        {
            if (weaponHolder.GetChild(i).CompareTag("WeaponPosition"))
            {
                ProcessWeaponPosition(weaponHolder.GetChild(i));
            }
        }
    }

    public Transform getEmptyWeaponSlot()
    {
        int weaponPositionCount = weaponHolder.childCount;

        for (int i = 0; i < weaponPositionCount; ++i)
        {
            
            if (weaponHolder.GetChild(i).CompareTag("WeaponPosition"))
            {
                bool hasWeapon = false;
                
                Transform weaponPosition = weaponHolder.GetChild(i);


                int childCount = weaponPosition.childCount;
                for (int j = 0; j < childCount; ++j)
                {
                    if (weaponPosition.GetChild(j).CompareTag("Weapon"))
                    {
                        hasWeapon = true;
                        /*
                        GameObject newWeapon = weaponPosition.GetChild(j).gameObject;
                        Debug.Log("Found Weapon: " + newWeapon.name + " in position " + weaponPosition.name);
                        weaponObjects.Add(newWeapon);

                        WeaponUnitController newWeaponScript = (WeaponUnitController)newWeapon.GetComponent(typeof(WeaponUnitController));

                        weaponScripts.Add(newWeaponScript);*/
                    }
                }

                if (!hasWeapon)
                {
                    return weaponPosition;
                }

            }
        }

        return null;
    }

    private void ProcessWeaponPosition(Transform weaponPosition)
    {
        int childCount = weaponPosition.childCount;
        for (int j = 0; j < childCount; ++j)
        {
            if (weaponPosition.GetChild(j).CompareTag("Weapon"))
            {
                GameObject newWeapon = weaponPosition.GetChild(j).gameObject;
                Debug.Log("Found Weapon: " + newWeapon.name + " in position " + weaponPosition.name);
                weaponObjects.Add(newWeapon);

                WeaponUnitController newWeaponScript = (WeaponUnitController)newWeapon.GetComponent(typeof(WeaponUnitController));

                weaponScripts.Add(newWeaponScript);
            }
        }
    }

    private void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            doFire();
            //R1Script.Fire();
        }
    }

    public void doFire()
    {
        Fire();
        foreach (WeaponUnitController weaponScript in weaponScripts)
        {
            weaponScript.Fire();
        }
    }

    public void Fire()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            audioSource.Play();
        }
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        
        rb.velocity = movement * speed;

        rb.position = new Vector3
        (
            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
        );

        rb.rotation = Quaternion.Euler(rb.velocity.z * yaw, 0.0f, rb.velocity.x * -tilt);
    }

}
