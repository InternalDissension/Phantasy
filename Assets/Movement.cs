using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    //Forward Back Strafe
    //Rotate sideways
    //Restrict up and down movement

    public float speed;
    public float rotSpeed;
    public float swapSpeed;

    public GameObject head;

    private float mouseX;
    private float mouseY;

    Vector3 targetRot;

    private float ground;

    private void Start()
    {
        ground = transform.position.y;
        targetRot = transform.rotation.eulerAngles;
        head.transform.rotation = Quaternion.Euler(Vector3.zero);
    }

    private void Update()
    {
        bool forward    = Input.GetKey(KeyCode.W);
        bool backward   = Input.GetKey(KeyCode.S);
        bool left       = Input.GetKey(KeyCode.A);
        bool right      = Input.GetKey(KeyCode.D);
        bool rightClick = Input.GetKeyDown(KeyCode.Mouse1);

        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        directionalMovement(forward, backward, left, right);
        swapRealm(rightClick);
        if (!Input.GetKey(KeyCode.LeftAlt))
        {
            horizontalRot();
            verticalRot();
        }

        if (transform.rotation.eulerAngles.z != targetRot.z)
        {
            Debug.Log(targetRot);
            realmRotation();
        }

        checkGrounded();

        }

    private void directionalMovement(bool forward, bool backward, bool left, bool right)
    {
        if (forward)
        {
            transform.position = Vector3.Lerp(transform.position, transform.position + transform.forward, 1 * speed * Time.deltaTime);
        }

        else if (backward)
        {
            transform.position = Vector3.Lerp(transform.position, transform.position - transform.forward, 1 * speed * Time.deltaTime);
        }

        if (left)
        {
            transform.position = Vector3.Lerp(transform.position, transform.position - transform.right, 1 * speed * Time.deltaTime);
        }

        if (right)
        {
            transform.position = Vector3.Lerp(transform.position, transform.position + transform.right, 1 * speed * Time.deltaTime);
        }
    }

    private void horizontalRot()
    {
        transform.Rotate(0, 1 * mouseX * rotSpeed * Time.deltaTime, 0);
    }

    private void verticalRot()
    {
        head.transform.Rotate(-mouseY * rotSpeed * Time.deltaTime, 0 ,0);
    }

    private void realmRotation()
    {
        Vector3 curRot = transform.rotation.eulerAngles;
        Vector3 angle = new Vector3(0, 0, 1);

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(targetRot), swapSpeed * Time.deltaTime);

        if (Quaternion.Angle(transform.rotation, Quaternion.Euler(targetRot)) < 1f)
            transform.rotation = Quaternion.Euler(targetRot);
    }

    private void swapRealm(bool rightClick)
    {
        float rotY = transform.rotation.eulerAngles.y;

        if (rightClick)
        {
            targetRot.z = targetRot.z > 0 ? 0 : 180;
        }

        targetRot = new Vector3(0, rotY, targetRot.z);
    }

    private void checkGrounded()
    {
        Vector3 floor = new Vector3(transform.position.x, ground, transform.position.z);

        if (transform.position.y != ground)
        {
            Vector3.Lerp(transform.position, floor , 0.3f);

            if (Vector3.Distance(transform.position, floor) < 1f)
                transform.position = floor;
        }
    }
}
