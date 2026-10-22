using UnityEngine;

public class Bamboo : MonoBehaviour
{
    public float angle;
    public float radius = 20f;
    public float offset;
    public float height = 1.4f;

    void Update()
    {
        float bob = Mathf.Sin(Time.time * 3f + angle) * 0.15f;
        transform.position = Quaternion.Euler(0f, angle, 0f) * new Vector3(0f, height + bob, radius + offset);
        transform.Rotate(0f, 120f * Time.deltaTime, 0f, Space.World);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        ScoreManager.Instance.AddBamboo();
        if (EnergyManager.Instance != null)
        {
            EnergyManager.Instance.AddEnergy(12.0f);
        }
        Destroy(gameObject);
    }
}