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
        //targetDoors‚É‚ ‚éDoor‘S‚Ä‚É‘Î‚µ‚Ä©g‚Ìî•ñ‚ğ“`‚¦‚é
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
            //©g‚Ì”j‰ó‚ğDoor‚É“`‚¦‚é
            door.DestroySwitch();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
