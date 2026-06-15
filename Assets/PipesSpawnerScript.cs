using UnityEngine;

public class PipesSpawnerScript : MonoBehaviour
{
    public GameObject pipePrefab;
    public float spawnInterval = 2f;
    private float timer;
    public float heightRange = 10f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = 2;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnPipe();
            timer = 0f;
        }
    }

    void SpawnPipe()
    {
        float lowestPoint = transform.position.y - heightRange;
        float highestPoint = transform.position.y + heightRange;

        Instantiate(pipePrefab, new Vector3(transform.position.x, 
            Random.Range(lowestPoint, highestPoint), 0), transform.rotation);
    }
}
