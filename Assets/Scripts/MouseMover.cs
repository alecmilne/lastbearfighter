using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMover : MonoBehaviour {

    private Vector3 screenSpace;
    //private Vector3 offset;

    public float speed;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        screenSpace = Camera.main.WorldToScreenPoint(transform.position);
    }

    private void OnMouseDown()
    {
        //translate the cubes position from the world to Screen Point
        

        //calculate any difference between the cubes world position and the mouses Screen position converted to a world point  
        //offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));

    }

    /*
    OnMouseDrag is called when the user has clicked on a GUIElement or Collider and is still holding down the mouse.
    OnMouseDrag is called every frame while the mouse is down.
    */

    private void Update()
    {

        //keep track of the mouse position
        Vector3 curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);

        //convert the screen mouse position to world point and adjust with offset
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace);// + offset;



        float moveHorizontal = curPosition.x - transform.position.x;
        float moveVertical = curPosition.y - transform.position.y;

        Debug.Log(moveHorizontal);

        //float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0.0f);


        rb.velocity = movement * speed;

        //rb.position = curPosition;

        //update the position of the object in the world
        //transform.position = curPosition;
    }
}
