using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    Animator _doorAnim;
    public bool Locked;
    public bool triggerPuzzle;
    LockDown LockDown;
    public PuzzleManager puzzle;

    private AudioPlayer audio;
    private void Awake()
    {
        LockDown = FindObjectOfType<LockDown>();
        puzzle = FindObjectOfType<PuzzleManager>();
        _doorAnim = GetComponentInParent<Animator>();
        audio = GetComponent<AudioPlayer>();
        audio.source.spatialBlend = 1;
        audio.source.volume = .3f;
    }

    internal void Close()
    {
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
    public void Start()
    {
        _doorAnim = this.transform.parent.GetComponent<Animator>();
        puzzle = FindObjectOfType<PuzzleManager>();
    }

    public void Lock()
    {
        _doorAnim.SetBool("Lockdown", true);
        transform.parent.GetComponentInParent<Renderer>().material.color = Color.red;
    }

    public void Unlock()
    {
        _doorAnim.SetBool("Lockdown", false);
        transform.parent.GetComponentInParent<Renderer>().material.color = Color.white;

    }
}

