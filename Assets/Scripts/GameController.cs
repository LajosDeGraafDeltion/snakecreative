using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public GameObject snakePref;
    public Snake head;
    public Snake tail;
    public int direction;
    public Vector2 nextPos;

    void Start()
    {
        InvokeRepeating("TimerInvoke", 0, 0.5f);
    }


    void Update()
    {
        ComChangeD();

    }

    void TimerInvoke()
    {
        Movement();
    }

    void Movement()
    {
        GameObject temp;
        nextPos = head.transform.position;
        switch (direction)
        {
            case 0:
                nextPos = new Vector2(nextPos.x, nextPos.y + 1);
                break;
            case 1:
                nextPos = new Vector2(nextPos.x + 1, nextPos.y );
                break;
            case 2:
                nextPos = new Vector2(nextPos.x, nextPos.y - 1);
                break;
            case 3:
                nextPos = new Vector2(nextPos.x - 1, nextPos.y);
                break;
        }
        temp = (GameObject)Instantiate(snakePref, nextPos, transform.rotation);
        head.Setnext(temp.GetComponent<Snake>());
        head = temp.GetComponent<Snake>();

        return;
    }

    void ComChangeD()
    {
        if (direction != 2 && Input.GetButtonDown("Up"))
        {
            direction = 0;
        }

        if (direction != 3 && Input.GetButtonDown("Right"))
        {
            direction = 1;
        }

        if (direction != 0 && Input.GetButtonDown("Down"))
        {
            direction = 2;
        }

        if (direction != 1 && Input.GetButtonDown("Left"))
        {
            direction = 3;
        }
    }
}
