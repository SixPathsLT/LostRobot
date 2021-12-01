using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Puzzles : MonoBehaviour
{
    public virtual void Activate(bool showMouse)
    {
        if (showMouse){
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
        FindObjectOfType<PuzzleManager>().movement.enabled = false;
        FindObjectOfType<PuzzleManager>().abil.enabled = false;


    }

    public virtual void Reset()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        FindObjectOfType<PuzzleManager>().movement.enabled = true;
        FindObjectOfType<PuzzleManager>().abil.enabled = true;

    }
}
