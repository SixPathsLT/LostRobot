using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PuzzleManager;

public abstract class Puzzles : MonoBehaviour
{
    public virtual void Activate()
    {
    }
    public virtual void Fail()
    {
        lockdown.LockDownInitiated = true;
    }

    public virtual void InProgress(bool started) { 
    }

    public virtual void Finished() { 
    }

    public virtual void Passed(GameObject door)
    {
        //unlocks information
    }

    
}
