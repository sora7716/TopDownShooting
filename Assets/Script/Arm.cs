using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : MonoBehaviour
{
    //所持している銃
    private GunBase gun_;
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
