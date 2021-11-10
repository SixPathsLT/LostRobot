using System;
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

    internal void Close()
    {

      
      StartCoroutine(ChangeColor());
        _doorAnim.SetBool("IsOpening", false);

        Locked = true;
    }

    IEnumerator ChangeColor()
    {
        transform.parent.GetComponentInParent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(3);
        transform.parent.GetComponentInParent<Renderer>().material.color = Color.white;
    }

    public void HandleDoor()
    {
        OpenDoor();


    }
    public void OpenDoor()
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
   

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Enemy>() != null)
            OpenDoor();

        if (Input.GetKey(KeyCode.Q))
            HandleDoor();

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

