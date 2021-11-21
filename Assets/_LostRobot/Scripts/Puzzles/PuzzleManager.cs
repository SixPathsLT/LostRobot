using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public Puzzles[] puzzles;
    int puzzleType;
    DoorController door;

    public void ChoosePuzzle(DoorController door)
    {
        puzzleType = Random.Range(0, puzzles.Length);
        this.door = door;
        puzzles[puzzleType].Activate();
        
    }

    public void Unlock()
    {
        door.Locked = false;
    }
}
