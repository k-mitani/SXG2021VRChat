
using System;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class TuyuJaguchi : UdonSharpBehaviour
{
    private ParticleSystem particle;
    private bool isOpen;
    private float openDuration;
    public GameObject tuyu1;
    public GameObject tuyu2;
    public GameObject tuyu3;
    public GameObject tuyu4;
    public GameObject tuyu5;
    private GameObject[] tuyus;
    private Donburi donburi;
    public int currentTuyuLevel = 0;

    void Start()
    {
        particle = GetComponent<ParticleSystem>();
        tuyus = new[] { tuyu1, tuyu2, tuyu3, tuyu4, tuyu5 };

        donburi = transform.parent.Find("Donburi").GetComponent<Donburi>();
    }

    internal void OnEat()
    {
        foreach (var tuyu in tuyus)
        {
            tuyu.SetActive(false);
        }
        currentTuyuLevel = 0;
        openDuration = 0;
        donburi.GetComponent<ParticleSystem>().Stop();
    }

    public override void Interact()
    {
        if (isOpen)
        {
            Close();
            SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Close");
        }
        else
        {
            Open();
            SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Open");
        }
    }

    public void Open()
    {
        var handle = transform.Find("Handle");
        handle.rotation = Quaternion.Euler(Vector3.up * 90);
        particle.Play();
        isOpen = true;
        openDuration = 0;
    }
    public void Close()
    {
        var handle = transform.Find("Handle");
        handle.rotation = Quaternion.Euler(Vector3.up * 0);
        particle.Stop();
        isOpen = false;
    }

    private void Update()
    {
        if (isOpen)
        {
            openDuration += Time.deltaTime;
            if (openDuration > 1)
            {
                openDuration = 0;
                if (currentTuyuLevel > 0) tuyus[currentTuyuLevel - 1].SetActive(false);
                tuyus[currentTuyuLevel].SetActive(true);
                if (currentTuyuLevel < tuyus.Length - 1) currentTuyuLevel++;

                var yuge = donburi.GetComponent<ParticleSystem>();
                if (!yuge.isPlaying) yuge.Play();
            }
        }
    }

}
