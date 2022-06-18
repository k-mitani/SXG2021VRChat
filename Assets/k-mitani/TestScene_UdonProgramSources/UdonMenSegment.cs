
using UdonSharp;
using UnityEngine;

public class UdonMenSegment : UdonSharpBehaviour
{
    public bool isInWater = false;
    public bool isWet = false;
    public float yukiriSpeedThreshold = 0.2f;
    public float floatingForce = 0.1f;
    public float forceProbability = 0.1f;
    private ParticleSystem particle;
    private Rigidbody rb;
    private float prevSpeed;


    private void Start()
    {
        particle = GetComponent<ParticleSystem>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!isWet && particle.isPlaying)
        {
            particle.Stop();
        }

        var speed = rb.velocity.magnitude;
        if (isWet && !isInWater)
        {
            var diff = Mathf.Abs(speed - prevSpeed);
            if (diff > yukiriSpeedThreshold)
            {
                OnYukiriCompleted();
                SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "OnYukiriCompleted");
            }
        }
        prevSpeed = speed;

        if (isInWater)
        {
            if (Random.value < forceProbability)
            {
                rb.AddForce(Vector3.up * floatingForce, ForceMode.Force);
            }
        }
    }

    public void OnYukiriCompleted()
    {
        isWet = false;
        particle.Stop();
    }



    internal void OnEnterWater()
    {
        isInWater = true;
        isWet = true;
        particle.Stop();
    }

    internal void OnExitWater()
    {
        isInWater = false;
        if (isWet)
        {
            particle.Play();
        }
    }
}
