using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum State { Menu, Loading, Playing, Paused, Puzzle, Email, Cutscene, Captured }
    State currentState;

    static private GameManager instance;
    private string SAVE_PATH;
    private GameObject player;
    public PlayerData data;

    void Awake() {
        if (instance != null) {
            Destroy(this);
            return;
        } else
            instance = this;

        DontDestroyOnLoad(this);

        SAVE_PATH = Application.persistentDataPath + "/player.data";
        //Load();
    }

    public void StartGame() {
        currentState = State.Loading;
        SceneManager.LoadScene(data.level);
    }

    public void forcestart()
    {
        currentState = State.Playing;
    }
    private void LateUpdate() {
        if (currentState != State.Loading) 
            return;

        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
           return;
        currentState = State.Playing;

        if (data.GetEmailsCount() < 1)
            FindObjectOfType<CutsceneManager>().PlayCutscene(CutsceneManager.INTRODUCTION_CUTSCENE);
    }

    public void ChangeState(State requestedState) {
        bool approveChange = false;

        if (InPlayingState())
            approveChange = true;
        else if (InPausedState()) {
            switch (requestedState) {
                case State.Playing:
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
                case State.Captured:
                    approveChange = true;
                    break;
            }
        } else if (InEmailState()) {
            switch (requestedState) {
                case State.Playing:
                case State.Cutscene:
                    approveChange = true;
                    break;
            }
        } else if (InCapturedState()) {
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

    [System.Serializable]
    public struct SaveableData {
        public int level;
        public int consciousness;
        public int health;
        public List<string> readEmails;
        public List<string> obtainedKeyCards;
        public List<string> obtainedKeyInfo;

        public SaveableData(PlayerData data) {
            level = data.level;
            consciousness = data.currentConciousness;
            health = data.currentHealth;
            readEmails = data.readEmails;
            obtainedKeyCards = data.obtainedKeyCards;
            obtainedKeyInfo = data.obtainedKeyInfo;
        }
    }

    public void Save() {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(SAVE_PATH, FileMode.Create);
        formatter.Serialize(stream, new SaveableData(data));
        stream.Close();
    }

    public void Load() {
        if (!File.Exists(SAVE_PATH))
            return;

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(SAVE_PATH, FileMode.Open);
        SaveableData savedData = (SaveableData) formatter.Deserialize(stream);

        data.level = savedData.level;
        data.currentConciousness = savedData.consciousness;
        data.currentHealth = savedData.health;
        data.readEmails = savedData.readEmails;
        data.obtainedKeyCards = savedData.obtainedKeyCards;
        data.obtainedKeyInfo = savedData.obtainedKeyInfo;

        stream.Close();

        StartGame();
    }

    public void NewGame() {
        if (File.Exists(SAVE_PATH))
            File.Delete(SAVE_PATH);

        data.Reset();
        StartGame();
    }

    public State GetCurrentState() { return currentState; }

    public bool InPlayingState() { return currentState == State.Playing; }
    public bool InPausedState() { return currentState == State.Paused; }
    public bool InCutsceneState() { return currentState == State.Cutscene; }
    public bool InPuzzleState() { return currentState == State.Puzzle; }
    public bool InEmailState() { return currentState == State.Email; }
    public bool InCapturedState() { return currentState == State.Captured; }

    public static GameManager GetInstance() {
        if (instance == null)
            instance = new GameObject().AddComponent<GameManager>();
        return instance;
    }

}
