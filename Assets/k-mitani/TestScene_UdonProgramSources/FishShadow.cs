
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class FishShadow : UdonSharpBehaviour
{
    public FishingHook target;
    public float power;
    public bool isHooked = false;
    public float hookHpMax = 1.3f;
    public float hookHp; // 食いつきまでの我慢ゲージ的なもの。0になったら必ず食いつく。
    public float hideTime;
    private Vector3 initialPosition;

    private bool isMoving;

    private Rigidbody rb;
    void Start()
    {
        hookHp = hookHpMax;
        rb = GetComponent<Rigidbody>();
        initialPosition = transform.position;
    }

    private void Update()
    {
        if (hideTime > 0)
        {
            hideTime -= Time.deltaTime;
            if (hideTime < 0)
            {
                transform.position = initialPosition;
            }
        }
        else if (!isMoving)
        {
            isMoving = true;


            if (target != null && target.isInWater)
            {
                transform.LookAt(target.transform);
                if (isHooked)
                {
                    target.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
                }
            }
            else
            {
                isHooked = false;
                hookHp = hookHpMax;
                transform.rotation = Quaternion.identity;
                transform.Rotate(Vector3.up * 360 * Random.value);
            }
            rb.velocity = transform.forward * power;
        }
        else
        {
            var thresh = 0.01;
            if (isHooked) thresh = 0.1;
            if (rb.velocity.magnitude < thresh)
            {
                isMoving = false;
            }
        }
        // 釣り竿にぶつかって水中からはじき出されたりしないように
        // 高さ移動範囲を制限する。
        // 面倒なので数値は決め打ちにしておく。
        var p = transform.position;
        p.y = Mathf.Min(p.y, 0.634f);
        transform.position = p;
    }

    internal void OnCatched()
    {
        isHooked = false;
        hookHp = hookHpMax;
        hideTime = 3;
        // とりあえず適当な座標に飛ばして隠したことにする。
        transform.position = Vector3.up * -10;
    }

    public void OnCollideWithHook(FishingHook hook)
    {
        hookHp -= 0.3f;
        if (Random.value > hookHp && !isHooked)
        {
            isHooked = true;

            // 追加済みか調べる
            var lastEmptySlot = -1;
            for (int i = 0; i < hook.hookedFishes.Length; i++)
            {
                if (hook.hookedFishes[i] == this) return;
                if (hook.hookedFishes[i] == null) lastEmptySlot = i;
            }
            if (lastEmptySlot != -1) hook.hookedFishes[lastEmptySlot] = this;
        }
    }
}
