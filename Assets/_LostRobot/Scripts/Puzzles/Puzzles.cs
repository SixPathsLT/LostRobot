using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Puzzles : MonoBehaviour
{
    public virtual void Activate()
    {
        //call puzzle ui
    }
    public virtual void Fail()
    {
        //start lockdown
    }

    public virtual void InProgress() { 
        Time.timeScale = 0;
    }

    public virtual void Finished() { 
        Time.timeScale = 1;
    }

    public virtual void Passed(GameObject door)
    {
        //unlocks door/computer
    }
}
