
using System;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class GunBulletManager : UdonSharpBehaviour
{
    public int nextNewBulletIndex = 0;

    public Bullet GetNewBullet()
    {
        var bullet = transform.GetChild(nextNewBulletIndex).GetComponent<Bullet>();
        nextNewBulletIndex = (nextNewBulletIndex + 1) % transform.childCount;
        return bullet;
    }

    public void SetOwner(VRCPlayerApi player)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var bullet = transform.GetChild(i).GetComponent<Bullet>(); ;
            Networking.SetOwner(player, bullet.gameObject);
        }
    }
}
