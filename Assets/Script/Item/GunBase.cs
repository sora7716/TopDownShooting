using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GunBase : MonoBehaviour
{
    [SerializeField]
    //発砲するプレハブ
    protected RayBullet bulletPrefab_;
    [SerializeField]
    //銃口のトランスフォーム。発射位置
    protected Transform muzzleTransform_;
    [SerializeField]
    //連射感覚
    protected float fireRate_ = 0;
    //発射タイマー
    protected float shotTimer_ = 0;
    [SerializeField]
    //威力
    protected float power_ = 0;
    [SerializeField]
    //取得時前に回転している速度
    private float itemRotateSpeedDeg_ = 90f;

    //発射入力があったら
    public abstract void OnTrigger();
    //発射入力が無かったら    
    public abstract void OffTrigger();

    private void ItemRotate()
    {
        //地面に落ちている間の回転処理
        transform.RotateAround
            (
            transform.position,
            Vector3.up,
            itemRotateSpeedDeg_ * Time.deltaTime
            );
    }

    public bool GetIsAlone()
    {
        //親がいなかったら誰にも所持されていない
        return transform.parent == null;
    }

    public virtual void Update()
    {
        //所持されていなかったら回転する
        if (GetIsAlone()) { ItemRotate(); }
        //タイマーの更新
        if (shotTimer_ <= 0) { return; }
        shotTimer_ -= Time.deltaTime;
    }
}
