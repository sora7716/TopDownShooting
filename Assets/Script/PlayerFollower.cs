using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Cursor))]
public class PlayerFollower : MonoBehaviour
{
    [SerializeField]
    //�ǐՂ���Ώ�
    private GameObject target_;
    [SerializeField, Range(0.0f, 1.0f)] 
    //�J�������ǂꂭ�炢�J�[�\���֊񂹂邩�B
    float interpolatedValue_ = 0.5f;
    //�}�E�X�J�[�\���̏��
    private Cursor cursor_;
    //�J�����ƃv���C���[�̈ʒu�����炩���ߋL�^���Ă���
    private Vector3 offset_;

    // Start is called before the first frame update
    void Start()
    {
        cursor_ = GetComponent<Cursor>();
        //���݂̃v���C���[�ƃJ�����̈ʒu�֌W��ۑ����Ă����B
        offset_ = transform.position - target_.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = cursor_.GetRaycastHit().point;
        Vector3 targetPosition = target_.transform.position;
        //Player��Mouse�J�[�\���̒��Ԃ𒍎�����
        Vector3 lookAt = Vector3.Lerp(targetPosition, mousePosition, interpolatedValue_);
        //�����_����offset�𑫂��ăJ�����𗣂�
        transform.position = lookAt + offset_;
        
    }
}
