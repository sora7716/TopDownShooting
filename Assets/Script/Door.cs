using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    //�������ꂽ�X�C�b�`�̐�
    private int switchNum = 0;
    //�j�󂳂ꂽ�X�C�b�`�̐�
    private int destroyedSwitchNum = 0;
    //�R�Â����ꂽSwitch�ɐ������ɌĂ�ł��炤
    public void AddSwitch()
    {
        switchNum++;
    }
    //�R�Â����ꂽSwitch���j�󂳂ꂽ�Ƃ��ɌĂ�ł��炤
    public void DestroySwitch()
    {
        destroyedSwitchNum++;
        //���̉�����f�́A�X�C�b�`�j�󎞂݂̂ɍs��
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
