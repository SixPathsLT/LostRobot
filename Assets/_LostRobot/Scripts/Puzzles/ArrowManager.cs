using UnityEngine;

public class ArrowManager : Puzzles
{
    public int guesses = 3;
    [HideInInspector]
   public int completedDials = 0;

    [HideInInspector]
    public int fails;
    
    public override void Activate(bool showMouse)
    {
        base.Activate(true);
        canvas.SetActive(true);
        state = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    
    public void IncrementDials(){
        completedDials++;
        if (completedDials >= 3) {
            puzzleManager.win.Play();
            Reset();
            puzzleManager.Unlock();
        }
    }

    public void IncrementFails() {
        fails++;
        if (fails >= guesses) {
            puzzleManager.fail.Play();
            Reset();
        }
    }

    public override void Reset()
    {
        base.Reset();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;

        state = false;
        completedDials = 0;
        canvas.SetActive(false);
        
        
    }
}
