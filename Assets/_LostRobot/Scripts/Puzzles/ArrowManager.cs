using UnityEngine;

public class ArrowManager : Puzzles
{
    [HideInInspector]
   public int completedDials = 0;
    
    public override void Activate(bool showMouse)
    {
        base.Activate(true);
        canvas.SetActive(true);
        state = true;
       
    }
    
    public void IncrementDials(){
        completedDials++;
        if (completedDials >= 3) {
            puzzleManager.win.Play();
            Reset();
            puzzleManager.Unlock();
        }
    }

    public override void Reset()
    {
        base.Reset();
        
        
        state = false;
        
        
    }
}
