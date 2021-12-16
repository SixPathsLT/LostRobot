using UnityEngine;
public class ArrowScript : MonoBehaviour
{
    
    public GameObject center;
    public GameObject dial;
    public float rotationSpeed;
    public RectTransform indicator;
    bool inLock = false;
    bool spin = true;

    RectTransform rect;
    ArrowManager arrowManager;

    private void OnEnable() {
        arrowManager = FindObjectOfType<ArrowManager>();
        rect = GetComponent<RectTransform>();
        spin = true;
        inLock = false;
    }

    void Update()
    {
      
        if (spin)
        {
            transform.RotateAround(center.transform.position, Vector3.forward, rotationSpeed * Time.deltaTime);
            dial.transform.RotateAround(center.transform.position, Vector3.forward, rotationSpeed * Time.deltaTime);
        }
        else
        {
            rect.localPosition = new Vector3(50, 0, 0);
            rect.localRotation = Quaternion.Euler(0, 0, 0);
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
    
    public void CheckIfInLock() {
        if (inLock && spin) {
            spin = false;
            arrowManager.IncrementDials();
        } else if (spin && !inLock) {
            arrowManager.IncrementFails();
        }
    }
}
