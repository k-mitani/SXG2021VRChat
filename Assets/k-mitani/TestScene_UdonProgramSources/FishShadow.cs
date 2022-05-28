
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class FishShadow : UdonSharpBehaviour
{
    public Transform target;
    public float power;
    private bool isMoving;
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!isMoving)
        {
            isMoving = true;
            if (target != null)
            {
                transform.LookAt(target);
            }
            else
            {
                transform.Rotate(Vector3.up * 360 * Random.value);
            }
            rb.velocity = transform.forward * power;
        }
        else
        {
            if (rb.velocity.magnitude == 0)
            {
                isMoving = false;
            }
        }

    }
}
