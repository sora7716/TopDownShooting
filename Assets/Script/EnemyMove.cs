using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyMove : MonoBehaviour
{
    [SerializeField] public Vector3 begin_;
    [SerializeField] public Vector3 end_;
    [SerializeField] public float motion_;
    protected float rotationFrame_;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Move()
    {
        rotationFrame_ += Time.deltaTime;
        // SinîgÇ0Å`1ÇÃîÕàÕÇ…ïœä∑
        float param = (Mathf.Sin(rotationFrame_ / motion_ * Mathf.PI * 2) + 1.0f) / 2.0f;
        transform.localEulerAngles = Vector3.Lerp(begin_, end_, param);
    }
}
