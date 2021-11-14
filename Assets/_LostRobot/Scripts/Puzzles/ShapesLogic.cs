using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShapesLogic : Puzzles
{
    [HideInInspector]
    public List<GameObject> puzzleImages = new List<GameObject>();

    int[] correctImages = new int[4];
    int[] displayedImages = new int[6];
    public int width = 1;
    public int displayYOffest = -1;
    public int displayXOffset = 0;
    public int choicesXOffset = -2;
    public int choicesYOffset = 3;

    private RaycastHit hit;
    private int imageCount = 0;
    public float timer;
    float countDown;
    bool state = false;

    public GameObject canvas;

    private void LoadImages() //loads all the images in the folder
    {
        object[] loadedImages = Resources.LoadAll("Image Prefabs", typeof(GameObject));
        for (int i = 0; i < loadedImages.Length; i++)
            puzzleImages.Add((GameObject)loadedImages[i]);
        canvas.SetActive(true);
    }

    private void SelectRandom()
    {
        int[] index = new int[puzzleImages.Count];
        for (int i = 0; i < puzzleImages.Count; i++)
            index[i] = i;
        for (int j = 0; j < index.Length; j++)//shuffles the index number to select random images to display
        {
            int k = Random.Range(j, index.Length);
            int temp = index[j];
            index[j] = index[k];
            index[k] = temp;
        }
        for (int a = 0; a < correctImages.Length; a++)
            correctImages[a] = index[a];
        for (int b = 0; b < displayedImages.Length; b++)
            displayedImages[b] = index[b];
        for (int c = 0; c < displayedImages.Length; c++)//reshuffles images to be displayed so they appear in a random order
        {
            int d = Random.Range(c, displayedImages.Length);
            int temp2 = displayedImages[c];
            displayedImages[c] = displayedImages[d];
            displayedImages[d] = temp2;
        }
    }

    private void SetPosition()
    {
        int imageIndex;
        Vector3 position;
        for (int i = 0; i < correctImages.Length; i++)//sets target image the player has to match
        {
            imageIndex = correctImages[i];
            position = new Vector3(displayXOffset, displayYOffest, 0);
            GameObject image = Instantiate(puzzleImages[imageIndex], position, Quaternion.identity);
            image.transform.parent = canvas.transform;
            displayXOffset = displayXOffset + width;
        }
        for (int j = 0; j < displayedImages.Length; j++)//sets position of images the player can choose from
        {
            imageIndex = displayedImages[j];
            position = new Vector3(choicesXOffset, choicesYOffset, 0);
            choicesXOffset = choicesXOffset + width;
            bool isChosen = false;
            for (int k = 0; k < correctImages.Length; k++)
            {
                if (displayedImages[j] == correctImages[k] && isChosen == false)
                {
                    puzzleImages[imageIndex].tag = "Solution";
                    isChosen = true;
                    break;
                }
            }
            if (!isChosen)
            {
                puzzleImages[imageIndex].tag = "Puzzle button";
            }
            GameObject image = Instantiate(puzzleImages[imageIndex], position, Quaternion.identity);
            image.transform.parent = canvas.transform;
        }
    }

    private void SelectOptions(bool active)
    {
        if (active)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(ray, out hit) && hit.collider.tag == "Solution")
                {
                    imageCount++;
                    hit.collider.tag = "Solved";
                    Destroy(hit.collider.gameObject);
                }
                if (imageCount == correctImages.Length)
                {
                    Debug.Log("You won!");
                    PuzzleManager.door.Locked = false;
                    state = false;
                    canvas.SetActive(false);
                }
            }
        }
    }

    public override void Activate()
    {
        base.Activate();
        LoadImages();
        SelectRandom();
        SetPosition();
        state = true;
    }

    private void Update()
    {
        if (state)
        {
            countDown += Time.deltaTime;
            if (countDown > timer)
            {
                Debug.Log("Time's up!");
                state = false;
                SelectOptions(state);
                canvas.SetActive(false);
                Fail();
            }
            else
            {
                SelectOptions(state);
            }
        }
    }

}
