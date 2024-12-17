using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : GunBase
{
    private Vector3 direction_;//���ł�������
    [SerializeField] private float dispersion_;//���U����x����
    //���ˍς݂��ۂ�
    bool fired_ = false;
    bool isFinishe_ = false;
    //�g���K�[�𗣂�����false;
    public override void OffTrigger()
    {
        fired_ = false;
        isFinishe_ = false;
    }

    public override void OnTrigger()
    {
        // ���ˊԊu�ȓ��Ȃ瑁�����^�[��
        if (shotTimer_ > 0)
        {
            return;
        }
        // �g���K�[�������ςȂ��Ȃ瑁�����^�[��
        if (fired_) { return; }
        // ���˃t���OON
        fired_ = true;
        // �^�C�}�[���Z�b�g
        shotTimer_ = fireRate_;

        // �e�}�Y�����畡���̒e�𔭎�
        foreach (var muzzleTransform in muzzleTransforms_)
        {
            // �V���b�g�K���̎U�e��
            int pelletCount = muzzleTransforms_.Count; // ��: 10���̎U�e�𔭎�
            for (int i = 0; i < pelletCount; i++)
            {
                // �����_���ȕ������v�Z
                Vector3 randomDirection = muzzleTransform.forward + new Vector3
                (
                    Random.Range(-dispersion_, dispersion_),
                    Random.Range(-dispersion_, dispersion_),
                    Random.Range(-dispersion_, dispersion_)
                );

                // ���C���΂�
                Ray ray = new Ray(muzzleTransform.position, randomDirection.normalized);
                RaycastHit raycastHit;

                // Item���C���[�𖳎�����
                int layerMask = ~LayerMask.GetMask(new string[] { "Item" });
                float rayLength = 100;
                Vector3 endPoint = muzzleTransform.position + randomDirection.normalized * rayLength;

                if (Physics.Raycast(ray, out raycastHit, rayLength, layerMask))
                {
                    // �Փ˒n�_���v�Z
                    endPoint = raycastHit.point;

                    // �Փ˂����I�u�W�F�N�g�� Health �R���|�[�l���g�������Ă���΃_���[�W��^����
                    if (raycastHit.collider.TryGetComponent<Health>(out Health healthComponent))
                    {
                        healthComponent.Damage(power_);
                    }
                }

                // �e�ۂ𐶐����ĕ`��
                GameObject bulletObject = Instantiate(bulletPrefab_.gameObject, muzzleTransform.position, Quaternion.identity);
                RayBullet bullet = bulletObject.GetComponent<RayBullet>();
                bullet.SetPosition(muzzleTransform.position, endPoint);
                // isFinishe �t���O�̊Ǘ�
                StartCoroutine(CheckBulletEnd(bullet));
            }
        }
    }
    // �e�ۂ̏I����ҋ@
    private IEnumerator CheckBulletEnd(RayBullet bullet)
    {
        while (bullet != null)
        {
            yield return null;
        }
        isFinishe_ = true;
    }

    public bool IsFinishe()
    {
        return isFinishe_;
    }
}
