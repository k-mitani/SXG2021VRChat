
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class Yukiri : UdonSharpBehaviour
{
    public UdonTamaManager manager;
    private Transform udonDropPoint;
    void Start()
    {
        udonDropPoint = transform.Find("UdonDropPoint");
    }

    public override void OnPickupUseDown()
    {
        DropUdonTama();
        SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "DropUdonTama");
    }

    public override void OnPickup()
    {
        if (!Networking.LocalPlayer.IsUserInVR())
        {
            var rot = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(0, rot.y, 0);
        }
    }

    public override void OnDrop()
    {
        if (!Networking.LocalPlayer.IsUserInVR())
        {
            var rot = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(-180, rot.y, 0);
        }
    }

    public void DropUdonTama()
    {
        var tama = manager.GetNewUdonTama();
        for (int i = 0; i < tama.childCount; i++)
        {
            var men = tama.GetChild(i);
            for (int j = 0; j < men.childCount; j++)
            {
                var seg = men.GetChild(j).GetComponent<Rigidbody>();
                seg.MovePosition(udonDropPoint.position + Vector3.up * 0.03f * i);
                Networking.SetOwner(Networking.LocalPlayer, seg.gameObject);
            }
        }
    }
}
