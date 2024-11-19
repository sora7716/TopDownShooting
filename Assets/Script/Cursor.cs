using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    //���̃��C���[���B���ȊO�͏Փ˂��Ȃ��悤�ɂ���
    private const string FLOOR_LAYER_NAME = "Floor";
    //�Փ˂��Ă��邩�ۂ�
    private bool isHit_;
    //���C�̏��
    private Ray ray_;
    //�Փ˂̏��
    private RaycastHit raycastHit_;
    //�J����
    private Camera camera_;

    // Start is called before the first frame update
    void Start()
    {
        //�J�������󂯎��
        camera_ = GetComponent<Camera>();
    }

    // Update is called once per frame
    private void Update()
    {
        //�J��������̃��C���Z�o
        ray_ = camera_.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray_.origin, ray_.direction * 1000, Color.green);
        //���C���΂��B�߂�l��bool�ŏՓ˂������ǂ���
        //�Փ˂��Ă����raycastHit����Փ˂̏�񂪎擾�ł���
        int checkNum = LayerMask.GetMask(FLOOR_LAYER_NAME);
        isHit_ = Physics.Raycast
            (
            ray_,
            out raycastHit_,
            1000);
        if (isHit_)
        {
            Debug.Log(raycastHit_.collider.gameObject.name);
        }
        else
        {
            Debug.Log("non hit");
        }
    }

    public bool GetIsHit() { return isHit_; }
    public Ray GetRay() { return ray_; }
    public RaycastHit GetRaycastHit() { return raycastHit_; }
}
