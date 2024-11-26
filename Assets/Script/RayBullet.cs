using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//LineRenderere�𑀍삷��X�N���v�g���K�{
[RequireComponent(typeof(LineRenderer))]
public class RayBullet : MonoBehaviour
{
    [SerializeField]
    //��������
    private float lifeTime_ = 0.5f;
    //�����c�莞��
    private float timer_;
    //LineRenderere�{��
    private LineRenderer line_;

    //Line�J�n���W(���ˈʒu)
    private Vector3 beginPosition_; 
    //Line�I�����W(���e�ʒu)
    private Vector3 endPosition_;

    private void Awake()
    {
        line_ = GetComponent<LineRenderer>();
        timer_ = lifeTime_;
    }

    public void SetPosition(Vector3 beginPosition, Vector3 endPosition)
    {
        beginPosition_ = beginPosition;
        endPosition_ = endPosition;
        //Vector3�̔z��ō��W��n��
        line_.SetPositions(new Vector3[] { beginPosition_,endPosition_});

    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //���Ԃ��v�����Đ������Ԃ��Ȃ��Ȃ��������
        timer_ -= Time.deltaTime;
        if (timer_ <= 0)
        {
            Destroy(gameObject);
        }
    }
}
