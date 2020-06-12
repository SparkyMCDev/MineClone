using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool colliding = true;

    private void Update()
    {

        // MOVEMENT START

        if (GameManager.cursorLocked())
        {

            float x = 0f;
            float y = 0f;
            float z = 0f;

            if (Input.GetKey(KeyCode.W)) z += 0.01f;
            if (Input.GetKey(KeyCode.S)) z -= 0.01f;
            if (Input.GetKey(KeyCode.A)) x -= 0.01f;
            if (Input.GetKey(KeyCode.D)) x += 0.01f;

            if (colliding && Input.GetKeyDown(KeyCode.Space)) { y += 500f; }

            Vector3 vec = new Vector3(x, 0, z);
            Transform cam = transform.Find("Camera");
            

            vec = cam.TransformDirection(vec);
            vec.y = 0;

            transform.position += vec;


            Rigidbody rb = GetComponent<Rigidbody>();
            rb.AddForce(new Vector3(0, y, 0));
        }

        // MOVEMENT END

        // CAMERA START

        if (GameManager.cursorLocked())
        {
            Cursor.lockState = CursorLockMode.Locked;

            Camera camera = transform.Find("Camera").GetComponent<Camera>();

            Transform cameraTransform = camera.GetComponent<Transform>();

            float cX = Input.GetAxis("Mouse X")*2;
            float cY = Input.GetAxis("Mouse Y")*2;

            cY = Mathf.Clamp(cY, -90, 90);

            cameraTransform.localEulerAngles += new Vector3(-cY, cX);

            var euler = cameraTransform.localEulerAngles;
            if (cameraTransform.localEulerAngles.x > 90&&cameraTransform.localEulerAngles.x < 270) euler.x = 90;
            else if (cameraTransform.localEulerAngles.x < -90) euler.x = -90;
            cameraTransform.localEulerAngles = euler;
        } else
        {
            Cursor.lockState = CursorLockMode.None;
        }

        // CAMERA END

        // BLOCK MANAGEMENT START

        if(GameManager.cursorLocked())
        {

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        colliding = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        colliding = false;
    }
}
