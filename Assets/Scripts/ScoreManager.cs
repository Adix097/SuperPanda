using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public PandaRunner panda;
    public int pointsPerBamboo = 10;
    public float bonus = 1f; // the double-bamboo power-up will change this later

    public int score;
    public int bamboo;
    public int best;

    public int Multiplier { get { return panda.lap; } }

    void Awake()
    {
        Instance = this;
        best = PlayerPrefs.GetInt("best", 0);
    }

    void Start()
    {
        panda.onDied += OnDied;
    }

    public void AddBamboo()
    {
        bamboo++;
        score += Mathf.RoundToInt(pointsPerBamboo * Multiplier * bonus);
        if (score > best) best = score;
    }

    void OnDied()
    {
        PlayerPrefs.SetInt("best", best);
        PlayerPrefs.Save();
        score = 0;
        bamboo = 0;
    }
}