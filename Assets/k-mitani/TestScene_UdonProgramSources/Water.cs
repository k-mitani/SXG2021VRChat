
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
        else
        {
            var seg = other.gameObject.GetComponent<UdonMenSegment>();
            if (seg != null)
            {
                seg.OnEnterWater();
            }
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
        else
        {
            var seg = other.gameObject.GetComponent<UdonMenSegment>();
            if (seg != null)
            {
                seg.OnExitWater();
            }
        }
    }
}
