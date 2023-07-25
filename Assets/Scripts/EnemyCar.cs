using System;
using Managers;
using UnityEngine;
using UnityEngine.UI;
using EventType = Managers.Events.EventType;
using Random = UnityEngine.Random;

public class EnemyCar : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    
    [SerializeField] private float minTimeChangeLane = 3f;
    [SerializeField] private float maxTimeChangeLane = 6f;
    [SerializeField] private float nextTimeToChangeLane = 3f;
    
    private Transform[] possibleLanesPositions;
    private Transform laneParent;

    private int currentLane;
    private Vector2 downDirection;
    private float speed = 100f;
    private int scoreGiven = 100;

    private Vector2 size;
    private Color color;
    private bool canMoveToOtherLanes;
    private bool isInitialized = false;

    public int ScoreGiven
    {
        get => scoreGiven;
        private set => scoreGiven = value;
    }

    private void Awake()
    {
        downDirection = new Vector2(0, -transform.up.y);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + (downDirection * (speed * Time.deltaTime)));

        if (canMoveToOtherLanes)
        {
            if (nextTimeToChangeLane <= Time.time)
            {
                ChangeLane();
                nextTimeToChangeLane = Time.time + Random.Range(minTimeChangeLane, maxTimeChangeLane);
            }
        }
    }

    public void SetValues(Vector2 size, Color color, float speed, bool canSwitchLanes, int scoreGiven, int lane)
    {
        this.size = size;
        this.color = color;
        this.speed = speed;
        canMoveToOtherLanes = canSwitchLanes;
        ScoreGiven = scoreGiven;
        currentLane = lane;
        UpdateFromValues();
    }
    
    private void UpdateFromValues()
    {
        GetComponent<RectTransform>().sizeDelta = size;
        GetComponent<BoxCollider2D>().size = size;
        GetComponent<Image>().color = color;
        
        isInitialized = true;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        // There was an issue which I couldn't find a better fix for where the player would immediately trigger 
        // collisions with the enemy cars, which trigger game over immediately, so this way the enemy cars won't 
        // trigger collisions until they are properly initialized
        if (!isInitialized) return;
        
        
        if (col.CompareTag("BottomCollider"))
        {
            MainManager.Instance.eventsManager.InvokeEvent(EventType.EnemyCarPassed, this);
        }
        else if (col.CompareTag("Player"))
        {
            MainManager.Instance.eventsManager.InvokeEvent(EventType.EnemyCarHitPlayer, this);
        }
    }
    public void AddAllLocationFromLanesParentObject(GameObject parentObject)
    {
        int childrenAmount = parentObject.transform.childCount;
        possibleLanesPositions = new Transform[childrenAmount];
        for (int i = 0; i < childrenAmount; ++i)
            possibleLanesPositions[i] = parentObject.transform.GetChild(i);
    }
    
    
    private void ChangeLane()
    {
        if (currentLane == 0)
        {
            MoveRight();
        }
        else if (currentLane == possibleLanesPositions.Length - 1)
        {
            MoveLeft();
        }
        else
        {
            if (Random.Range(0f, 1f) > 0.5f)
            {
                MoveRight();
            }
            else
            {
                MoveLeft();
            }
        }
    }

    private void MoveRight()
    {
        currentLane++;
        if (currentLane > possibleLanesPositions.Length - 1)
        {
            currentLane = possibleLanesPositions.Length - 1;
            Debug.LogError("Tried to move too much to the right");
            return;
        }

        UpdateLanePosition();
    }
    
    private void MoveLeft()
    {
        currentLane--;
        if (currentLane < 0)
        {
            currentLane = 0;
            Debug.LogError("Tried to move too much to the left");
            return;
        }

        UpdateLanePosition();
    }

    private void UpdateLanePosition()
    {
        transform.SetParent(possibleLanesPositions[currentLane]);
        transform.localPosition = new Vector3(0, transform.localPosition.y, 0);
    }
}
