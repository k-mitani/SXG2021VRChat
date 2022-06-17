
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class SearchRange : UdonSharpBehaviour
{
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals("FishingHook"))
        {
            var hook = other.GetComponent<FishingHook>();
            //var isInWater = hook.isInWater;
            var isOwnedHook = Networking.IsOwner(Networking.LocalPlayer, hook.gameObject);
            if (isOwnedHook)
            {
                var shadow = transform.parent.GetComponent<FishShadow>();
                shadow.target = hook;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        
    }
}
