
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class CatchedFish : UdonSharpBehaviour
{
    public FishingHook hook;
    public Vector3 hookOffset;
    public bool isPickedUp = false;

    private void Update()
    {
        // 初めてピックアップされるまではHookに追従する。
        if (hook != null && !isPickedUp)
        {
            transform.position = hook.transform.position + hookOffset;
        }
    }

    public override void OnPickup()
    {
        isPickedUp = true;
    }
}
