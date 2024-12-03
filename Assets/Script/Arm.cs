using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Arm : MonoBehaviour
{
    //�}�E�X�J�[�\���N���X
    private Cursor cursor_;
    //�������Ă���e
    private GunBase gun_;

    private void Start()
    {
        //�J�����̎擾
        Camera.main.TryGetComponent<Cursor>(out cursor_);
        Assert.IsTrue(cursor_!=null,"Cursor�擾���s");
    }

    private void Update()
    {
        //�}�E�X�J�[�\���������n�ʂ̍��W���擾
        Vector3 cursorPoint = cursor_.GetRaycastHit().point;
        //�J�����ւ̌���
        Vector3 toCamera = -cursor_.GetRay().direction;
        //������Ƃ̓���
        float dot = Vector3.Dot(Vector3.up, toCamera);
        //aeccos�������Ċp�x��
        float angleRad=Mathf.Acos(dot);
        //�n�ʂ���̍������Z�o
        float h = transform.position.y - cursorPoint.y;
        //�Εӂ̒������Z�o
        float cLength=h/Mathf.Cos(angleRad);
        //���C�̏Փ˓_����A�J�����Ɍ�����cLentght��߂�
        Vector3 loocAtPoint = cursorPoint + toCamera * cLength;
        //�Z�o�������W�𒍎�����
        transform.LookAt(loocAtPoint);
    }

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
