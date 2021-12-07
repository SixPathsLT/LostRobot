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
    public GameObject canvas;
    bool state = false;
    public float timer;
    float countDown = 0;
    public bool hasTimer;
    public override void Activate(bool showMouse)
    {
        base.Activate(false);
        state = true;
        SetTargetOrder();
        ActiveTarget();
        canvas.SetActive(true);
    }
    private void Update()
    {
        if (state)
        {
            if (hasTimer)
            {
                countDown += Time.deltaTime;
                if (countDown <= timer)
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        GameObject projectile = Instantiate(bullet, ship.transform.position, Quaternion.identity);
                        projectile.transform.SetParent(ship.transform);
                    }
                }
                else if (countDown > timer)
                {
                    GetComponent<PuzzleManager>().fail.Play();
                    Reset();
                }
            }
            else if (!hasTimer)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    GameObject projectile = Instantiate(bullet, ship.transform.position, Quaternion.identity);
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
                GetComponent<PuzzleManager>().win.Play();
                Reset();
                FindObjectOfType<PuzzleManager>().Unlock();
            }
        }
        else
        {
            GetComponent<PuzzleManager>().fail.Play();
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
    }
}
