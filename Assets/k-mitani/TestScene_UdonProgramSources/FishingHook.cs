
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class FishingHook : UdonSharpBehaviour
{
    public bool isInWater = false;
    public FishShadow[] hookedFishes = new FishShadow[10];

    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        var shadow = collision.gameObject.GetComponent<FishShadow>();
        if (shadow != null)
        {
            shadow.OnCollideWithHook(this);

            // 食いついていたら大きめのパーティクルを使う。
            if (shadow.isHooked)
            {
                // やっぱりFishShadow側で行うことにする。
                // transform.GetChild(0).GetComponent<ParticleSystem>().Play();
            }
            else
            {
                GetComponent<ParticleSystem>().Play();

            }
        }
    }

    public void OnEnterWater()
    {
        isInWater = true;
    }

    public void OnExitWater()
    {
        isInWater = false;
        var fishManager = GameObject.Find("FishManager").GetComponent<FishManager>();

        // かかっている魚がいれば、その数分釣った魚を生成（疑似生成）する。
        for (int i = 0; i < hookedFishes.Length; i++)
        {
            var fish = hookedFishes[i];
            if (fish != null)
            {
                fish.OnCatched();
                hookedFishes[i] = null;
                var catchedFish = fishManager.GetNewFish();
                catchedFish.transform.Rotate(Vector3.up * Random.value * 360);
                catchedFish.hook = this;
                catchedFish.isPickedUp = false;
                Networking.SetOwner(Networking.LocalPlayer, catchedFish.gameObject);
            }
        }
    }
}
