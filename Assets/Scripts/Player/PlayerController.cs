using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float WalkSpeed = 350F;
    public float RunSpeed = 650F;
    public float SneakSpeed = 100F;
    private bool grounded = false;
    private float CurrentSpeed = 250F;
    private bool Sneaking = false;
    private bool SneakingCamera = false;

    private void Update()
    {

        grounded = (CollisionFlags.CollidedBelow != 0);
        // MOVEMENT START
        Rigidbody rb = GetComponent<Rigidbody>();
        if (GameManager.cursorLocked())
        {

            float x = 0f;
            float y = 0f;
            float z = 0f;

            if(Input.GetKeyDown(KeyCode.LeftControl)) { CurrentSpeed = RunSpeed; }

            if(Input.GetKeyUp(KeyCode.LeftControl)) { CurrentSpeed = WalkSpeed; }

            if (Input.GetKeyDown(KeyCode.LeftShift)) { CurrentSpeed = SneakSpeed; Sneaking = true; }

            if (Input.GetKeyUp(KeyCode.LeftShift)) { CurrentSpeed = WalkSpeed; Sneaking = false; }

            if (Input.GetKey(KeyCode.W)) z += 0.01f * CurrentSpeed * Time.deltaTime;
            if (Input.GetKey(KeyCode.S)) z -= 0.01f * CurrentSpeed * Time.deltaTime;
            if (Input.GetKey(KeyCode.A)) x -= 0.01f * CurrentSpeed * Time.deltaTime;
            if (Input.GetKey(KeyCode.D)) x += 0.01f * CurrentSpeed * Time.deltaTime;


            if (grounded)
            {
                if (GetComponent<Rigidbody>().velocity.y > -0.2 && GetComponent<Rigidbody>().velocity.y < 0.1 && Input.GetKeyDown(KeyCode.Space)) { y += 500f; }
            }
            Vector3 vec = new Vector3(x, 0, z );
            Transform cam = transform.Find("Camera");

            float origX = cam.eulerAngles.x;

            var eu = cam.eulerAngles;


            eu.x = 0;

            cam.eulerAngles = eu;

            vec = cam.TransformDirection(vec);
            vec.y = 0;

            eu.x = origX;
            cam.eulerAngles = eu;

            transform.position += vec;

            
            
            rb.AddForce(new Vector3(0, y, 0));


        }

        rb.angularVelocity = Vector3.zero;

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
            cX = Mathf.Clamp(cX, -360, 360);

            cameraTransform.localEulerAngles += new Vector3(-cY, cX);

            var euler = cameraTransform.localEulerAngles;
            if (cameraTransform.localEulerAngles.x > 90&&cameraTransform.localEulerAngles.x < 270) euler.x = 90;
            else if (cameraTransform.localEulerAngles.x < -90) euler.x = -90;
            cameraTransform.localEulerAngles = euler;

            if(Sneaking && !SneakingCamera) { SneakingCamera = true; cameraTransform.localPosition = cameraTransform.localPosition + new Vector3(0, (float) -0.3, 0); }
            if(!Sneaking && SneakingCamera) { SneakingCamera = false; cameraTransform.localPosition = cameraTransform.localPosition + new Vector3(0, (float) 0.3, 0); }
        
        } else
        {
            Cursor.lockState = CursorLockMode.None;
        }

        // CAMERA END

        // BLOCK MANAGEMENT START

        if(GameManager.cursorLocked())
        {

            GameObject obj = null;

            if(obj!=null&&obj.GetComponent<BlockHolder>()!=null)
            {
                

                if(Input.GetMouseButtonDown(0))
                {
                    GameManager.getCurrentWorld().destroyBlockAt(obj.GetComponent<BlockHolder>().getPos());
                } else if(Input.GetMouseButtonDown(1))
                {
                    
                }
            }
        }
    }

}
