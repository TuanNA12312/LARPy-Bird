using UnityEngine;

public class PipeScript : MonoBehaviour
{
    public float speed = 5f;
    public float deadZone = -24;
    private LogicScript logicScript;

    void Start()
    {
        GameObject logicObj = GameObject.FindGameObjectWithTag("Logic");
        if (logicObj != null)
        {
            logicScript = logicObj.GetComponent<LogicScript>();
        }

        // Tự động kiểm tra và đảm bảo Pipe hiển thị phía trước Background
        SpriteRenderer[] renderers = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sr in renderers)
        {
            if (sr.sortingOrder <= 0)
            {
                sr.sortingOrder = 1;
            }
        }
    }

    void Update()
    {
        if (logicScript != null && (!logicScript.isGameStarted || logicScript.isGameOver)) return;

        transform.position += Vector3.left * speed * Time.deltaTime;
        if (transform.position.x < deadZone)
        {
            Destroy(gameObject);
        }
    }
}
