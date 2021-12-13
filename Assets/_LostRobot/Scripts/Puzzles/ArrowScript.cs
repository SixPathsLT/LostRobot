using UnityEngine;
public class ArrowScript : Puzzles
{
    
    public GameObject center;
    public float rotationSpeed;
    public RectTransform indicator;
    bool inLock = false;
    bool spin = true;

    RectTransform rect;
    ArrowManager arrowManager;

    private void Start() {
        arrowManager = FindObjectOfType<ArrowManager>();
        rect = GetComponent<RectTransform>();
    }

    void Update()
    {
      
        if (spin)
        {
            transform.RotateAround(center.transform.position, Vector3.forward, rotationSpeed * Time.deltaTime);
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
        if (collision.CompareTag("DialLock") && !inLock)
        {
            inLock = true;
            arrowManager.IncrementDials();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("DialLock") && inLock)
        {
            inLock = false;
        }
    }
    
    /*public void CheckIfInLock()
    {
        if (inLock)
        {
            spin = false;
        }
    }*/
}
