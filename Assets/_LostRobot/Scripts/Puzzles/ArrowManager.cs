using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowManager : Puzzles
{
    
    public GameObject canvas;
    bool state = false;

    public override void Activate(bool showMouse)
    {
        base.Activate(true);
        canvas.SetActive(true);
        state = true;
       
    }
    void Update()
    {



    }

    public override void Reset()
    {
        base.Reset();
        FindObjectOfType<ArrowScript>();
        
        
        state = false;
        
        
    }
}
