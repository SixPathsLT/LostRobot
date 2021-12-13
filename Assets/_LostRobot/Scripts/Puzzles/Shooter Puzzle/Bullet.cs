
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    [SerializeField] private float destroyTime = 3f;
    private void Start()
    {
        GetComponent<Rigidbody2D>().velocity = transform.up * speed;
    }

    internal void DestroySelf()
    {
        gameObject.SetActive(false);
        Destroy(this);
    }

    private void Awake()
    {
        Invoke(nameof(DestroySelf), destroyTime);
    }
}
