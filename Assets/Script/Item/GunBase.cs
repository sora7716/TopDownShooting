using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GunBase : MonoBehaviour
{
    [SerializeField]
    //���C����v���n�u
    protected RayBullet bulletPrefab_;
    [SerializeField]
    //�e���̃g�����X�t�H�[���B���ˈʒu
    protected Transform muzzleTransform_;
    [SerializeField]
    //�A�ˊ��o
    protected float fireRate_ = 0;
    //���˃^�C�}�[
    protected float shotTimer_ = 0;
    [SerializeField]
    //�З�
    protected float power_ = 0;
    [SerializeField]
    //�擾���O�ɉ�]���Ă��鑬�x
    private float itemRotateSpeedDeg_ = 90f;

    //���˓��͂���������
    public abstract void OnTrigger();
    //���˓��͂�����������    
    public abstract void OffTrigger();

    private void ItemRotate()
    {
        //�n�ʂɗ����Ă���Ԃ̉�]����
        transform.RotateAround
            (
            transform.position,
            Vector3.up,
            itemRotateSpeedDeg_ * Time.deltaTime
            );
    }

    public bool GetIsAlone()
    {
        //�e�����Ȃ�������N�ɂ���������Ă��Ȃ�
        return transform.parent == null;
    }

    public virtual void Update()
    {
        //��������Ă��Ȃ��������]����
        if (GetIsAlone()) { ItemRotate(); }
        //�^�C�}�[�̍X�V
        if (shotTimer_ <= 0) { return; }
        shotTimer_ -= Time.deltaTime;
    }
}
