using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float angle;
    public float radius = 20f;
    public float height = 0.65f;
    public bool moving;
    public float range = 2f;
    public float speed = 2f;

    float phase;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        phase = Random.value * 6.28f;
    }

    // Update is called once per frame
    void Update()
    {
        float off = moving ? Mathf.Sin(Time.time * speed + phase) * range : 0f;
        transform.position = Quaternion.Euler(0f, angle, 0f) * new Vector3(0f, height, radius + off);
        transform.rotation = Quaternion.Euler(0f, angle + 90f, 0f);
    }
}