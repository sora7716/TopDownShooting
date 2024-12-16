using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed_ = 10f;
    private Rigidbody rb_;
    private Vector2 moveInput_;
    [SerializeField] private Cursor cursor_;

    [SerializeField]
    //腕の情報。Unityエディタ上で直接設定する
    private Arm arm_;
    //InputSystemのFireが押されているか否か
    private bool isPushFire_;

    private bool isHaveShotGun_ = false;
    private Vector3 velocity_ = Vector3.zero;
    private Vector3 acceleration_ = Vector3.zero;
    [SerializeField] private float recoil_ = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        //リジットボディを受け取る
        rb_ = GetComponent<Rigidbody>();
        isPushFire_ = false;
    }

    // Update is called once per frame
    void Update()
    {
        velocity_ += acceleration_ * Time.deltaTime;
        transform.position += velocity_ * Time.deltaTime;

        //もしCursorのレイがヒットしていなければ早期リターン
        if (!cursor_.GetIsHit()) { return; }
        //銃を持った時のの更新
        UpdataGunTrigger();
        //レイの衝突判定を取得
        RaycastHit raycastHit = cursor_.GetRaycastHit();
        //pointが衝突の座標
        Vector3 lookAt = raycastHit.point;
        //衝突位置は床なので、Playerと同じ目線の高さまで補正
        lookAt.y = transform.position.y;
        //LookAtメソッドは、引数で指定した座標へ向くメソッドだ
        transform.LookAt(lookAt);
    }

    public void OnMove(InputValue value)
    {
        moveInput_ = value.Get<Vector2>();
    }

    /// <summary>
    /// 毎回呼ばれない一定間隔でしか呼ばれない(物理を使ったことをしたい場合はここに書いたほうがいい)
    /// </summary>
    private void FixedUpdate()
    {
        Vector3 input;
        input = new Vector3
            (
            //水平方向への入力
            moveInput_.x,
            0,
            moveInput_.y
            );
        //入力が早かったら早期リターン
        if (input.sqrMagnitude == 0) { return; }
        //Rigidbodyの移動機能を用いて移動
        rb_.MovePosition
            (
            transform.position +
            input * moveSpeed_ * Time.deltaTime
            );
    }

    /// <summary>
    /// 銃を持った時の更新
    /// </summary>
    private void UpdataGunTrigger()
    {
        //armが銃を持っていなければ早期リターン
        if (!arm_.IsGrabGun()) { return; }
        //Fireを押しているか否かで呼び出す処理を変える
        if (isPushFire_)
        {
            arm_.OnTrigger();
        }
        else
        {
            arm_.OffTrigger();
        }
    }

    /// <summary>
    /// 銃を拾う
    /// </summary>
    /// <param name="item"></param>
    private void TryGetGun(Collider item)
    {
        GunBase gun;
        //GameBaseコンポーネントを持っていなければ
        if (!item.TryGetComponent(out gun)) { return; }
        //銃の親が居たら早期リターン
        if (!gun.GetIsAlone()) { return; }
        //銃を取得
        arm_.Grab(gun);
    }

    //落ちている銃に触れたら取得しようとする
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Item"))
        {
            return;
        }
        TryGetGun(other);
        if (other.gameObject.layer == LayerMask.NameToLayer("ShotGun"))
        {
            isHaveShotGun_ = true;
        }
        else
        {
            isHaveShotGun_ = false;
        }
    }

    /// <summary>
    /// 押したかどうかの関数(InputSystemにもともと入っている)
    /// </summary>
    /// <param name="inputValue"></param>
    public void OnFire(InputValue inputValue)
    {
        isPushFire_ = inputValue.isPressed;
        if (isHaveShotGun_)
        {
            acceleration_ = -transform.forward * recoil_;
        }
        if (!isPushFire_)
        {
            acceleration_ = Vector3.zero;
            velocity_ = Vector3.zero;
        }
    }

}
