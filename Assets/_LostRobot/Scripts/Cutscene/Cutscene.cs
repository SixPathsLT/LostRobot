using UnityEngine;
using UnityEngine.Playables;

public abstract class Cutscene : MonoBehaviour {

    public GameObject[] objectsToDisable;
    protected GameObject player;
    private PlayableDirector playableDirector;
    
    public virtual void Start() {
        playableDirector = GetComponent<PlayableDirector>();
        player = GameObject.FindGameObjectWithTag("Player");

        foreach (var obj in objectsToDisable)
            obj.SetActive(false);
    }

    public abstract void Process();

    public virtual void Stop() {
        foreach (var obj in objectsToDisable)
            obj.SetActive(true);

        playableDirector.Stop();
        gameObject.SetActive(false);
    }

    public PlayableDirector GetPlayableDirector() {
        return playableDirector;
    }
}
