
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Journal : MonoBehaviour
{
    public GameObject prefabButton;
    public GameObject panel;
    Dictionary<string, PCUI> entries = new Dictionary<string, PCUI>(); 

    void Start() {
        foreach (var id in GameManager.GetInstance().data.readEmails) {
            foreach (var pcUI in FindObjectsOfType<PCUI>()) {
                if (pcUI.ID == id)
                    CreateEntry(pcUI);
            }
        }
    }

    public void CreateEntry(PCUI pcUI) {
        if (entries.ContainsKey(pcUI.ID))
            return;

        GameObject buttonObj = Instantiate(prefabButton);
        buttonObj.GetComponent<Button>().onClick.AddListener(OpenEntry);
        buttonObj.GetComponent<Button>().GetComponentInChildren<Text>().text = pcUI.file.text;
        buttonObj.transform.SetParent(panel.transform, false);
        buttonObj.name = pcUI.ID;
        entries.Add(buttonObj.name, pcUI);
    }

    private void OpenEntry() {
        PCUI pcUI = entries[EventSystem.current.currentSelectedGameObject.name];
        if (pcUI != null) {
            FindObjectOfType<PauseMenu>().ResumeGame();
            pcUI.DisplayText();
        }
    }

}
