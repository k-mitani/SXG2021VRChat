
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class Donburi : UdonSharpBehaviour
{
    public GameObject udonTama;
    public TuyuJaguchi jaguchi;

    void Start()
    {
        
    }

    public override void Interact()
    {
        Eat();
        SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Eat");
    }

    public void Eat()
    {
        udonTama.SetActive(false);
        jaguchi.OnEat();
    }
}
