
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [HideInInspector]
    public Puzzles currentPuzzle;
    public Puzzles[] puzzles;
    int puzzleType;
    DoorController door;
    PCUI pc;

    public AudioSource win;
    public AudioSource fail;

    public void ChooseDoorPuzzle(DoorController door)
    {
        if (!GameManager.GetInstance().InPlayingState())
            return;
        this.door = door;
        OpenPuzzle();
        pc = null;
    }

    public void ChoosePCPUzzle(PCUI pc)
    {
        if (!GameManager.GetInstance().InPlayingState())
            return;
        this.pc = pc;
        OpenPuzzle();
        door = null;
    }

    private void OpenPuzzle() {
        puzzleType = Random.Range(0, puzzles.Length);
        currentPuzzle = puzzles[puzzleType];
        currentPuzzle.Activate();
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
