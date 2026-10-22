using UnityEngine;

public class TrackBuilder : MonoBehaviour
{
    public GameObject segmentPrefab;
    public int segmentCount = 36;
    public float radius = 20f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < segmentCount; i++)
        {
            float angle = i * (360f / segmentCount);
            Quaternion rot = Quaternion.Euler(0f, angle, 0f);
            Vector3 pos = rot * new Vector3(0f, 0f, radius);
            Instantiate(segmentPrefab, pos, rot, transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
