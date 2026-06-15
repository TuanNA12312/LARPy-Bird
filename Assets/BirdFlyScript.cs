using UnityEngine;

public class BirdFlyScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public float flySpeed;
    public LogicScript logicScript;
    public bool isAlive = true;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        logicScript = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isAlive)
        {
            rb.linearVelocity = Vector2.up * flySpeed;
        }
        
    }
    public void gameOver()
    {
        logicScript.GameOver();
        isAlive = false;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        gameOver();
    }

}
