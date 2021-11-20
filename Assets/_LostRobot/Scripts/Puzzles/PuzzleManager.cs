using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static LockDown lockdown;
    public static DoorController doors;
    public Puzzles[] puzzles;
    //public enum PuzzleState { Activated, InProgress, Finished, Failed, Passed};
    //public enum PuzzleType { Tetris, Shapes};
    public void ChoosePuzzle(int type)
    {
        puzzles[type].Activate();
    }

    private void Start()
    {
        lockdown = FindObjectOfType<LockDown>();
        doors = GetComponentInParent<DoorController>();
    }
}
