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
    private void Awake()
    {
        LockDown = FindObjectOfType<LockDown>();
        _doorAnim = GetComponentInParent<Animator>();
    }

    internal void Close()
    {
        _doorAnim.SetBool("Open", false);

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

            Locked = false;

        }
        else if (LockDown.LockDownInitiated && !Locked)
        {
            _doorAnim.SetBool("Open", true);

        }
    }
   

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Locked && triggerPuzzle)
            {
                puzzle.ChooseDoorPuzzle(this);
            }
        }

        if (other.GetComponent<AIManager>() != null)
            OpenDoor();

        if (Input.GetKey(KeyCode.E))
            HandleDoor();

    }

    private void OnTriggerExit(Collider other)
    {
        Close();
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

