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
        //���łɏe�������Ă��邩���m�F
        CheckGrabGun();
    }

    private void Update()
    {
        //�����傪Player�łȂ������瑁�����^�[��
        if (!OwnerIsPlayer()) { return; }
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
        gun_.transform.localEulerAngles = new Vector3(0, 0, -90);
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

    private void CheckGrabGun()
    {
        //��̘r�Ɋ��蓖�Ă��Ă���e��1�ǉ��ł��邱�Ƃ��m�F
        Assert.IsTrue
            (
            transform.childCount <= 1,
            "��̘r�ɕ����̏e�����蓖�Ă��Ă��܂��B"
            );
        //�������������Ɏq�����邩���m�F�B���Ȃ���Ώe�͎����Ă��Ȃ�
        if (transform.childCount == 0)
        {
            return;
        }
        GunBase gradGun;
        //�q��GunBase�N���X���A�^�b�`����Ă��邩�m�F
        bool hasGun = transform.GetChild(0).TryGetComponent(out gradGun);
        //�����Ă�����擾�������s��
        if (hasGun) { Grab(gradGun); }
    }

    private bool OwnerIsPlayer()
    {
        //���g�̐e��null�Ȃ�Ύ������Player�ł͂Ȃ�
        if (transform.parent == null)
        {
            return false;
        }
        //�e�̃^�O��Player�������玝�����Player�ł���
        return transform.parent.tag == "Player";
    }
}
