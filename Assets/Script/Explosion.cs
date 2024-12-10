using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    //�Ώۂɗ^����_���[�W
    float damege_ = 5;
    [SerializeField]
    //���ł܂ł̎���
    float extinctionTime_ = 1;
    //���ł܂ł̎���
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
        //���Ԍo�߂ŏ���
        timer_ -= Time.deltaTime;
        if (timer_ > 0) { return; }
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        ray_ = new Ray(transform.position, (other.transform.position - transform.position).normalized);
        //�ڐG�������肪Helth�R���|�[�l���g��
        //�����Ă�����_���[�W��^����
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
