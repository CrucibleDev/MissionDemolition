using UnityEngine;

public class RigidbodySleep : MonoBehaviour
{
    private int sleepCountdown = 4;
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (sleepCountdown > 0)
        {
            rb.Sleep();
            sleepCountdown--;
        }
    }
}
