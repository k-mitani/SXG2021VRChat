
using System;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class FishManager : UdonSharpBehaviour
{
    public int nextNewFishIndex = 0;

    public CatchedFish GetNewFish()
    {
        var fish = transform.GetChild(nextNewFishIndex).GetComponent<CatchedFish>();
        nextNewFishIndex = (nextNewFishIndex + 1) % transform.childCount;
        return fish;
    }
}
