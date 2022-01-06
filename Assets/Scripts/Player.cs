using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //
    private bool goLeft = false;
    private bool goRight = false;
    private bool goUp = false;
    private bool goDown = false;
    //
    private float speed = 3f;
    //
    Vector3 right = Vector3.right;
    Vector3 left = Vector3.left;
    Vector3 up = Vector3.up;
    Vector3 down = Vector3.down;
    //
    [HideInInspector] public int currentScore = 0;
    private int oldScore = 0;

    //
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject loseCanvas;
    private bool tailIsGrowing = false;


    //
    private List<GameObject> snakeParts = new List<GameObject>();

    private void Awake()
    {
        Time.timeScale = 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        goRight = true;
        snakeParts.Add(this.gameObject);

        //
        Vector2 spawnPos = new Vector2(transform.position.x, transform.position.y);
        GameObject newSnakeParts = Instantiate(playerPrefab, spawnPos, quaternion.identity);
        snakeParts.Add(newSnakeParts);
        snakeParts[1].SetActive(false);

        loseCanvas.SetActive(false);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        IsMoving();
        GrowUp();
        for (int i = snakeParts.Count - 1; i > 0; i--)
        {
            snakeParts[i].transform.position = snakeParts[i - 1].transform.position;

        }
    }

    private void Update()
    {
        IsInput();
    }

    void IsMoving()
    {
        if (goDown == true)
        {
            Vector3 translation = down *speed* Time.deltaTime;
            transform.Translate(translation);
        }
        if (goUp == true)
        {
            Vector3 translation = up *speed* Time.deltaTime;
            transform.Translate(translation);
        }
        if (goLeft == true)
        {
            Vector3 translation = left *speed* Time.deltaTime;
            transform.Translate(translation);
        }
        if (goRight == true)
        {
            Vector3 translation = right *speed* Time.deltaTime;
            transform.Translate(translation);
        }
    }

    void IsInput()
    {
        if (tailIsGrowing = true)
        {
            if (goLeft == false)
            {
                if (Input.GetKeyDown(KeyCode.D))
                {
                    goRight = true;
                    goLeft = false;
                    goUp = false;
                    goDown = false;

                    Debug.Log("Going Right");
                }
            }

            if (goRight == false)
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    goRight = false;
                    goLeft = true;
                    goUp = false;
                    goDown = false;

                    Debug.Log("Going Left");

                }
            }

            if (goDown == false)
            {
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    goRight = false;
                    goLeft = false;
                    goUp = true;
                    goDown = false;

                    Debug.Log("Going Up");
                }
            }

            if (goUp == false)
            {
                if (Input.GetKeyDown(KeyCode.S))
                {
                    goRight = false;
                    goLeft = false;
                    goUp = false;
                    goDown = true;

                    Debug.Log("Going Down");
                }
            }
        }
        else
        {

            if (Input.GetKeyDown(KeyCode.D))
            {
                goRight = true;
                goLeft = false;
                goUp = false;
                goDown = false;

                Debug.Log("Going Right");
            }



            if (Input.GetKeyDown(KeyCode.Q))
            {
                goRight = false;
                goLeft = true;
                goUp = false;
                goDown = false;

                Debug.Log("Going Left");

            }



            if (Input.GetKeyDown(KeyCode.Z))
            {
                goRight = false;
                goLeft = false;
                goUp = true;
                goDown = false;

                Debug.Log("Going Up");
            }



            if (Input.GetKeyDown(KeyCode.S))
            {
                goRight = false;
                goLeft = false;
                goUp = false;
                goDown = true;

                Debug.Log("Going Down");
            }

        }
    }

    void GrowUp()
    {
        if (oldScore < currentScore)
        {
            Vector2 spawnPos = new Vector2(transform.position.x, transform.position.y);
            GameObject newSnakeParts = Instantiate(playerPrefab,spawnPos,quaternion.identity);
            snakeParts.Add(newSnakeParts);
            newSnakeParts.transform.position = snakeParts[snakeParts.Count - 1].transform.position;
            tailIsGrowing = true;
            
            oldScore = currentScore;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("wall"))
        {
            Debug.Log("Collision wall");
            Time.timeScale = 0;
            loseCanvas.SetActive(true);
        }

        if (collision.gameObject.CompareTag("tail"))
        {
            Debug.Log("Collision tail");
            Time.timeScale = 0;
            loseCanvas.SetActive(true);
        }
    }

    public void restart()
    {
        SceneManager.LoadScene("Main");
    }

    public void LeaveGame()
    {
        Application.Quit();
    }
}
