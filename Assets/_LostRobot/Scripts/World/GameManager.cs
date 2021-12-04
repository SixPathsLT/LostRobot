using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum State { Playing, Paused, Puzzle, Email, Cutscene }
    State currentState;

    static private GameManager instance;

    void Awake() {
        if (instance != null)
            Destroy(this);
        else
            instance = this;
    }

    public void ChangeState(State requestedState) {
        bool approveChange = false;

        if (InPlayingState()) {
            switch (requestedState) {
                case State.Paused:
                    PauseGame();
                    break;
            }
            approveChange = true;
        } else if (InPausedState()) {
            switch (requestedState) {
                case State.Playing:
                    ResumeGame();
                    approveChange = true;
                    break;
            }
        } else if (InPuzzleState()) {
            switch (requestedState) {
                case State.Playing:
                    ClosePuzzle();
                    approveChange = true;
                    break;
            }
        } else if (InCutsceneState()) {
            switch (requestedState) {
                case State.Playing:
                    approveChange = true;
                    break;
            }
        } else if (InEmailState()) {
            switch (requestedState) {
                case State.Playing:
                    approveChange = true;
                    break;
            }
        }

        if (approveChange)
            currentState = requestedState;

    }

    public void OnRespawn() {
        ClosePuzzle();
    }

    void ClosePuzzle() {
        Puzzles puzzle = FindObjectOfType<PuzzleManager>().currentPuzzle;
        if (puzzle != null)
            puzzle.Reset();
    }

    void PauseGame() {
        FindObjectOfType<PauseMenu>().Pause();
    }
    void ResumeGame() {
        FindObjectOfType<PauseMenu>().ResumeGame();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public bool InPlayingState() { return currentState == State.Playing; }
    public bool InPausedState() { return currentState == State.Paused; }
    public bool InCutsceneState() { return currentState == State.Cutscene; }
    public bool InPuzzleState() { return currentState == State.Puzzle; }
    public bool InEmailState() { return currentState == State.Email; }

    public static GameManager GetInstance() {
        if (instance == null)
            instance = new GameObject().AddComponent<GameManager>();
        return instance;
    }
}
