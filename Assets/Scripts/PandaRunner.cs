using UnityEngine;
using UnityEngine.InputSystem;

public class PandaRunner : MonoBehaviour
{
    public float runSpeed = 8f;
    public float strafeSpeed = 5f;
    public float jumpForce = 7f;
    public float trackRadius = 20f;
    public float maxOffset = 2.5f;

    public int lap = 1;
    public System.Action onDied;

    Rigidbody rb;
    float angle = 0f;
    float offset = 0f;
    bool grounded;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
    }

    // Update is called once per frame
    void Update()
    {
        var kb = Keyboard.current;
        if (kb == null) return;

        float h = 0f;
        if (kb.aKey.isPressed || kb.leftArrowKey.isPressed) h -= 1f;
        if (kb.dKey.isPressed || kb.rightArrowKey.isPressed) h += 1f;

        // Right of the panda is toward the center of the ring, so subtract
        offset = Mathf.Clamp(offset - h * strafeSpeed * Time.deltaTime, -maxOffset, maxOffset);

        bool tapped = Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame;

        if ((kb.spaceKey.wasPressedThisFrame || tapped) && grounded)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
            grounded = false;
        }
    }

   void FixedUpdate()
    {
        float r = trackRadius + offset;
        angle += (runSpeed / trackRadius) * Mathf.Rad2Deg * Time.fixedDeltaTime;

        if (angle >= 360f)
        {
            angle -= 360f;
            lap++;
            Debug.Log("Lap " + lap);
        }

        Vector3 flat = Quaternion.Euler(0f, angle, 0f) * new Vector3(0f, 0f, r);
        rb.MovePosition(new Vector3(flat.x, rb.position.y, flat.z));

        // +90 so the panda faces along the track instead of outward
        rb.MoveRotation(Quaternion.Euler(0f, angle + 90f, 0f));
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Obstacle>() != null) {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("Hit! Final lap: " + lap);
        angle = 0f;
        offset = 0f;
        lap = 1;
        onDied?.Invoke();
    }

    void OnCollisionStay(Collision c)
    {
        grounded = true;
    }
}
