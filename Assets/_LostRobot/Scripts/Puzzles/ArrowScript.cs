using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ArrowScript : Puzzles
{
    
    public GameObject center;
    public float rotationSpeed;
    public RectTransform indicator;
    bool inLock = false;
    bool spin = true;
    
    void Update()
    {
      
    
        if (spin)
        {
            transform.RotateAround(center.transform.position, Vector3.forward, rotationSpeed * Time.deltaTime);
        }
        else
        {
            GetComponent<RectTransform>().localPosition = new Vector3(50, 0, 0);
            GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, 0);
        }
        
        if (inLock)
        {
            indicator.sizeDelta = new Vector2(233,indicator.rect.height);
        }
        else
        {
            indicator.sizeDelta = new Vector2(15, indicator.rect.height);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DialLock"))
        {
            inLock = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("DialLock"))
        {
            inLock = false;
        }
    }
    
    public void CheckIfInLock()
    {
        if (inLock)
        {
            spin = false;
        }
    }
}
