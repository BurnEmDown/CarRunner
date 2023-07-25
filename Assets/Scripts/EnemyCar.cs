using Managers;
using UnityEngine;
using UnityEngine.UI;
using EventType = Managers.Events.EventType;

public class EnemyCar : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

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
    }

    public void SetValues(Vector2 size, Color color, float speed, bool canSwitchLanes, int scoreGiven)
    {
        this.size = size;
        this.color = color;
        this.speed = speed;
        ScoreGiven = scoreGiven;
        canMoveToOtherLanes = canSwitchLanes;
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
}
