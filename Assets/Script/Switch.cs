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
        //targetDoorsにあるDoor全てに対して自身の情報を伝える
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
            //自身の破壊をDoorに伝える
            door.DestroySwitch();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
