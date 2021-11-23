using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public AbilitiesManager abil;
    public Puzzles[] puzzles;
    int puzzleType;
    DoorController door;
    PCUI pc;

    private void Start()
    {
        abil = FindObjectOfType<AbilitiesManager>();
    }
    public void ChooseDoorPuzzle(DoorController door)
    {
        puzzleType = Random.Range(0, puzzles.Length);
        this.door = door;
        puzzles[puzzleType].Activate();
        pc = null;
    }

    public void ChoosePCPUzzle(PCUI pc)
    {
        puzzleType = Random.Range(0, puzzles.Length);
        this.pc = pc;
        puzzles[puzzleType].Activate();
        door = null;
    }

    public void Unlock()
    {
        if (door != null)
            door.Locked = false;
        else if (pc != null)
            pc.locked = false;
        door = null;
        pc = null;
    }
}
