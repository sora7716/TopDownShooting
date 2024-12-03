using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//銃の攻撃を受け取るコライダーを必須とする
[RequireComponent(typeof(Collider))]
public class Health : MonoBehaviour
{
    [SerializeField]
    //最大体力
    private float maxHealth_ = 3;
    //現在の体力
    private float currentHealth_;
    //自身のコライダー
    protected Collider collider_;

    private void Awake()
    {
        currentHealth_ = maxHealth_;
        collider_ = GetComponent<Collider>();
    }

    public void Damage(float point)
    {
        //体力を減らし、現在体力が0となれば死亡
        currentHealth_-= point;
        if (currentHealth_ > 0) { return; }
        Death();
    }

    protected virtual void Death()
    {
        //死亡時に消滅する
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
