using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static LockDown lockdown;
    public static DoorController door;
    public enum PuzzleState { Activated, InProgress, Finished, Failed, Passed};
    public enum PuzzleType { Tetris, Shapes};
    public void ChoosePuzzle(int type)
    {
        if (type == (int)PuzzleType.Tetris)
        {
            //call tetris puzzle
        }
        else if (type == (int)PuzzleType.Shapes)
        {
            //call shapes puzzle
        }
    }

    private void Start()
    {
        lockdown = FindObjectOfType<LockDown>();
    }
}
