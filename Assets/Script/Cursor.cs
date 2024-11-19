using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    //床のレイヤー名。床以外は衝突しないようにする
    private const string FLOOR_LAYER_NAME = "Floor";
    //衝突しているか否か
    private bool isHit_;
    //レイの情報
    private Ray ray_;
    //衝突の情報
    private RaycastHit raycastHit_;
    //カメラ
    private Camera camera_;

    // Start is called before the first frame update
    void Start()
    {
        //カメラを受け取る
        camera_ = GetComponent<Camera>();
    }

    // Update is called once per frame
    private void Update()
    {
        //カメラからのレイを算出
        ray_ = camera_.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray_.origin, ray_.direction * 1000, Color.green);
        //レイを飛ばす。戻り値はboolで衝突したかどうか
        //衝突しているとraycastHitから衝突の情報が取得できる
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
