using UnityEngine;
using UnityEngine.UI;

public class PCUI : MonoBehaviour
{

    public static PCUI current;
    public bool locked;
    public bool triggerPuzzle;
    public TextAsset file;
    PuzzleManager puzzle;
    public GameObject canvas;
    public Text text;
    bool textOpened = false;
    bool trigger;

    bool read = false;
    //CameraController controller;

    [SerializeField] string id;
    public string ID { get { return id; } }

    PlayerData data;
    private void Start()
    {
        id = name;
        puzzle = FindObjectOfType<PuzzleManager>();
        //controller = FindObjectOfType<CameraController>();
        data = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().data;
        if (data.readEmails.Contains(id)) {
            read = true;
            locked = false;
        }

        if (!read)
            AreasOfInterest.Register(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            trigger = true;
        }
    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.E) && textOpened && GameManager.GetInstance().InEmailState()) {
            Close();
            return;
        }
        if (Input.GetKeyDown(KeyCode.E) && trigger && GameManager.GetInstance().InPlayingState())
        {
            if (locked && triggerPuzzle)
                puzzle.ChoosePCPUzzle(this);
            else if (!locked && !textOpened)
                DisplayText();
            else if (!locked && textOpened)
                Close();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) {
            //Close();
            trigger = false;
        }

    }

    private void OnTriggerStay(Collider other) { 
        if (other.CompareTag("Player"))
            trigger = true;
    }

    public void DisplayText()
    {
        current = this;
        GameManager.GetInstance().ChangeState(GameManager.State.Email);
        canvas.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        text.GetComponent<Text>().text = file.text;
        textOpened = true;
        //controller.enabled = false;

        if (!read)
        {
            //GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().data.IncreasePCCount();
            data.AddEmail(id);
            read = true;
            FindObjectOfType<Journal>(true).CreateEntry(this);
            AreasOfInterest.DeRegister(gameObject);
        }
    }

    public void Close()
    {
        current = null;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        text.GetComponent<Text>().text = "";
        canvas.SetActive(false);
        textOpened = false;
        //controller.enabled = true;
        if (!GameManager.GetInstance().InCapturedState())
            GameManager.GetInstance().ChangeState(GameManager.State.Playing);
    }

}
