using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShapesLogic : Puzzles
{
    private List<Sprite> puzzleImages = new List<Sprite>();

    int[] correctImages = new int[4];
    int[] displayedImages = new int[6];

    private GraphicRaycaster hit;
    PointerEventData pointerData;
    EventSystem eventSystem;
    private int imageCount = 0;
    public float timer;
    float countDown;
    public int maxGuesses;
    private int remainingGuesses;
    bool state = false;
    public Text guesses;
    public Slider time;

    public GameObject canvas;
    public Image[] optionsImages;
    public Image[] displayImages;


    private void Start()
    {
        object[] loadedImages = Resources.LoadAll("Shapes", typeof(Sprite));
        for (int i = 0; i < loadedImages.Length; i++)
            puzzleImages.Add((Sprite)loadedImages[i]);
        hit = canvas.GetComponent<GraphicRaycaster>();
        eventSystem = GetComponent<EventSystem>();
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

    private void SetTags()
    {
        int imageIndex;
        for (int i = 0; i < correctImages.Length; i++)//sets target image the player has to match
        {
            imageIndex = correctImages[i];
            displayImages[i].GetComponent<Image>().sprite = puzzleImages[imageIndex];
        }
        for (int j = 0; j < displayedImages.Length; j++)//sets tags of images the player can choose from
        {
            imageIndex = displayedImages[j];
            bool isChosen = false;
            for (int k = 0; k < correctImages.Length; k++)
            {
                if (displayedImages[j] == correctImages[k] && isChosen == false)
                {
                    optionsImages[j].tag = "Solution";
                    isChosen = true;
                    break;
                }
            }
            if (!isChosen)
            {
                optionsImages[j].tag = "Incorrect";
            }
            optionsImages[j].GetComponent<Image>().sprite = puzzleImages[imageIndex];
        }
    }

    private void SelectOptions(bool active)
    {
        if (active)
        {
            if (Input.GetMouseButtonDown(0))
            {
                pointerData = new PointerEventData(eventSystem);
                pointerData.position = Input.mousePosition;
                List<RaycastResult> results = new List<RaycastResult>();
                hit.Raycast(pointerData, results);
                foreach (RaycastResult result in results)
                {
                    if (result.gameObject.tag == "Solution")
                    {
                        imageCount++;
                        result.gameObject.tag = "Solved";
                        result.gameObject.GetComponent<Image>().color = Color.gray;
                    }
                    else if (result.gameObject.tag == "Incorrect")
                    {
                        remainingGuesses--;
                        guesses.text = "" + remainingGuesses;
                    }
                }
                if (remainingGuesses <= 0)
                {
                    FindObjectOfType<Notification>().SendNotification("You Ran out of Guesses");
                    Reset();
                }
                if (imageCount == correctImages.Length)
                {
                    Debug.Log("You won!");
                    Reset();
                    FindObjectOfType<PuzzleManager>().Unlock();
                }
            }
        }
    }

    public override void Activate(bool showMouse)
    {
        base.Activate(true);
        canvas.SetActive(true);
        SelectRandom();
        SetTags();
        remainingGuesses = maxGuesses;
        guesses.text = "" + remainingGuesses;
        time.maxValue = timer;        
        state = true;
        FindObjectOfType<PuzzleManager>().abil.enabled = false;
    }

    private void Update()
    {
        if (state)
        {
            time.value = countDown;
            countDown += Time.deltaTime;
            if (countDown > timer)
            {
                Debug.Log("Time's up!");
                Reset();
            }
            else
            {
                SelectOptions(state);
            }
        }
    }

    public override void Reset()
    {
        base.Reset();
        canvas.SetActive(false);
        FindObjectOfType<PuzzleManager>().abil.enabled = true;
        countDown = 0;
        imageCount = 0;
        foreach (Image image in optionsImages)
            image.color = Color.white;
        state = false;
        SelectOptions(state);
    }

}
