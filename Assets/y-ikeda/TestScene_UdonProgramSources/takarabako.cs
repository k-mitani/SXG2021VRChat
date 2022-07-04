
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class takarabako : UdonSharpBehaviour
{
    [SerializeField] private GameObject Canvas;

    // 宝箱が開いているか
    private bool open = false;

    void Start()
    {
        
    }
    // 触ったとき（インタラクトした時）に動作
    public override void Interact()
    {
        // transformを取得
        Transform myTransform = this.transform;

        if(open == false)
        {
            // 現在の回転量へ加算
            myTransform.Rotate(0.0f, 120.0f, 0.0f);
            open = true;
        }
        else
        {
            // 現在の回転量へ加算
            myTransform.Rotate(0.0f, -120.0f, 0.0f);
            open = false;
        }
    }
}
