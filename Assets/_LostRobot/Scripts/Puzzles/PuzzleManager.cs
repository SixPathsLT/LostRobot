using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [HideInInspector]
    public Puzzles currentPuzzle;
   // public AbilitiesManager abil;
   // public PlayerMovement movement;
    public Puzzles[] puzzles;
    int puzzleType;
    DoorController door;
    PCUI pc;

    private void Start()
    {
        //abil = FindObjectOfType<AbilitiesManager>();
        //movement = FindObjectOfType<PlayerMovement>();
    }
    public void ChooseDoorPuzzle(DoorController door)
    {
        if (!GameManager.GetInstance().InPlayingState())
            return;

        puzzleType = Random.Range(0, puzzles.Length);
        this.door = door;

        currentPuzzle = puzzles[puzzleType];
        currentPuzzle.Activate(true);
        //puzzles[puzzleType].Activate(true);

        pc = null;
    }

    public void ChoosePCPUzzle(PCUI pc)
    {
        if (!GameManager.GetInstance().InPlayingState())
            return;

        puzzleType = Random.Range(0, puzzles.Length);
        this.pc = pc;

        currentPuzzle = puzzles[puzzleType];
        currentPuzzle.Activate(true);
        //puzzles[puzzleType].Activate(true);

        door = null;
    }

    public void Unlock()
    {
        if (door != null)
            door.Locked = false;
        else if (pc != null)
        {
            pc.locked = false;
            pc.DisplayText();
        }
        door = null;
        pc = null;
    }
}
