using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGun : GunBase
{
    //���ˍς݂��ۂ�
    bool fired_ = false;
    //�g���K�[�𗣂�����false;
    public override void OffTrigger()
    {
        fired_ = false;
    }

    public override void OnTrigger()
    {
        //���ˊԊu�ȓ��Ȃ瑁�����^�[��
        if (shotTimer_ > 0)
        {
            return;
        }
        //�g���K�[�������ςȂ��Ȃ瑁�����^�[��
        if (fired_) { return; }
        //���˃t���OON
        fired_ = true;
        //�^�C�}�[���Z�b�g
        shotTimer_ = fireRate_;
        //muzzle�̐��ʂɃ��C���΂�
        Ray ray = new Ray
            (
            muzzleTransform_.position,
            muzzleTransform_.forward
            );
        RaycastHit raycastHit;
        //Item���C���[�𖳎�����BLayerMask.GetMask�ł���
        //���C���[�̃r�b�g��1�ƂȂ��Ă���l���擾������A
        //~�Ńr�b�g���]���s��
        int layerMask = ~LayerMask.GetMask
            (
            new string[] { "Item" }
            );
        //���C�̒����͎G��100m�Ƃ���
        float rayLength = 100;
        //���C�̏I�_�͂ЂƂ܂��ő��
        Vector3 endPoint = muzzleTransform_.position + muzzleTransform_.forward * rayLength;
        if (Physics.Raycast(ray, out raycastHit, rayLength, layerMask))
        {
            //�Փ˒n�_�Ƀ��C��Z�k
            endPoint = raycastHit.point;
            //�Ώۂ�Helth�R���|�[�l���g���������Ă��邩�m�F 
            Health healthComponent;
            bool hasHealth = raycastHit.collider.TryGetComponent(out healthComponent);
            //�����Ă�����_���[�W��^����
            if (hasHealth)
            {
                healthComponent.Damage(power_);
            }
        }
        //�e�e�𐶐��ERayBullet�R���|�[�l���g�̎擾
        GameObject bulletObject = Instantiate(bulletPrefab_.gameObject, muzzleTransform_.position, muzzleTransform_.rotation);
        RayBullet bullet = bulletObject.GetComponent<RayBullet>();
        //�`�悷��Line�̎n�_�ƏI�_��ݒ�
        bullet.SetPosition(muzzleTransform_.position, endPoint);

    }
}
