using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed_ = 10f;
    private Rigidbody rb_;
    private Vector2 moveInput_;
    [SerializeField]private Cursor cursor_;
    // Start is called before the first frame update
    void Start()
    {
        //���W�b�g�{�f�B���󂯎��
        rb_ = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //����Cursor�̃��C���q�b�g���Ă��Ȃ���Α������^�[��
        if (!cursor_.GetIsHit()) { return; }
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
}
