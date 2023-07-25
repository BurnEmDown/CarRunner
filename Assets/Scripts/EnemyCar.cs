using Managers;
using UnityEngine;
using EventType = Managers.Events.EventType;

public class EnemyCar : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    private Vector2 downDirection;
    private float speed = 100f;
    private int scoreGiven = 100;

    public int ScoreGiven => scoreGiven;

    private void Awake()
    {
        downDirection = new Vector2(0, -transform.up.y);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + (downDirection * (speed * Time.deltaTime)));
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
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
