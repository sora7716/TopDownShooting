using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : GunBase
{
    private Vector3 direction_;//飛んでいく方向
    [SerializeField] private float dispersion_;//分散する度合い
    //発射済みか否か
    bool fired_ = false;
    bool isFinishe_ = false;
    //トリガーを離したらfalse;
    public override void OffTrigger()
    {
        fired_ = false;
        isFinishe_ = false;
    }

    public override void OnTrigger()
    {
        // 発射間隔以内なら早期リターン
        if (shotTimer_ > 0)
        {
            return;
        }
        // トリガー引きっぱなしなら早期リターン
        if (fired_) { return; }
        // 発射フラグON
        fired_ = true;
        // タイマーリセット
        shotTimer_ = fireRate_;

        // 各マズルから複数の弾を発射
        foreach (var muzzleTransform in muzzleTransforms_)
        {
            // ショットガンの散弾数
            int pelletCount = muzzleTransforms_.Count; // 例: 10発の散弾を発射
            for (int i = 0; i < pelletCount; i++)
            {
                // ランダムな方向を計算
                Vector3 randomDirection = muzzleTransform.forward + new Vector3
                (
                    Random.Range(-dispersion_, dispersion_),
                    Random.Range(-dispersion_, dispersion_),
                    Random.Range(-dispersion_, dispersion_)
                );

                // レイを飛ばす
                Ray ray = new Ray(muzzleTransform.position, randomDirection.normalized);
                RaycastHit raycastHit;

                // Itemレイヤーを無視する
                int layerMask = ~LayerMask.GetMask(new string[] { "Item" });
                float rayLength = 100;
                Vector3 endPoint = muzzleTransform.position + randomDirection.normalized * rayLength;

                if (Physics.Raycast(ray, out raycastHit, rayLength, layerMask))
                {
                    // 衝突地点を計算
                    endPoint = raycastHit.point;

                    // 衝突したオブジェクトが Health コンポーネントを持っていればダメージを与える
                    if (raycastHit.collider.TryGetComponent<Health>(out Health healthComponent))
                    {
                        healthComponent.Damage(power_);
                    }
                }

                // 弾丸を生成して描画
                GameObject bulletObject = Instantiate(bulletPrefab_.gameObject, muzzleTransform.position, Quaternion.identity);
                RayBullet bullet = bulletObject.GetComponent<RayBullet>();
                bullet.SetPosition(muzzleTransform.position, endPoint);
                // isFinishe フラグの管理
                StartCoroutine(CheckBulletEnd(bullet));
            }
        }
    }
    // 弾丸の終了を待機
    private IEnumerator CheckBulletEnd(RayBullet bullet)
    {
        while (bullet != null)
        {
            yield return null;
        }
        isFinishe_ = true;
    }

    public bool IsFinishe()
    {
        return isFinishe_;
    }
}
