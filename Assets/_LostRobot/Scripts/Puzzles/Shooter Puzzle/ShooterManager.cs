using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShooterManager : Puzzles
{
    public GameObject ship;
    public GameObject bullet;
    public GameObject[] targetsArray;
    Queue<GameObject> targetsQueue = new Queue<GameObject>();
    public bool hasTimer;
    public Slider time;

    public AudioSource bul;
    public AudioSource hit;
    public override void Activate()
    {
        base.Activate();
        state = true;
        countDown = 0;
        SetTargetOrder();
        ActiveTarget();
        if (hasTimer)
            time.maxValue = timer;
        else
            time.gameObject.SetActive(false);
        canvas.SetActive(true);
    }
    private void Update()
    {
        if (state)
        {
            if (hasTimer)
            {
                time.value = timer - countDown;
                countDown += Time.deltaTime;
                if (countDown > timer)
                {
                    puzzleManager.fail.Play();
                    Reset();
                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        GameObject projectile = Instantiate(bullet, ship.transform.position, Quaternion.identity);
                        bul.Play();
                        projectile.transform.SetParent(ship.transform);;
                    }
                }
            }
            else if (!hasTimer)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    GameObject projectile = Instantiate(bullet, ship.transform.position, Quaternion.identity);
                    bul.Play();
                    projectile.transform.SetParent(ship.transform);
                }
            }

        }
        
    }

    void SetTargetOrder()
    {
        for (int i = 0; i < targetsArray.Length; i++)
        {
            int j = Random.Range(i, targetsArray.Length);
            var temp = targetsArray[i];
            targetsArray[i] = targetsArray[j];
            targetsArray[j] = temp;
        }
        for (int k = 0; k < targetsArray.Length; k++)
        {
            targetsQueue.Enqueue(targetsArray[k]);
        }
    }

    public void CheckTargetHit(GameObject target)
    {
        if (target == targetsQueue.Peek())
        {
            targetsQueue.Dequeue();
            if (targetsQueue.Count > 0)
                ActiveTarget();
            else if (targetsQueue.Count <= 0)
            {
                puzzleManager.win.Play();
                Reset();
                puzzleManager.Unlock();
            }
        }
        else
        {
            puzzleManager.fail.Play();
            Reset();
        }
    }

    void ActiveTarget()
    {
        targetsQueue.Peek().GetComponent<Image>().color = Color.green;
    }

    public override void Reset()
    {
        base.Reset();
        FindObjectOfType<ShipMovement>().ResetPosition();
        foreach (GameObject obj in targetsArray)
        {
            obj.SetActive(true);
            obj.GetComponent<Image>().color = Color.red;
        }
        targetsQueue.Clear();
        state = false;
        countDown = 0;
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (GameObject bullet in bullets)
            Destroy(bullet);
        canvas.SetActive(false);
        countDown = 0;
    }
}
