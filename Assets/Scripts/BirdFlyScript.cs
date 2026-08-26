using UnityEngine;

public class BirdFlyScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public float flySpeed = 5f;
    public LogicScript logicScript;
    public bool isAlive = true;

    private float initialGravityScale = 1f;

    void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }

        if (rb != null)
        {
            initialGravityScale = rb.gravityScale > 0 ? rb.gravityScale : 1f;
        }

        if (logicScript == null)
        {
            GameObject logicObj = GameObject.FindGameObjectWithTag("Logic");
            if (logicObj != null)
            {
                logicScript = logicObj.GetComponent<LogicScript>();
            }
        }

        // Nếu game chưa bắt đầu thì tạm thời khóa trọng lực. Nếu đã bắt đầu (khi chơi lại) thì mở trọng lực
        if (logicScript != null && !logicScript.isGameStarted)
        {
            if (rb != null) rb.gravityScale = 0f;
        }
        else
        {
            if (rb != null) rb.gravityScale = initialGravityScale;
        }
    }

    void Update()
    {
        if (logicScript != null)
        {
            // Bắt đầu game bằng phím Space (cho lần đầu mở game)
            if (!logicScript.isGameStarted && Input.GetKeyDown(KeyCode.Space))
            {
                logicScript.StartGame();
                if (rb != null)
                {
                    rb.gravityScale = initialGravityScale;
                    rb.linearVelocity = Vector2.up * flySpeed;
                }
                return;
            }

            // Bay khi game đang diễn ra
            if (logicScript.isGameStarted && isAlive && Input.GetKeyDown(KeyCode.Space))
            {
                if (rb != null)
                {
                    rb.gravityScale = initialGravityScale;
                    rb.linearVelocity = Vector2.up * flySpeed;
                }
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) && isAlive)
            {
                if (rb != null) rb.linearVelocity = Vector2.up * flySpeed;
            }
        }
    }

    public void gameOver()
    {
        isAlive = false;
        if (logicScript != null)
        {
            logicScript.GameOver();
        }
        else
        {
            BackgroundScript.StopAllBackgrounds();
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        gameOver();
        if (rb != null) rb.gravityScale = 0f;
        gameObject.SetActive(false); // Player biến mất khi va chạm
    }
}
