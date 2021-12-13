
using UnityEngine;

public abstract class Puzzles : MonoBehaviour
{
    public float timer;
    public GameObject canvas;
    protected bool state = false;
    protected float countDown = 0;

    protected PuzzleManager puzzleManager;

    public virtual void Activate(bool showMouse)
    {
        puzzleManager = GetComponent<PuzzleManager>();
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
