using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    //対象に与えるダメージ
    float damege_ = 5;
    [SerializeField]
    //消滅までの時間
    float extinctionTime_ = 1;
    //消滅までの時間
    float timer_;

    Ray ray_;
    RaycastHit raycastHit_;
    // Start is called before the first frame update
    void Start()
    {
        timer_ = extinctionTime_;
    }

    // Update is called once per frame
    void Update()
    {
        //時間経過で消滅
        timer_ -= Time.deltaTime;
        if (timer_ > 0) { return; }
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
}
