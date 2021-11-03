using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    bool isStunned;

    void Start() { }
    
    void Update() {
        Move();
    }

    float timeElapsed = 0;
    private void Move() {
        if (isStunned)
            return;

        timeElapsed += Time.deltaTime;
        transform.position += (timeElapsed < 5 ? transform.right : -transform.right) * Time.deltaTime;

        if (timeElapsed > 10)
            timeElapsed = 0;

    }


    public IEnumerator Stun(float duration) {
        isStunned = true;

        yield return new WaitForSeconds(duration);

        isStunned = false;
    }
}
