using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//LineRenderereを操作するスクリプトが必須
[RequireComponent(typeof(LineRenderer))]
public class RayBullet : MonoBehaviour
{
    [SerializeField]
    //生存時間
    private float lifeTime_ = 0.5f;
    //生存残り時間
    private float timer_;
    //LineRenderere本体
    private LineRenderer line_;

    //Line開始座標(発射位置)
    private Vector3 beginPosition_; 
    //Line終了座標(着弾位置)
    private Vector3 endPosition_;

    private void Awake()
    {
        line_ = GetComponent<LineRenderer>();
        timer_ = lifeTime_;
    }

    public void SetPosition(Vector3 beginPosition, Vector3 endPosition)
    {
        beginPosition_ = beginPosition;
        endPosition_ = endPosition;
        //Vector3の配列で座標を渡す
        line_.SetPositions(new Vector3[] { beginPosition_,endPosition_});

    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //時間を計測して生存時間がなくなったら消滅
        timer_ -= Time.deltaTime;
        if (timer_ <= 0)
        {
            Destroy(gameObject);
        }
    }
}
