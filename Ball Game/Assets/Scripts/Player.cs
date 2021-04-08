using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    public static Player instance;
    public FloatingJoystick joyStick;
    Rigidbody rb;
    
    private float dirZ;
    private float dirX;
    public bool controlsEnabled;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        controlsEnabled = true;
        rb = GetComponent<Rigidbody>();
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        if (controlsEnabled)
        {
            //dirX = -(joyStick.Vertical * moveSpeed * Time.deltaTime);
            //dirZ = joyStick.Horizontal * moveSpeed * Time.deltaTime;

            dirX = -(Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime);
            dirZ = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;

            Vector3 moveForce = new Vector3(dirX, 0, dirZ);
            rb.AddForce(moveForce,ForceMode.Force);
        }

        //rb.velocity = new Vector3(dirX, 0, dirZ);
    }
}
