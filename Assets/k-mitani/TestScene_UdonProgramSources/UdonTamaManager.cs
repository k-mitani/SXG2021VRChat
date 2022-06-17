
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class UdonTamaManager : UdonSharpBehaviour
{
    public int nextNewUdonTamaIndex = 0;

    public Transform GetNewUdonTama()
    {
        var udonTama = transform.GetChild(nextNewUdonTamaIndex);
        nextNewUdonTamaIndex = (nextNewUdonTamaIndex + 1) % transform.childCount;
        return udonTama;
    }

    public void SetOwner(VRCPlayerApi player)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var udonTama = transform.GetChild(i);
            Networking.SetOwner(player, udonTama.gameObject);
        }
    }
}
