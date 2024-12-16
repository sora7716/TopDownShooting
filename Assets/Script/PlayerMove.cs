using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed_ = 10f;
    private Rigidbody rb_;
    private Vector2 moveInput_;
    [SerializeField] private Cursor cursor_;

    [SerializeField]
    //�r�̏��BUnity�G�f�B�^��Œ��ڐݒ肷��
    private Arm arm_;
    //InputSystem��Fire��������Ă��邩�ۂ�
    private bool isPushFire_;

    private bool isHaveShotGun_ = false;
    private Vector3 velocity_ = Vector3.zero;
    private Vector3 acceleration_ = Vector3.zero;
    [SerializeField] private float recoil_ = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        //���W�b�g�{�f�B���󂯎��
        rb_ = GetComponent<Rigidbody>();
        isPushFire_ = false;
    }

    // Update is called once per frame
    void Update()
    {
        velocity_ += acceleration_ * Time.deltaTime;
        transform.position += velocity_ * Time.deltaTime;

        //����Cursor�̃��C���q�b�g���Ă��Ȃ���Α������^�[��
        if (!cursor_.GetIsHit()) { return; }
        //�e�����������̂̍X�V
        UpdataGunTrigger();
        //���C�̏Փ˔�����擾
        RaycastHit raycastHit = cursor_.GetRaycastHit();
        //point���Փ˂̍��W
        Vector3 lookAt = raycastHit.point;
        //�Փˈʒu�͏��Ȃ̂ŁAPlayer�Ɠ����ڐ��̍����܂ŕ␳
        lookAt.y = transform.position.y;
        //LookAt���\�b�h�́A�����Ŏw�肵�����W�֌������\�b�h��
        transform.LookAt(lookAt);
    }

    public void OnMove(InputValue value)
    {
        moveInput_ = value.Get<Vector2>();
    }

    /// <summary>
    /// ����Ă΂�Ȃ����Ԋu�ł����Ă΂�Ȃ�(�������g�������Ƃ��������ꍇ�͂����ɏ������ق�������)
    /// </summary>
    private void FixedUpdate()
    {
        Vector3 input;
        input = new Vector3
            (
            //���������ւ̓���
            moveInput_.x,
            0,
            moveInput_.y
            );
        //���͂����������瑁�����^�[��
        if (input.sqrMagnitude == 0) { return; }
        //Rigidbody�̈ړ��@�\��p���Ĉړ�
        rb_.MovePosition
            (
            transform.position +
            input * moveSpeed_ * Time.deltaTime
            );
    }

    /// <summary>
    /// �e�����������̍X�V
    /// </summary>
    private void UpdataGunTrigger()
    {
        //arm���e�������Ă��Ȃ���Α������^�[��
        if (!arm_.IsGrabGun()) { return; }
        //Fire�������Ă��邩�ۂ��ŌĂяo��������ς���
        if (isPushFire_)
        {
            arm_.OnTrigger();
        }
        else
        {
            arm_.OffTrigger();
        }
    }

    /// <summary>
    /// �e���E��
    /// </summary>
    /// <param name="item"></param>
    private void TryGetGun(Collider item)
    {
        GunBase gun;
        //GameBase�R���|�[�l���g�������Ă��Ȃ����
        if (!item.TryGetComponent(out gun)) { return; }
        //�e�̐e�������瑁�����^�[��
        if (!gun.GetIsAlone()) { return; }
        //�e���擾
        arm_.Grab(gun);
    }

    //�����Ă���e�ɐG�ꂽ��擾���悤�Ƃ���
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Item"))
        {
            return;
        }
        TryGetGun(other);
        if (other.gameObject.layer == LayerMask.NameToLayer("ShotGun"))
        {
            isHaveShotGun_ = true;
        }
        else
        {
            isHaveShotGun_ = false;
        }
    }

    /// <summary>
    /// ���������ǂ����̊֐�(InputSystem�ɂ��Ƃ��Ɠ����Ă���)
    /// </summary>
    /// <param name="inputValue"></param>
    public void OnFire(InputValue inputValue)
    {
        isPushFire_ = inputValue.isPressed;
        if (isHaveShotGun_)
        {
            acceleration_ = -transform.forward * recoil_;
        }
        if (!isPushFire_)
        {
            acceleration_ = Vector3.zero;
            velocity_ = Vector3.zero;
        }
    }

}
