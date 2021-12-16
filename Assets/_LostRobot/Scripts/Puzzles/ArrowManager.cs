using UnityEngine;
using UnityEngine.UI;

public class ArrowManager : Puzzles
{
    public Text attemps;
    public int guesses = 3;
    [HideInInspector]
   public int completedDials = 0;

    [HideInInspector]
    public int fails;
    
    public override void Activate()
    {
        base.Activate();
        canvas.SetActive(true);
        attemps.text = "" + guesses;
        state = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    
    public void IncrementDials(){
        completedDials++;
        puzzleManager.click.Play();
        if (completedDials >= 3) {
            puzzleManager.win.Play();
            Reset();
            puzzleManager.Unlock();
        }
    }

    public void IncrementFails() {
        fails++;
        puzzleManager.wrong.Play();
        attemps.text = "" + (guesses - fails);
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
