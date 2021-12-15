
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController Controller;
    public Animator anim;
    public float speed = 12f;
    public float gravity = 10f;
    public GameObject focus;

    public float turnSmoothTime = 0.1f;
   public float turnSmoothVelocity;
    AbilitiesManager abilitiesManager;

    [HideInInspector]
    public Vector3 direction;
    internal bool inCombat;

    private void Start()
    {
        abilitiesManager = GetComponent<AbilitiesManager>();
    }

    void Update() {
        if (!GameManager.GetInstance().InPlayingState()) {
            direction = Vector3.zero;
            anim.SetFloat("Vel", 0);
            return;
        }
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");


        //Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;
        direction = ((focus.transform.right * horizontal) + (focus.transform.forward * vertical)).normalized;
        direction.y = 0;

       anim.SetFloat("Vel", direction.magnitude);

        if (direction.magnitude >= 0.1f)
        {

            float targetAngle = (Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg);
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            float movementSpeed = speed;
            if (inCombat) {
                if (Utils.HasEntity(transform.position, false, 6)) {
                    if (!abilitiesManager.UsingSpeedAbility())
                        movementSpeed /= 2f;
                } else
                    inCombat = false;
            }

            Controller.Move(direction * movementSpeed * Time.deltaTime);
        }

        if (transform.position.y > 1)
            Controller.Move(Vector3.down * gravity * Time.deltaTime);


    }
}
