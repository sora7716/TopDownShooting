using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    //�����̘r
    Arm myArm_;
    [SerializeField]
    //Player���������鋗��
    float findDistance_ = 10;
    [SerializeField]
    //Player����������p�x
    float findAngleDeg_ = 60;
    //Player�̏��
    PlayerMove player_;

    //�ŏ��̊p�x
    [SerializeField] Vector3 beginAngle_ = new Vector3(0.0f, -20.0f, 0.0f);

    //�Ō�̊p�x
    [SerializeField] Vector3 endAngle_ = new Vector3(0.0f, Mathf.PI * 20.0f, 0.0f);

    //����
    [SerializeField] float motion_ = 1.0f;
    //�t���[����
    float rotationFrame_ = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        //�܂���Player�̏���T��
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        Assert.IsNotNull(playerObject);
        bool findPlayerMove = playerObject.TryGetComponent(out player_);
        Assert.IsTrue(findPlayerMove);
    }

    private bool IsFindPlayer()
    {
        //Player�ւ̃x�N�g�����Z�o
        Vector3 toPlayer = player_.transform.position - transform.position;
        //������2����Z�o(���[�g��������y����)
        float sqrPlayerDistance = Vector3.SqrMagnitude(toPlayer);
        //������2��Ŋ��m�����O�Ȃ瑁�����^�[��  
        if (sqrPlayerDistance > findDistance_ * findDistance_)
        {
            return false;
        }
        //���g�̐��ʂƓ��ς��Ƃ�
        float dot = Vector3.Dot
            (
            toPlayer.normalized,
            transform.forward
            );
        //Player�ւ̃x�N�g���p�x���Z�o
        float toPlayerAngleDeg = Mathf.Acos(dot) * Mathf.Rad2Deg;
        //�p�x�����m�͈͊O�Ȃ瑁�����^�[��
        if (toPlayerAngleDeg > findAngleDeg_ / 2)
        {
            return false;
        }
        //Player�Ɍ����Ẵ��C���`
        Ray ray = new Ray(transform.position, toPlayer);
        RaycastHit hit;
        //�A�C�e���𖳎�����
        int mask = ~LayerMask.GetMask("Item");
        //���C���΂�������findDistance_�ŏ[��
        //�����������C���q�b�g���Ȃ���Α������^�[��
        if(!Physics.Raycast(ray,out hit, findAngleDeg_, mask))
        {
            return false;
        }
        //���C�̏Փ˂�Player�������甭��
        return hit.collider.gameObject == player_.gameObject;
    }

    private void OnTrigger()
    {
        myArm_.OnTrigger();
    }

    private void OffTrigger()
    {
        myArm_.OffTrigger();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsFindPlayer())
        {
            //Player���������猩������
            transform.LookAt(player_.transform);
            //�A�˃p�b�h���畉���̓��t���[��On/Off�؂�ւ�
            OnTrigger();
            OffTrigger();
        }else
        {
            OffTrigger();
            rotationFrame_ += Time.deltaTime;
            // Sin�g��0�`1�͈̔͂ɕϊ�
            float param = (Mathf.Sin(rotationFrame_ / motion_ * Mathf.PI * 2) + 1.0f) / 2.0f;
            transform.localEulerAngles = Vector3.Lerp(beginAngle_, endAngle_, param);
        }
    }
}
