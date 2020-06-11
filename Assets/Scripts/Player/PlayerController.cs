﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool jumping = false;

    private void Update()
    {
        float x = 0f;
        float y = 0f;
        float z = 0f;

        if (Input.GetKey(KeyCode.W)) z += 0.01f;
        if (Input.GetKey(KeyCode.S)) z -= 0.01f;
        if (Input.GetKey(KeyCode.A)) x -= 0.01f;
        if (Input.GetKey(KeyCode.D)) x += 0.01f;

        if (!jumping&&Input.GetKeyDown(KeyCode.Space)) { y += 1f;jumping = true; }

        Vector3 vec = new Vector3(x, 0, z);

        vec = Camera.main.transform.TransformDirection(vec);

        transform.position += vec;


        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(0, y, 0));
    }

    private void OnCollisionEnter(Collision collision)
    {
        jumping = false;
    }
}
