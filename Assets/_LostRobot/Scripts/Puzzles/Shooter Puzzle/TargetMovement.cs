using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMovement : MonoBehaviour
{
    public float speed = 3f;
    Vector3 movePos = new Vector3(1, 0, 0);
    float pos;

    private void Update()
    {
        if (GetComponent<RectTransform>().offsetMin.x >= 750 || GetComponent<RectTransform>().offsetMax.x <= -750)
        {
            movePos *= -1;
        }
            
        if (GetComponent<RectTransform>().anchoredPosition.x > 801)
        {
            GetComponent<RectTransform>().anchoredPosition = new Vector2(800, 0);
        }
        else if (GetComponent<RectTransform>().anchoredPosition.x < -801)
        {
            GetComponent<RectTransform>().anchoredPosition = new Vector2(-800, 0);

        }
        transform.Translate(movePos * speed * Time.deltaTime);
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
