using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {

    [SerializeField] float rotateSpeed = 200f;
    [SerializeField] float shootSpeed = 10f;

    bool controlledByTouch = false;
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
        if (canRotate && !controlledByTouch) {
            controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        }
        transform.RotateAround(Vector3.zero, Vector3.forward, controlThrow * Time.fixedDeltaTime * -rotateSpeed);
        if (CrossPlatformInputManager.GetButtonDown("Jump")) {
            Shoot();
        }
    }

    void StopMoving() {
        controlThrow = 0;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0;
    }

    public void Shoot() {
        if (canShoot) {
            canRotate = false;
            canShoot = false;
            StopMoving();
            rb.velocity = Vector2.zero;
            Vector3 shootDirection = (Vector3.zero - rb.transform.position).normalized;
            rb.velocity = shootDirection * shootSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Circle")) {
            StopMoving();
            FindObjectOfType<CameraShake>().StartShake(-1);
            FindObjectOfType<Counter>().StopCounting();
            FindObjectOfType<GameSession>().ProcessLostLife();
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Center")) {
            StopMoving();
            rb.transform.position = Vector3.zero;
        }
    }

    public void SetControlledByTouch(bool controlledByTouch) {
        this.controlledByTouch = controlledByTouch;
    }

    public void SetControlThrow(float controlThrow) {
        if (canRotate) {
            this.controlThrow = controlThrow;
        }
    }

}
