using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class PlayerController : MonoBehaviour
{
    public float WalkSpeed = 350F;
    public float RunSpeed = 650F;
    public float SneakSpeed = 100F;
    private bool grounded = false;
    private float CurrentSpeed = 250F;
    private bool Sneaking = false;
    private bool SneakingCamera = false;
    private float SideSpeed = 350F;
    
    private static PlayerController Instance { get; set; }

    public static PlayerController getInstance()
    {
        return Instance;
    }

    private GameObject oldHighlightedGameObject;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (GameManager.Playing && !GameManager.InGui)
        {
            grounded = (CollisionFlags.CollidedBelow != 0);
            // MOVEMENT START
            Rigidbody rb = GetComponent<Rigidbody>();
            if (GameManager.cursorLocked())
            {

                float x = 0f;
                float y = 0f;
                float z = 0f;

                if (Input.GetKeyDown(GameManager.getInstance().settings.KeySprint))
                {
                    CurrentSpeed = RunSpeed;
                }

                if (Input.GetKeyUp(GameManager.getInstance().settings.KeySprint))
                {
                    CurrentSpeed = WalkSpeed;
                }

                if (Input.GetKeyDown(GameManager.getInstance().settings.KeySneak))
                {
                    CurrentSpeed = SneakSpeed;
                    Sneaking = true;
                    SideSpeed = SneakSpeed;
                }

                if (Input.GetKeyUp(GameManager.getInstance().settings.KeySneak))
                {
                    CurrentSpeed = WalkSpeed;
                    Sneaking = false;
                    SideSpeed = WalkSpeed;
                }

                float DeltaTime = Time.deltaTime;

                if (Input.GetKey(GameManager.getInstance().settings.KeyForward)) z += 0.01f * CurrentSpeed * DeltaTime;
                if (Input.GetKey(GameManager.getInstance().settings.KeyBackward)) z -= 0.01f * SideSpeed * DeltaTime;
                if (Input.GetKey(GameManager.getInstance().settings.KeyLeft)) x -= 0.01f * SideSpeed * DeltaTime;
                if (Input.GetKey(GameManager.getInstance().settings.KeyRight)) x += 0.01f * SideSpeed * DeltaTime;


                if (grounded)
                {
                    if (GetComponent<Rigidbody>().velocity.y > -0.2 && GetComponent<Rigidbody>().velocity.y < 0.1 && Input.GetKeyDown(GameManager.getInstance().settings.KeyJump))
                    {
                        y += 1000000 * DeltaTime;
                    }
                }

                Vector3 vec = new Vector3(x, 0, z);
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
                Cursor.visible = false;

                Camera camera = transform.Find("Camera").GetComponent<Camera>();

                Transform cameraTransform = camera.GetComponent<Transform>();

                float cX = Input.GetAxis("Mouse X") * 2;
                float cY = Input.GetAxis("Mouse Y") * 2;

                cY = Mathf.Clamp(cY, -90, 90);
                cX = Mathf.Clamp(cX, -360, 360);

                cameraTransform.localEulerAngles += new Vector3(-cY, cX);

                var euler = cameraTransform.localEulerAngles;
                if (cameraTransform.localEulerAngles.x > 90 && cameraTransform.localEulerAngles.x < 270) euler.x = 90;
                else if (cameraTransform.localEulerAngles.x < -90) euler.x = -90;
                cameraTransform.localEulerAngles = euler;

                if (Sneaking && !SneakingCamera)
                {
                    SneakingCamera = true;
                    cameraTransform.localPosition = cameraTransform.localPosition + new Vector3(0, (float) -0.3, 0);
                }

                if (!Sneaking && SneakingCamera)
                {
                    SneakingCamera = false;
                    cameraTransform.localPosition = cameraTransform.localPosition + new Vector3(0, (float) 0.3, 0);
                }

            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = false;
            }

            // CAMERA END

            // BLOCK MANAGEMENT START

            //REALLY BAD CODE
            Camera cameracamera = transform.Find("Camera").GetComponent<Camera>();

            Transform cameraTransformTransform = cameracamera.GetComponent<Transform>();

            var eulereuler = cameraTransformTransform.localEulerAngles;
            cameraTransformTransform.localEulerAngles = eulereuler;
            Vector3 worldDirection = Quaternion.Euler(eulereuler) * transform.forward;
            //NEVER USE THIS CODE AGAIN

            GameObject obj = null;
            if (GameManager.cursorLocked())
            {
                RaycastHit hit;
                if (Physics.Raycast(cameraTransformTransform.position, transform.TransformDirection(-worldDirection),
                    out hit, 5F))
                {

                    obj = hit.collider.gameObject;
                }


                if (obj != null && obj.GetComponent<BlockHolder>() != null)
                {

                    if (oldHighlightedGameObject != null && obj != oldHighlightedGameObject)
                    {
                        oldHighlightedGameObject.GetComponent<BlockHolder>().showHighlight(false);
                    }

                    obj.GetComponent<BlockHolder>().showHighlight(true);

                    oldHighlightedGameObject = obj;

                    if (Input.GetMouseButtonDown(0))
                    {
                        GameManager.getCurrentWorld().destroyBlockAt(obj.GetComponent<BlockHolder>().getPos());
                    }
                    else if (Input.GetMouseButtonDown(1))
                    {

                    }
                }
            }
        }
    }

    
}
