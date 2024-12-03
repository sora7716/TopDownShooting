using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionHealth : Health
{
    [SerializeField]
    //爆発プレハブ
    Explosion explosionPrefab_;
    //死んだときに呼ばれる関数を少々こちらでオーバーライド
    protected override void Death()
    {
        //爆発を生成
        Instantiate(explosionPrefab_, transform.position, Quaternion.identity);
        //基底クラスのDeathメソッドを呼び出す
        base.Death();
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
