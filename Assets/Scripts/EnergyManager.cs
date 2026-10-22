using UnityEngine;
using UnityEngine.UI;

public class EnergyManager : MonoBehaviour
{
    public static EnergyManager Instance;

    public PandaRunner panda;
    public float maxEnergy = 100f;
    public float baseDrain = 4f;
    public float extraDrainPerLap = 0.5f;
    public bool paused;

    public Slider energyBar;
    public Image barFill;
    public Light sun;

    public float Energy { get; private set; }
    float sunStart = 1f;
    bool depleted;

    void Awake() { Instance = this; }

    void Start()
    {
        Energy = maxEnergy;
        if (sun != null) sunStart = sun.intensity;
        panda.onDied += ResetEnergy;
        UpdateVisuals();
    }

    void OnDestroy()
    {
        if (panda != null) panda.onDied -= ResetEnergy;
    }

    void Update()
    {
        if (depleted || paused) return;

        float drain = baseDrain + extraDrainPerLap * (panda.lap - 1);
        Energy -= drain * Time.deltaTime;

        if (Energy <= 0f)
        {
            Energy = 0f;
            depleted = true;
            panda.Die();
        }
        UpdateVisuals();
    }

    public void AddEnergy(float amount)
    {
        if (depleted) return;
        Energy = Mathf.Min(maxEnergy, Energy + amount);
    }

    void ResetEnergy()
    {
        Energy = maxEnergy;
        depleted = false;
        UpdateVisuals();
    }

    void UpdateVisuals()
    {
        float t = Energy / maxEnergy;
        if (energyBar != null) energyBar.value = t;
        if (barFill != null) barFill.color = Color.Lerp(Color.red, Color.green, t);
        if (sun != null) sun.intensity = Mathf.Lerp(sunStart * 0.3f, sunStart, t);
    }
}