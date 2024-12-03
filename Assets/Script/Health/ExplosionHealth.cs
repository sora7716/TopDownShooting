using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionHealth : Health
{
    [SerializeField]
    //�����v���n�u
    Explosion explosionPrefab_;
    //���񂾂Ƃ��ɌĂ΂��֐������X������ŃI�[�o�[���C�h
    protected override void Death()
    {
        //�����𐶐�
        Instantiate(explosionPrefab_, transform.position, Quaternion.identity);
        //���N���X��Death���\�b�h���Ăяo��
        base.Death();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
