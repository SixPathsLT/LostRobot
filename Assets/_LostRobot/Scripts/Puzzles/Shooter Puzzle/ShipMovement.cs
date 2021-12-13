
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    readonly int maxPos = 750;
    readonly int minPos = -750;
    public float speed;
    RectTransform rectMove;
    Vector3 initialPos;
    private void Start()
    {
        rectMove = GetComponent<RectTransform>();
        initialPos = rectMove.anchoredPosition;
        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            if (rectMove.offsetMin.x >= minPos)
            {
                transform.Translate(Vector2.left * speed);
            }

        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            if (rectMove.offsetMax.x <= maxPos)
            {
                transform.Translate(Vector2.right * speed);
            }
        }
    }

    public void ResetPosition()
    {
        rectMove.anchoredPosition = initialPos;
    }

}
