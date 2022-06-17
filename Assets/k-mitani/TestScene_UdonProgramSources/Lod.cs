
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class Lod : UdonSharpBehaviour
{
    public override void OnPickup()
    {
        var hook = transform.parent.Find("FishingHook");
        Networking.SetOwner(Networking.LocalPlayer, hook.gameObject);
    }
}
