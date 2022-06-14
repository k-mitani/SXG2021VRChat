
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class Bullet : UdonSharpBehaviour
{
    private Rigidbody rb;
    private ParticleSystem particle;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        particle = GetComponent<ParticleSystem>();
    }

    internal void OnShoot(Gun gun)
    {
        transform.position = gun.transform.position + gun.transform.forward * 0.2f;
        transform.rotation = gun.transform.rotation;
        rb.angularVelocity = Vector3.zero;
        rb.velocity = gun.transform.forward * 5;
        rb.transform.Find("Paint").gameObject.SetActive(false);
        particle.Stop();
        SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "HidePaint");
        RequestSerialization();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Networking.IsOwner(gameObject))
        {
            transform.forward = -collision.contacts[0].normal;

            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            particle.Play();
            var paint = transform.Find("Paint");
            paint.gameObject.SetActive(true);
            transform.Rotate(Vector3.forward * Random.value * 360);
            SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "ShowPaint");
        }
    }

    public void ShowPaint()
    {
        particle.Play();
        var paint = transform.Find("Paint");
        paint.gameObject.SetActive(true);
        transform.Rotate(Vector3.forward * Random.value * 360);
    }

    public void HidePaint()
    {
        particle.Stop();
        transform.Find("Paint").gameObject.SetActive(false);
    }
}
