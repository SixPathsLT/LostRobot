using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Puzzles : MonoBehaviour
{
    public virtual void Activate(bool showMouse)
    {
        GameManager.GetInstance().ChangeState(GameManager.State.Puzzle);
        if (showMouse){
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
        //FindObjectOfType<PuzzleManager>().movement.enabled = false;
        //FindObjectOfType<PuzzleManager>().abil.enabled = false;


    }

    public virtual void Reset()
    {

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
       // FindObjectOfType<PuzzleManager>().movement.enabled = true;
        //<PuzzleManager>().abil.enabled = true;
        FindObjectOfType<PuzzleManager>().currentPuzzle = null;

        GameManager.GetInstance().ChangeState(GameManager.State.Playing);
    }
}
