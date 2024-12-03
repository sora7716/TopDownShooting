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
    }

    private void Update()
    {
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
        gun_.transform.localRotation = Quaternion.identity;
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
}
