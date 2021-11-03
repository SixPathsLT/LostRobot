using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool IsOpen;
    Animator _doorAnim;


    public void Start()
    {

        _doorAnim = this.transform.GetComponent<Animator>();


    }

    public void Close ()
    {
        IsOpen = false;

        _doorAnim.SetBool("IsOpening", false);

    }

    public void Open()
    {
        IsOpen = true;

        _doorAnim.SetBool("IsOpening", true);
    }
    
}
