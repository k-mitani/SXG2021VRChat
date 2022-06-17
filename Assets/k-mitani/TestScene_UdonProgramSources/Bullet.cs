
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class Bullet : UdonSharpBehaviour
{
    private Rigidbody rb;
    private ParticleSystem particle;
    private new SphereCollider collider;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        particle = GetComponent<ParticleSystem>();
        collider = GetComponent<SphereCollider>();
    }

    internal void OnShoot(Gun gun)
    {
        // 着弾時に他のペイント弾とぶつからないようにしていたのを元に戻す。
        collider.enabled = true;

        transform.position = gun.transform.position + gun.transform.forward * 0.2f;
        transform.rotation = gun.transform.rotation;
        rb.angularVelocity = Vector3.zero;
        rb.velocity = gun.transform.forward * 6;
        rb.transform.Find("Paint").gameObject.SetActive(false);
        particle.Stop();
        SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "HidePaint");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Networking.IsOwner(gameObject))
        {
            transform.forward = -collision.contacts[0].normal;

            // 他のペイント弾とぶつからないようにする。
            collider.enabled = false;

            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            particle.Play();
            var paint = transform.Find("Paint");
            paint.gameObject.SetActive(true);
            transform.Rotate(Vector3.forward * Random.value * 360);
            SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "ShowPaint");
        }
    }

    // 20220617 なぜか動かないのでとりあえずコメントアウト
    //public override void OnPlayerCollisionEnter(VRCPlayerApi player)
    //{
    //    if (Networking.IsOwner(gameObject))
    //    {
    //        particle.Play();
    //        gameObject.SetActive(false);
    //        SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "HitPlayer");
    //    }
    //}
    //public void HitPlayer()
    //{
    //    particle.Play();
    //    gameObject.SetActive(false);
    //}

    public void ShowPaint()
    {
        // 他のペイント弾とぶつからないようにする。
        collider.enabled = false;

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        particle.Play();
        var paint = transform.Find("Paint");
        paint.gameObject.SetActive(true);
        transform.Rotate(Vector3.forward * Random.value * 360);
    }

    public void HidePaint()
    {
        gameObject.SetActive(true);

        rb.isKinematic = false;
        particle.Stop();
        transform.Find("Paint").gameObject.SetActive(false);
    }
}
