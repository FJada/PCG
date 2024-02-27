using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    public Transform[] startingPositions;
    public GameObject[] rooms; // index 0 --> LR, index 1 --> LRB, index 2 --> LRT, index 3 --> LRTB
    public GameObject boss;
    public float moveAmt;

    private int direction;

    private float timeBtwRoom;
    public float startTimeBtwRoom = 0.25f;
    public bool stopGeneration = false;

    private int downCounter;

    public LayerMask room;

    public float minX;
    public float maxX;
    public float minY;


    // Start is called before the first frame update
    void Start()
    {
        int randStartingPos = Random.Range(0, startingPositions.Length);
        transform.position = startingPositions[randStartingPos].position;
        Instantiate(rooms[0], transform.position, Quaternion.identity);
        direction = Random.Range(1, 6);
    }

    // Update is called once per frame
    void Update()
    {
        if(timeBtwRoom <= 0 && stopGeneration == false)
        {
            Move();
            timeBtwRoom = startTimeBtwRoom;
        } else
        {
            timeBtwRoom -= Time.deltaTime;
        }
    }

    private void Move()
    {
        
        if (direction == 1 || direction == 2) // move right
        {
            downCounter = 0;
            if (transform.position.x < maxX)
            {
                Vector2 newPos = new Vector2(transform.position.x + moveAmt, transform.position.y);
                transform.position = newPos;

                int rand = Random.Range(0, rooms.Length);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

                direction = Random.Range(1, 6);
                if(direction == 3)
                {
                    direction = 2;
                } else if(direction == 4)
                {
                    direction = 5;
                }
            } else
            {
                direction = 5;
            }
        } else if (direction == 3 || direction == 4) // move left
        {
            downCounter = 0;
            if (transform.position.x > minX)
            {
                Vector2 newPos = new Vector2(transform.position.x - moveAmt, transform.position.y);
                transform.position = newPos;

                int rand = Random.Range(0, rooms.Length);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

                direction = Random.Range(3, 6);
            } else
            {
                direction = 5;
            }
        } else if (direction == 5) // move down
        {
            downCounter++;
            if(transform.position.y > minY)
            {
                Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);
                if(roomDetection.GetComponent<RoomType>().type != 1 && roomDetection.GetComponent<RoomType>().type!=3)
                {
                    if (downCounter >= 2)
                    {
                        roomDetection.GetComponent<RoomType>().RoomDestruction();
                        Instantiate(rooms[3], transform.position, Quaternion.identity);
                    } else
                    {
                        roomDetection.GetComponent<RoomType>().RoomDestruction();

                        int randBottomRoom = Random.Range(1, 4);
                        if( randBottomRoom == 2)
                        {
                            randBottomRoom = 1;
                        }
                        Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);
                    }
                }

                Vector2 newPos = new Vector2(transform.position.x, transform.position.y - moveAmt);
                transform.position = newPos;

                int rand = Random.Range(2,4);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

                direction = Random.Range(1, 6);
            } else
            {
                Instantiate(boss, transform.position, Quaternion.identity);
                // stop level generation
                stopGeneration = true;
            }
        }
    }

}
