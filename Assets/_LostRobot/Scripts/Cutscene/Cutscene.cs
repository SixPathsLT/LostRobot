using UnityEngine;
using UnityEngine.Playables;

public abstract class Cutscene : MonoBehaviour {
    [HideInInspector]
    public GameObject playerCam;

    public GameObject[] objectsToDisable;
    protected GameObject player;
    private PlayableDirector playableDirector;
    
    public virtual void Init() {
        playableDirector = GetComponent<PlayableDirector>();
        player = GameObject.FindGameObjectWithTag("Player");

        foreach (var obj in objectsToDisable) {
            obj.SetActive(false);
            if (obj.CompareTag("MainCamera"))
                playerCam = obj;
        }

        if (playableDirector != null)
            playableDirector.Play();
        
    }

    public abstract void Process();

    public virtual void Stop() {
        foreach (var obj in objectsToDisable) {
            if (obj == playerCam)
                continue;
            obj.SetActive(true);
        }

        if (playableDirector != null)
            playableDirector.Stop();
    }

    public PlayableDirector GetPlayableDirector() {
        return playableDirector;
    }

}
