using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIBehaviour : ScriptableObject
{
    protected GameObject gameObject;
    protected AIManager aiManager;

    public virtual void Init(GameObject gameObject) { 
        this.gameObject = gameObject;
        aiManager = gameObject.GetComponent<AIManager>();
    }

    public abstract void Process();
    public abstract void End();
}
