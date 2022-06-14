
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class Gun : UdonSharpBehaviour
{
    public GunBulletManager manager;

    public override void OnPickup()
    {
        Networking.SetOwner(Networking.LocalPlayer, gameObject);
        manager.SetOwner(Networking.LocalPlayer);
    }

    public override void OnPickupUseDown()
    {
        var b = manager.GetNewBullet();
        b.OnShoot(this);
    }
}
