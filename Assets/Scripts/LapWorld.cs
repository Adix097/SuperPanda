using UnityEngine;

public class LapWorld : MonoBehaviour
{
    public PandaRunner panda;
    public Transform track;
    public Light sun;

    [System.Serializable]
    public class LapTheme
    {
        public string name = "Theme";
        public Color trackColor = Color.green;
        public Color skyColor = Color.cyan;
        public Color sunColor = Color.white;
    }

    public LapTheme[] themes;

    int currentLap = 0;
    public float startSpeed = 8f;
    public float acceleration = 0.1f;
    public float maxSpeed = 25f;
    float runTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ApplyTheme(1);
        panda.onDied += () => runTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        runTime += Time.deltaTime;
        panda.runSpeed = Mathf.Min(startSpeed + acceleration * runTime, maxSpeed);
        if (panda.lap != currentLap) ApplyTheme(panda.lap);
    }

    void ApplyTheme(int lap)
    {
        currentLap = lap;
        if (themes == null || themes.Length == 0) return;

        // Laps beyond the list reuse the last theme, speed keeps growing
        int index = (lap - 1) % themes.Length;
        LapTheme t = themes[index];

        foreach (Renderer r in track.GetComponentsInChildren<Renderer>()) {
            r.material.color = t.trackColor;
        }

        Camera.main.clearFlags = CameraClearFlags.SolidColor;
        Camera.main.backgroundColor = t.skyColor;
        RenderSettings.ambientLight = t.skyColor * 0.6f;

        if (sun != null) sun.color = t.sunColor;

        Debug.Log("Lap " + lap + ": " + t.name);
    }
}
