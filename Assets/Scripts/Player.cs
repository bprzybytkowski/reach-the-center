﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {

    [SerializeField] float rotateSpeed = 200f;
    [SerializeField] float shootSpeed = 10f;

    bool canRotate = true;
    bool canShoot = true;
    float controlThrow = 0f;
    Rigidbody2D rb;


    // Start is called before the first frame update
    void Start() {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        if (canRotate) {
            controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        }
        transform.RotateAround(Vector3.zero, Vector3.forward, controlThrow * Time.fixedDeltaTime * -rotateSpeed);
        if (CrossPlatformInputManager.GetButtonDown("Jump") && canShoot) {
            Shoot();
        }
    }

    void StopMoving() {
        controlThrow = 0;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0;
    }

    void Shoot() {
        canRotate = false;
        canShoot = false;
        StopMoving();
        rb.velocity = Vector2.zero;
        Vector3 shootDirection = (Vector3.zero - rb.transform.position).normalized;
        rb.velocity = shootDirection * shootSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Circle")) {
            StopMoving();
            FindObjectOfType<CameraShake>().StartShake(-1);
            FindObjectOfType<Counter>().StopCounting();
            RestartLevel();
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Center")) {
            StopMoving();
            rb.transform.position = Vector3.zero;
        }
    }

    private void RestartLevel() {
        FindObjectOfType<GameSession>().ProcessLostLive();
    }

}
