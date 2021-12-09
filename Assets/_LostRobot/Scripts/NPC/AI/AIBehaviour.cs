using UnityEngine;

public abstract class AIBehaviour : ScriptableObject
{

    public abstract void Init(AIManager aiManager);
    public abstract void Process(AIManager aiManager);
    public abstract void End(AIManager aiManager);
}
