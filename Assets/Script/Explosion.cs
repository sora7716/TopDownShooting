using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    //ダメージの初期値
    float beginDamege_ = 1;
    [SerializeField]
    //ダメージの最終値
    float endDamege_ = 5;
    //対象に与えるダメージ
    float damege_;
    [SerializeField]
    //消滅までの時間
    float extinctionTime_ = 1;
    //消滅までの時間
    float aliveTimer_;

    //スケールアップする用の変数
    [SerializeField] Vector3 minScale_ = Vector3.zero;
    [SerializeField] Vector3 maxScale_ = new Vector3(3.0f, 3.0f, 3.0f);
    float sceleUpEndSecond_;
    float scaleUpTimer_ = 0.0f;

    Ray ray_;
    RaycastHit raycastHit_;
    // Start is called before the first frame update
    void Start()
    {
        aliveTimer_ = extinctionTime_;
        transform.localScale = minScale_;
        damege_ = beginDamege_;
        sceleUpEndSecond_ = aliveTimer_ / 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //時間経過で消滅
        aliveTimer_ -= Time.deltaTime;
        ScaleUp();
        if (aliveTimer_ > 0) { return; }
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        ray_ = new Ray(transform.position, (other.transform.position - transform.position).normalized);
        //接触した相手がHelthコンポーネントを
        //持っていたらダメージを与える
        if (Physics.Raycast(ray_, out raycastHit_))
        {
            if (!raycastHit_.collider.CompareTag("Wall"))
            {
                Health health;
                bool hasHealth = other.TryGetComponent(out health);
                if (!hasHealth) { return; }
                health.Damage(damege_);
            }
        }
    }

    //スケールアップ
    private void ScaleUp()
    {
        scaleUpTimer_ += Time.deltaTime;
        transform.localScale = Vector3.Lerp(minScale_, maxScale_, scaleUpTimer_ / sceleUpEndSecond_);
        damege_ = Mathf.Lerp(beginDamege_, endDamege_, scaleUpTimer_ / sceleUpEndSecond_);
    }
}
