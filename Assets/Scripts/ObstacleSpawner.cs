using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public PandaRunner panda;
    public float radius = 20f;
    public float safeStartAngle = 40f;

    readonly List<GameObject> spawned = new();
    int currentLap = 0;

    void Start()
    {
        panda.onDied += () => Spawn(panda.lap);
    }

    void Update()
    {
        if (panda.lap != currentLap) Spawn(panda.lap);
    }

    void Spawn(int lap)
    {
        currentLap = lap;
        foreach (var o in spawned) Destroy(o);
        spawned.Clear();

        int count = Mathf.Min(10 + lap, 50);
        float usable = 360f - safeStartAngle - 15f;
        float step = usable / count;

        for (int i = 0; i < count; i++)
        {
            float angle = safeStartAngle + i * step + Random.Range(0f, step * 0.3f);

            // Lap 1-2: logs only. Lap 3+: rocks too. Lap 4+: some move.
            bool rock = lap >= 3 && Random.value < 0.5f;
            bool moving = lap >= 4 && Random.value < 0.5f;
            spawned.Add(rock ? MakeRock(angle, moving) : MakeLog(angle, moving));
            SpawnBambooGroup(angle + step * 0.5f);
        }
    }

    GameObject MakeLog(float angle, bool moving)
    {
        var g = GameObject.CreatePrimitive(PrimitiveType.Cube);
        g.name = "Log";
        g.transform.localScale = new Vector3(5f, 0.8f, 1f);
        return Setup(g, angle, moving, 0.65f, new Color(0.45f, 0.28f, 0.1f));
    }

    GameObject MakeRock(float angle, bool moving)
    {
        var g = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        g.name = "Rock";
        g.transform.localScale = new Vector3(1.6f, 1.6f, 1.6f); // narrow: strafe or jump
        return Setup(g, angle, moving, 1.0f, Color.gray);
    }

    void SpawnBambooGroup(float centerAngle)
    {
        float offset = Random.Range(-2f, 2f);
        for (int k = -1; k <= 1; k++)
        {
            var g = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            g.name = "Bamboo";
            g.transform.localScale = new Vector3(0.5f, 0.6f, 0.5f);
            g.GetComponent<Collider>().isTrigger = true;
            g.GetComponent<Renderer>().material.color = new Color(0.4f, 0.8f, 0.25f);

            var b = g.AddComponent<Bamboo>();
            b.angle = centerAngle + k * 4f;
            b.radius = radius;
            b.offset = offset;
            spawned.Add(g);
        }
    }

    GameObject Setup(GameObject g, float angle, bool moving, float height, Color c)
    {
        g.GetComponent<Collider>().isTrigger = true;
        g.GetComponent<Renderer>().material.color = c;
        var ob = g.AddComponent<Obstacle>();
        ob.angle = angle;
        ob.radius = radius;
        ob.height = height;
        ob.moving = moving;
        return g;
    }
}