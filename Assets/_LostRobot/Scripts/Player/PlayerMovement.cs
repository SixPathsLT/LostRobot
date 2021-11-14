using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController Controller;
    public Animator anim;
    public float speed = 12f;
    public GameObject focus;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

   
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");


        //Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;
        Vector3 direction = ((focus.transform.right * horizontal) + (focus.transform.forward * vertical)).normalized;
        direction.y = 0;

       anim.SetFloat("Vel", direction.magnitude);

        if (direction.magnitude >= 0.1f)
        {

            float targetAngle = (Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg);
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            
            Controller.Move(direction * speed * Time.deltaTime);

        }
    }
}
