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

    /// <summary>
    /// �V�F�C�N
    /// </summary>
    [SerializeField] protected Shake shake_;

    private void Awake()
    {
        currentHealth_ = maxHealth_;
        collider_ = GetComponent<Collider>();
        shake_ = GetComponent<Shake>();
    }

    public void Damage(float point)
    {
        //�̗͂����炵�A���ݑ̗͂�0�ƂȂ�Ύ��S
        currentHealth_ -= point;
        shake_.SetIsShake(true);
        if (currentHealth_ > 0) { return; }
        Death();
    }

    protected virtual void Death()
    {
        if (gameObject.tag != "Player")
        {
            //���S���ɏ��ł���
            Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        shake_.ShakeStart();
    }
}
