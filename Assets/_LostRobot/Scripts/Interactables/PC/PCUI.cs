using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class PCUI : MonoBehaviour
{
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

    private void Start()
    {
        puzzle = FindObjectOfType<PuzzleManager>();
        //controller = FindObjectOfType<CameraController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            trigger = true;
        }
    }

    private void KeyPressed()
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
        if (!read)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().data.IncreasePCCount();
            read = true;
        }        
        GameManager.GetInstance().ChangeState(GameManager.State.Email);
        canvas.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        text.GetComponent<Text>().text = file.text;
        textOpened = true;
        //controller.enabled = false;
    }

    public void Close()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        text.GetComponent<Text>().text = "";
        canvas.SetActive(false);
        textOpened = false;
        //controller.enabled = true;
        GameManager.GetInstance().ChangeState(GameManager.State.Playing);
    }

    private void Update()
    {
        KeyPressed();
    }
}
