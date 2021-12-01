using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMovement : MonoBehaviour
{
    public float speed = 3f;
    Vector3 movePos = new Vector3(1, 0, 0);

    private void Update()
    {
        transform.Translate(movePos * speed * Time.deltaTime);
        if (GetComponent<RectTransform>().offsetMin.x >= 750 || GetComponent<RectTransform>().offsetMax.x <= -750)
        {
            movePos.x *= -1;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            gameObject.SetActive(false);
            other.gameObject.SetActive(false);
            FindObjectOfType<ShooterManager>().CheckTargetHit(this.gameObject);
        }
    }
}
