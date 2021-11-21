using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public AbilitiesManager abil;
    public Puzzles[] puzzles;
    int puzzleType;
    DoorController door;

    private void Start()
    {
        abil = FindObjectOfType<AbilitiesManager>();
    }
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
