using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Arm : MonoBehaviour
{
    //マウスカーソルクラス
    private Cursor cursor_;
    //所持している銃
    private GunBase gun_;

    private void Start()
    {
        //カメラの取得
        Camera.main.TryGetComponent<Cursor>(out cursor_);
        Assert.IsTrue(cursor_!=null,"Cursor取得失敗");
        //すでに銃を持っているかを確認
        CheckGrabGun();
    }

    private void Update()
    {
        //持ち主がPlayerでなかったら早期リターン
        if (!OwnerIsPlayer()) { return; }
        //マウスカーソルが示す地面の座標を取得
        Vector3 cursorPoint = cursor_.GetRaycastHit().point;
        //カメラへの向き
        Vector3 toCamera = -cursor_.GetRay().direction;
        //上方向との内積
        float dot = Vector3.Dot(Vector3.up, toCamera);
        //aeccosをかけて角度へ
        float angleRad=Mathf.Acos(dot);
        //地面からの高さを算出
        float h = transform.position.y - cursorPoint.y;
        //斜辺の長さを算出
        float cLength=h/Mathf.Cos(angleRad);
        //レイの衝突点から、カメラに向けてcLentghtを戻す
        Vector3 loocAtPoint = cursorPoint + toCamera * cLength;
        //算出した座標を注視する
        transform.LookAt(loocAtPoint);
    }

    //銃の取得
    public void Grab(GunBase gun)
    {
        //もしすでに銃を持っていたら破棄する
        if (gun_ != null)
        {
            Destroy(gun_.gameObject);
        }
        //新しく銃を置き換える
        gun_ = gun;
        //対象の銃に親を自分とする
        gun.transform.SetParent(transform);
        //自分の位置に重ね、回転を初期化する
        gun_.transform.localPosition = Vector3.zero;
        gun_.transform.localEulerAngles = new Vector3(0, 0, -90);
    }
    //銃を持っているか否か
    public bool IsGrabGun()
    {
        return gun_ != null;
    }
    //トリガーを引いていることを銃に伝える
    public void OnTrigger()
    {
        if (!IsGrabGun())
        {
            return;
        }
        gun_.OnTrigger();
    }
    //トリガーを離していることを銃に伝える
    public void OffTrigger()
    {
        if (!IsGrabGun()) 
        {
            return;
        }
        gun_.OffTrigger();
    }

    private void CheckGrabGun()
    {
        //一つの腕に割り当てられている銃は1追加であることを確認
        Assert.IsTrue
            (
            transform.childCount <= 1,
            "一つの腕に複数の銃が割り当てられています。"
            );
        //そもそも自分に子がいるかを確認。居なければ銃は持っていない
        if (transform.childCount == 0)
        {
            return;
        }
        GunBase gradGun;
        //子にGunBaseクラスがアタッチされているか確認
        bool hasGun = transform.GetChild(0).TryGetComponent(out gradGun);
        //持っていたら取得処理を行う
        if (hasGun) { Grab(gradGun); }
    }

    private bool OwnerIsPlayer()
    {
        //自身の親がnullならば持ち主はPlayerではない
        if (transform.parent == null)
        {
            return false;
        }
        //親のタグがPlayerだったら持ち主はPlayerである
        return transform.parent.tag == "Player";
    }
}
