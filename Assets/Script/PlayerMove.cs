using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed_ = 10f;
    private Rigidbody rb_;
    private Vector2 moveInput_;
    [SerializeField]private Cursor cursor_;
    // Start is called before the first frame update
    void Start()
    {
        //リジットボディを受け取る
        rb_ = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //もしCursorのレイがヒットしていなければ早期リターン
        if (!cursor_.GetIsHit()) { return; }
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
}
