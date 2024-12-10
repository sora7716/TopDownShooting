using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    //生成されたスイッチの数
    private int switchNum = 0;
    //破壊されたスイッチの数
    private int destroyedSwitchNum = 0;
    //紐づけされたSwitchに生成時に呼んでもらう
    public void AddSwitch()
    {
        switchNum++;
    }
    //紐づけされたSwitchが破壊されたときに呼んでもらう
    public void DestroySwitch()
    {
        destroyedSwitchNum++;
        //扉の解放判断は、スイッチ破壊時のみに行う
        if (switchNum > destroyedSwitchNum)
        {
            return;
        }
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
