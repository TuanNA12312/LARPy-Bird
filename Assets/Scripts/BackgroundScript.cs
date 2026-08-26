using UnityEngine;

public class BackgroundScript : MonoBehaviour
{
    private float startPos, length;
    public float speed = 5f;
    public float parallaxEffect = 1f;
    private float distanceTraveled = 0f;
    public bool isStopped = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position.x;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            length = sr.bounds.size.x;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isStopped) return;

        distanceTraveled += speed * parallaxEffect * Time.deltaTime;
        transform.position = new Vector3(startPos - distanceTraveled, transform.position.y, transform.position.z);

        if (length > 0 && distanceTraveled >= length)
        {
            distanceTraveled = Mathf.Repeat(distanceTraveled, length);
        }
    }

    public void StopBackground()
    {
        isStopped = true;
        speed = 0f;
    }

    public static void StopAllBackgrounds()
    {
        BackgroundScript[] backgrounds = Object.FindObjectsOfType<BackgroundScript>();
        foreach (BackgroundScript bg in backgrounds)
        {
            bg.StopBackground();
        }
    }
}
