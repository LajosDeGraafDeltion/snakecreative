﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public int maxSize;
    public int currentSize;
    public int xBound;
    public int yBound;
    public int score;
    public GameObject foodPref;
    public GameObject currentFood;
    public GameObject snakePref;
    public Snake head;
    public Snake tail;
    public int direction;
    public Vector2 nextPos;
    public Text scoreText;

    private void OnEnable()
    {
        Snake.hit += hit;
    }

    void Start()
    {
        InvokeRepeating("TimerInvoke", 0, 0.5f);
        FoodFunction();
    }

    private void OnDisable()
    {
        Snake.hit -= hit;
    }


    void Update()
    {
        ComChangeD();

    }

    void TimerInvoke()
    {
        Movement();
        if (currentSize >= maxSize)
        {
            TailFunction();
        }
        else
        {
            currentSize++;
        }
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

    void TailFunction()
    {
        Snake tempSnake = tail;
        tail = tail.GetNext();
        tempSnake.RemoveTail();
    }

    void FoodFunction()
    {
        int xPos = Random.Range(-xBound, xBound);
        int yPos = Random.Range(-yBound, yBound);

        currentFood = (GameObject)Instantiate(foodPref, new Vector2(xPos, yPos), transform.rotation);
        StartCoroutine(CheckRender(currentFood));
    }

    IEnumerator CheckRender(GameObject IN)
    {
        yield return new WaitForEndOfFrame();
        if (IN.GetComponent<Renderer>().isVisible == false)
        {
            if(IN.tag == "Food")
            {
                Destroy(IN);
                FoodFunction();
            }
        }
    }

    void hit(string WhatWasSent)
    {
        if (WhatWasSent == "Snake")
        {
            CancelInvoke("TimerInvoke");
            Exit();
        }

        if (WhatWasSent == "Food")
        {
            FoodFunction();
            maxSize++;
            score++;
            scoreText.text = score.ToString();
            int temp = PlayerPrefs.GetInt("HighScore");
            if (score > temp)
            {
                PlayerPrefs.SetInt("HighScore", score);
            }
        }
    }

    public void Exit()
    {
        SceneManager.LoadScene(0);
    }
}
