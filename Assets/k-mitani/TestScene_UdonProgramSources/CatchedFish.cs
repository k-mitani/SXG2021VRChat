
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class CatchedFish : UdonSharpBehaviour
{
    public FishingHook hook;
    public float hookOffset;
    private Quaternion hookRotation;
    public bool isPickedUp = false;

    private void Update()
    {
        // 初めてピックアップされるまではHookに追従する。
        if (hook != null && !isPickedUp)
        {
            transform.position = hook.transform.position + transform.forward * hookOffset;
        }
    }

    public override void OnPickup()
    {
        isPickedUp = true;
        SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "PickedUp");
    }
    public void PickedUp()
    {
        isPickedUp = true;
    }
}
