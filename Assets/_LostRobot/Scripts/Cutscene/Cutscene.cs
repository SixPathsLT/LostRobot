using UnityEngine;
using UnityEngine.Playables;

public abstract class Cutscene : MonoBehaviour {
    [HideInInspector]
    public GameObject playerCam;

    public GameObject[] objectsToDisable;
    protected GameObject player;
    private PlayableDirector playableDirector;
    
    public virtual void Init() {
        GameManager.GetInstance().ChangeState(GameManager.State.Cutscene);
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
        foreach (var obj in objectsToDisable)
            obj.SetActive(true);

        if (playableDirector != null)
            playableDirector.Stop();

        GameManager.GetInstance().ChangeState(GameManager.State.Playing);
    }

    public PlayableDirector GetPlayableDirector() {
        return playableDirector;
    }

}
