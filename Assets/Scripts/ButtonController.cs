using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour {

    private Rigidbody rb;
    private Vector3 originalPosition;
    //private Quaternion originalRotation;
    public GameObject consoleFloor;

    public float pressY;

    private float triggerYDiff = 0.8f;

    public float returnSpeed;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();

        Physics.IgnoreCollision(consoleFloor.GetComponent<Collider>(), GetComponent<Collider>());

        originalPosition = transform.position;
        //originalRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {

        //rb.position = new Vector3(originalPosition.x, Mathf.Clamp(rb.position.y,-pressY, originalPosition.y), originalPosition.z);

        rb.velocity = new Vector3(0.0f, returnSpeed, 0.0f);
        //Debug.Log(originalPosition.y + "   " + -pressY + "     " + rb.position.y);

        rb.position = new Vector3(originalPosition.x, Mathf.Clamp(rb.position.y, originalPosition.y - pressY, originalPosition.y), originalPosition.z);

        /*
        rb.position = new Vector3
       (
           Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
           0.0f,
           Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
       );*/


        //rb.rotation = originalRotation;

    }
}
