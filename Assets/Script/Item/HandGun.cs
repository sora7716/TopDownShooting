using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGun : GunBase
{
    //発射済みか否か
    bool fired_ = false;
    //トリガーを離したらfalse;
    public override void OffTrigger()
    {
        fired_ = false;
    }

    public override void OnTrigger()
    {
        //発射間隔以内なら早期リターン
        if (shotTimer_ > 0)
        {
            return;
        }
        //トリガー引きっぱなしなら早期リターン
        if (fired_) { return; }
        //発射フラグON
        fired_ = true;
        //タイマーリセット
        shotTimer_ = fireRate_;
        //muzzleの正面にレイを飛ばす
        Ray ray = new Ray
            (
            muzzleTransform_.position,
            muzzleTransform_.forward
            );
        RaycastHit raycastHit;
        //Itemレイヤーを無視する。LayerMask.GetMaskでその
        //レイヤーのビットが1となっている値を取得した後、
        //~でビット反転を行う
        int layerMask = ~LayerMask.GetMask
            (
            new string[] { "Item" }
            );
        //レイの長さは雑に100mとする
        float rayLength = 100;
        //レイの終点はひとまず最大に
        Vector3 endPoint = muzzleTransform_.position + muzzleTransform_.forward * rayLength;
        if (Physics.Raycast(ray, out raycastHit, rayLength, layerMask))
        {
            //衝突地点にレイを短縮
            endPoint = raycastHit.point;
            //対象がHelthコンポーネントを所持しているか確認 
            Health healthComponent;
            bool hasHealth = raycastHit.collider.TryGetComponent(out healthComponent);
            //持っていたらダメージを与える
            if (hasHealth)
            {
                healthComponent.Damage(power_);
            }
        }
        //銃弾を生成・RayBulletコンポーネントの取得
        GameObject bulletObject = Instantiate(bulletPrefab_.gameObject, muzzleTransform_.position, muzzleTransform_.rotation);
        RayBullet bullet = bulletObject.GetComponent<RayBullet>();
        //描画するLineの始点と終点を設定
        bullet.SetPosition(muzzleTransform_.position, endPoint);

    }
}
