
using UnityEngine;

public class TargetMovement : MonoBehaviour
{
    public float speed = 3f;
    Vector3 movePos = new Vector3(1, 0, 0);
    RectTransform rect;

    public void Start() {
        rect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (rect.offsetMin.x >= 750 || GetComponent<RectTransform>().offsetMax.x <= -750)
        {
            movePos *= -1;
        }
            
        if (rect.anchoredPosition.x > 801)
        {
            rect.anchoredPosition = new Vector2(800, 0);
        }
        else if (rect.anchoredPosition.x < -801)
        {
            rect.anchoredPosition = new Vector2(-800, 0);

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
            FindObjectOfType<ShooterManager>().hit.Play();
        }
    }
}
