using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    //自分の腕
    Arm myArm_;
    [SerializeField]
    //Playerを見つけられる距離
    float findDistance_ = 10;
    [SerializeField]
    //Playerを見つけられる角度
    float findAngleDeg_ = 60;
    //Playerの情報
    PlayerMove player_;

    //最初の角度
    [SerializeField] Vector3 beginAngle_ = new Vector3(0.0f, -20.0f, 0.0f);

    //最後の角度
    [SerializeField] Vector3 endAngle_ = new Vector3(0.0f, Mathf.PI * 20.0f, 0.0f);

    //周期
    [SerializeField] float motion_ = 1.0f;
    //フレーム数
    float rotationFrame_ = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        //まずはPlayerの情報を探す
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        Assert.IsNotNull(playerObject);
        bool findPlayerMove = playerObject.TryGetComponent(out player_);
        Assert.IsTrue(findPlayerMove);
    }

    private bool IsFindPlayer()
    {
        //Playerへのベクトルを算出
        Vector3 toPlayer = player_.transform.position - transform.position;
        //距離の2乗を算出(ルートかけるより軽負荷)
        float sqrPlayerDistance = Vector3.SqrMagnitude(toPlayer);
        //距離の2乗で感知距離外なら早期リターン  
        if (sqrPlayerDistance > findDistance_ * findDistance_)
        {
            return false;
        }
        //自身の正面と内積をとる
        float dot = Vector3.Dot
            (
            toPlayer.normalized,
            transform.forward
            );
        //Playerへのベクトル角度を算出
        float toPlayerAngleDeg = Mathf.Acos(dot) * Mathf.Rad2Deg;
        //角度が感知範囲外なら早期リターン
        if (toPlayerAngleDeg > findAngleDeg_ / 2)
        {
            return false;
        }
        //Playerに向けてのレイを定義
        Ray ray = new Ray(transform.position, toPlayer);
        RaycastHit hit;
        //アイテムを無視する
        int mask = ~LayerMask.GetMask("Item");
        //レイを飛ばす距離はfindDistance_で充分
        //そもそもレイがヒットしなければ早期リターン
        if(!Physics.Raycast(ray,out hit, findAngleDeg_, mask))
        {
            return false;
        }
        //レイの衝突がPlayerだったら発見
        return hit.collider.gameObject == player_.gameObject;
    }

    private void OnTrigger()
    {
        myArm_.OnTrigger();
    }

    private void OffTrigger()
    {
        myArm_.OffTrigger();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsFindPlayer())
        {
            //Playerを見つけたら見続ける
            transform.LookAt(player_.transform);
            //連射パッドも顔負けの同フレームOn/Off切り替え
            OnTrigger();
            OffTrigger();
        }else
        {
            OffTrigger();
            rotationFrame_ += Time.deltaTime;
            // Sin波を0～1の範囲に変換
            float param = (Mathf.Sin(rotationFrame_ / motion_ * Mathf.PI * 2) + 1.0f) / 2.0f;
            transform.localEulerAngles = Vector3.Lerp(beginAngle_, endAngle_, param);
        }
    }
}
