
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
            Networking.IsOwner(Networking.LocalPlayer, other.transform.parent.gameObject);
            var shadow = transform.parent.GetComponent<FishShadow>();
            shadow.target = other.transform;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        
    }
}
