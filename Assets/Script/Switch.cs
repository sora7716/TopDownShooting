using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField]
    private List<Door> targetDoors;

    // Start is called before the first frame update
    void Start()
    {
        //targetDoors�ɂ���Door�S�Ăɑ΂��Ď��g�̏���`����
        foreach(Door door in targetDoors)
        {
            door.AddSwitch();
        }
    }

    private void OnDestroy()
    {
        foreach(Door door in targetDoors)
        {
            if (door == null)
            {
                continue;
            }
            //���g�̔j���Door�ɓ`����
            door.DestroySwitch();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
