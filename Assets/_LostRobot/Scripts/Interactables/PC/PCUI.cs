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
    CameraController controller;

    private void Start()
    {
        puzzle = FindObjectOfType<PuzzleManager>();
        controller = FindObjectOfType<CameraController>();
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
        if (Input.GetKeyDown(KeyCode.E) && trigger)
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
        Close();
        trigger = false;

    }

    void DisplayText()
    {
        canvas.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        text.GetComponent<Text>().text = file.text;
        textOpened = true;
        controller.enabled = false;

    }

    void Close()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        text.GetComponent<Text>().text = "";
        canvas.SetActive(false);
        textOpened = false;
        controller.enabled = true;

    }

    private void Update()
    {
        KeyPressed();
    }
}
