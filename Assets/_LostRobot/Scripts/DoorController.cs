using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    Animator _doorAnim;
    public bool Locked = true;
    LockDown LockDown;
    private void Awake()
    {
        LockDown = FindObjectOfType<LockDown>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!LockDown.LockDownInitiated)
        {
            _doorAnim.SetBool("IsOpening", true);

            Locked = false;

        }
        else if (LockDown.LockDownInitiated && !Locked)
        {
            _doorAnim.SetBool("IsOpening", true);
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _doorAnim.SetBool("IsOpening", false);
    }
    public void Start()
    {

        _doorAnim = this.transform.parent.GetComponent<Animator>();


    }




}

