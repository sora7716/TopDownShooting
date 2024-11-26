using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : MonoBehaviour
{
    //�������Ă���e
    private GunBase gun_;
    //�e�̎擾
    public void Grab(GunBase gun)
    {
        //�������łɏe�������Ă�����j������
        if (gun_ != null)
        {
            Destroy(gun_.gameObject);
        }
        //�V�����e��u��������
        gun_ = gun;
        //�Ώۂ̏e�ɐe�������Ƃ���
        gun.transform.SetParent(transform);
        //�����̈ʒu�ɏd�ˁA��]������������
        gun_.transform.localPosition = Vector3.zero;
        gun_.transform.localRotation = Quaternion.identity;
    }
    //�e�������Ă��邩�ۂ�
    public bool IsGrabGun()
    {
        return gun_ != null;
    }
    //�g���K�[�������Ă��邱�Ƃ��e�ɓ`����
    public void OnTrigger()
    {
        if (!IsGrabGun())
        {
            return;
        }
        gun_.OnTrigger();
    }
    //�g���K�[�𗣂��Ă��邱�Ƃ��e�ɓ`����
    public void OffTrigger()
    {
        if (!IsGrabGun()) 
        {
            return;
        }
        gun_.OffTrigger();
    }
}
