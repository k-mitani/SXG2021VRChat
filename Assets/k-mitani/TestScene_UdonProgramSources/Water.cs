
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class Water : UdonSharpBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("FishingHook"))
        {
            Debug.Log("EnterWater");
            var hook = other.gameObject.GetComponent<FishingHook>();
            hook.OnEnterWater();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Equals("FishingHook"))
        {
            Debug.Log("ExitWater");
            var hook = other.gameObject.GetComponent<FishingHook>();
            hook.isInWater = false;
            hook.OnExitWater();
        }
    }
}
