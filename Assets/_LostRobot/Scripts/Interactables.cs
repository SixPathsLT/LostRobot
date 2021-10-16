using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactables : MonoBehaviour
{
    public bool isInRange;
    public KeyCode InteractKey;
    public UnityEvent interactAction;


    void Start()
    {






    }


    void Update()
    {
        if (isInRange)
        {
            if (Input.GetKeyDown(InteractKey))
            {
                interactAction.Invoke();
                Debug.Log("Door is open");
            }

        }

        void OnTriggerEnter(Collider collision)
    {
            if(collision.gameObject.CompareTag("Player"))
            {
                isInRange = true;
                Debug.Log("Player now is in range");

            }


            void onTriggerExit(Collider collision)
            {


                if (collision.gameObject.CompareTag("Player"))
                {
                    isInRange = false;
                    Debug.Log("Player now is not in range");

                }

            }
        
    }


}






    }
    



