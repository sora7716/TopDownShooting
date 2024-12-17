using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    //カメラの初期位置
    Vector3 firstPos_ = Vector3.zero;

    //シェイクを開始フラグ
    bool isShake_ = false;
    bool isStart_ = false;

    //シェイクの幅
    [SerializeField] private Vector2 randomRangeX_ = new Vector2(-0.1f, 0.1f);
    [SerializeField] private Vector2 randomRangeY_ = new Vector2(-0.1f, 0.1f);
    [SerializeField] private float z_ = 0.0f;

    //シェイクの行っている時間
    [SerializeField] float shakeTime_ = 0.5f;
    float frame_ = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        ShakeStart();
    }

    /// <summary>
    /// シェイクのフラグのセッター
    /// </summary>
    /// <param name="isShake"></param>
    public void SetIsShake(bool isShake)
    {
        isShake_ = isShake;
    }

    /// <summary>
    /// シェイクをスタートさせる
    /// </summary>
    public void ShakeStart()
    {
        if (isShake_)
        {
            if (frame_ < shakeTime_)
            {
                isStart_ = true;
                frame_ += Time.deltaTime / shakeTime_;
            }
            else
            {
                isStart_ = false;

            }
            if (isStart_)
            {
                Vector3 randomPos;
                randomPos = new Vector3(
                    Random.Range(randomRangeX_.x, randomRangeX_.y),
                    z_,
                    Random.Range(randomRangeY_.x, randomRangeY_.y)
                );
                transform.position += randomPos;
            }
            else
            {
                transform.position = firstPos_;
                frame_ = 0.0f;
            }
        }
        if (!isStart_)
        {
            isShake_ = false;
            firstPos_ = transform.position;
        }
    }
}