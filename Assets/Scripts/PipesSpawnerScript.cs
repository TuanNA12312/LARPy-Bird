using UnityEngine;

public class PipesSpawnerScript : MonoBehaviour
{
    public GameObject pipePrefab;
    public float spawnInterval = 2f;
    private float timer;
    public float heightRange = 3f; // Điều chỉnh phạm vi chiều cao hợp lý (tránh pipe sinh ngoài màn hình)
    private LogicScript logicScript;

    void Start()
    {
        timer = spawnInterval;
        GameObject logicObj = GameObject.FindGameObjectWithTag("Logic");
        if (logicObj != null)
        {
            logicScript = logicObj.GetComponent<LogicScript>();
        }
    }

    void Update()
    {
        // Chỉ sinh pipe khi game đã bắt đầu và chưa Game Over
        if (logicScript != null && (!logicScript.isGameStarted || logicScript.isGameOver))
        {
            return;
        }

        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnPipe();
            timer = 0f;
        }
    }

    void SpawnPipe()
    {
        if (pipePrefab == null) return;

        float lowestPoint = transform.position.y - heightRange;
        float highestPoint = transform.position.y + heightRange;
        float spawnY = Random.Range(lowestPoint, highestPoint);

        // Giữ nguyên tọa độ Z của PipePrefab/Spawner
        Vector3 spawnPos = new Vector3(transform.position.x, spawnY, pipePrefab.transform.position.z);
        GameObject newPipe = Instantiate(pipePrefab, spawnPos, transform.rotation);

        // Đảm bảo SpriteRenderer của Pipe có Sorting Order nổi trên background
        SpriteRenderer[] renderers = newPipe.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sr in renderers)
        {
            if (sr.sortingOrder <= 0)
            {
                sr.sortingOrder = 1;
            }
        }
    }
}
