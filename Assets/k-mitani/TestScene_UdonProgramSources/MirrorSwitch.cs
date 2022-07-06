
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class MirrorSwitch : UdonSharpBehaviour
{
    public GameObject mirror;

    public override void Interact()
    {
        mirror.SetActive(!mirror.activeSelf);
    }
}
