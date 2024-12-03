using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�e�̍U�����󂯎��R���C�_�[��K�{�Ƃ���
[RequireComponent(typeof(Collider))]
public class Health : MonoBehaviour
{
    [SerializeField]
    //�ő�̗�
    private float maxHealth_ = 3;
    //���݂̗̑�
    private float currentHealth_;
    //���g�̃R���C�_�[
    protected Collider collider_;

    private void Awake()
    {
        currentHealth_ = maxHealth_;
        collider_ = GetComponent<Collider>();
    }

    public void Damage(float point)
    {
        //�̗͂����炵�A���ݑ̗͂�0�ƂȂ�Ύ��S
        currentHealth_-= point;
        if (currentHealth_ > 0) { return; }
        Death();
    }

    protected virtual void Death()
    {
        //���S���ɏ��ł���
        Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
