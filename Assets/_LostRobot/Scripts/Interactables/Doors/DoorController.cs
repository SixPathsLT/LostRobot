
using UnityEngine;

public class DoorController : MonoBehaviour
{
    Animator _doorAnim;
    public bool Locked;
    public bool triggerPuzzle;
    LockDown LockDown;
    public PuzzleManager puzzle;
    public bool cardRequired;

    private AudioPlayer audio;
    static GameObject[] doorsLights;

    public void Start()
    {
        LockDown = FindObjectOfType<LockDown>();
        puzzle = FindObjectOfType<PuzzleManager>();
        _doorAnim = GetComponentInParent<Animator>();
        audio = GetComponent<AudioPlayer>();
        audio.source.spatialBlend = 1;
        audio.source.volume = .1f;
        _doorAnim = this.transform.parent.GetComponent<Animator>();
        //puzzle = FindObjectOfType<PuzzleManager>();

       UpdateDoorColors();
    }

    void UpdateDoorColors() {
        Color color = (Locked && cardRequired) ? Color.yellow : (Locked && triggerPuzzle) ? Color.magenta : Locked ? Color.red : Color.cyan;
        for (int i = 0; i < transform.parent.childCount; i++) {
            Transform child = transform.parent.GetChild(i);
            if (!child.CompareTag("DoorLight"))
                continue;

            child.GetComponent<Renderer>().materials[1].SetColor("_EmissionColor", color * 2f);
            child.GetComponentInChildren<Light>().color = color;
        }

    }


    internal void Close()
    {
        UpdateDoorColors();

        _doorAnim.SetBool("Open", false);
        if (_doorAnim.GetCurrentAnimatorStateInfo(0).IsName("door_3_opened"))
            audio.PlayClip("Door_Close_SFX");
        //Commented to prevent doors from locking when walking away
        //Locked = true;
    }


    public void HandleDoor()
    {
        OpenDoor();
    }
    public void OpenDoor()
    {
        if (!GameManager.GetInstance().InPlayingState())
            return;
        UpdateDoorColors();

        if (!LockDown.LockDownInitiated && !Locked)
        {
            _doorAnim.SetBool("Open", true);
            audio.PlayClip("Door_Open_SFX");
            Locked = false;

        }
        else if (LockDown.LockDownInitiated && !Locked)
        {
            _doorAnim.SetBool("Open", true);
            //audio.PlayClip("Door_Open_SFX");
        }
    }
   

    private void OnTriggerStay(Collider other) {
        if (other.GetComponent<AIManager>() != null)
        {
            _doorAnim.SetBool("AIinRange", true);
            OpenDoor();
        }            

        if (other.CompareTag("Player") && Input.GetKey(KeyCode.E)) {
            HandleDoor();

            if (Locked && triggerPuzzle)
                puzzle.ChooseDoorPuzzle(this);
            else if (Locked && cardRequired)
                FindObjectOfType<Notification>().SendNotification("Access Card Required");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Close();

        if (other.GetComponent<AIManager>() != null)
        {
            _doorAnim.SetBool("AIinRange", false);
        }
    }

    public void Lock()
    {
        _doorAnim.SetBool("Lockdown", true);
        // transform.parent.GetComponentInParent<Renderer>().material.color = Color.red;
       // UpdateDoorColors();

    }

    public void Unlock()
    {

        _doorAnim.SetBool("Lockdown", false);
        //transform.parent.GetComponentInParent<Renderer>().material.color = Color.white;
       // UpdateDoorColors();

    }
}

