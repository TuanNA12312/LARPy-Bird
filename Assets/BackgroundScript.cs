using UnityEngine;

public class BackgroundScript : MonoBehaviour
{
    private float startPos, length;
    public float speed = 5f;
    public float parallaxEffect;
    private float distanceTraveled = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        distanceTraveled += speed * parallaxEffect * Time.deltaTime;
        transform.position = new Vector3(startPos - distanceTraveled, transform.position.y, transform.position.z);

        if (distanceTraveled >= length)
        {
            distanceTraveled = Mathf.Repeat(distanceTraveled, length);
        }
    }
}
