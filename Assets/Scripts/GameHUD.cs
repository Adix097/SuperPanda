using UnityEngine;
using UnityEngine.UI;

public class GameHUD : MonoBehaviour
{
    public PandaRunner panda;
    public ScoreManager score;

    Text lapText, scoreText;

    void Start()
    {
        var canvasGO = new GameObject("HUD Canvas");
        var canvas = canvasGO.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        var scaler = canvasGO.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920f, 1080f);

        lapText = MakeText("LapText", canvasGO.transform, TextAnchor.UpperLeft, new Vector2(0f, 1f));
        scoreText = MakeText("ScoreText", canvasGO.transform, TextAnchor.UpperRight, new Vector2(1f, 1f));
    }

    Text MakeText(string name, Transform parent, TextAnchor align, Vector2 anchor)
    {
        var go = new GameObject(name);
        go.transform.SetParent(parent, false);

        var t = go.AddComponent<Text>();
        t.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        t.fontSize = 48;
        t.fontStyle = FontStyle.Bold;
        t.color = Color.white;
        t.alignment = align;
        go.AddComponent<Outline>();

        var rt = t.rectTransform;
        rt.anchorMin = anchor;
        rt.anchorMax = anchor;
        rt.pivot = anchor;
        rt.sizeDelta = new Vector2(700f, 220f);
        rt.anchoredPosition = new Vector2(anchor.x < 0.5f ? 40f : -40f, -30f);
        return t;
    }

    void Update()
    {
        lapText.text = "LAP " + panda.lap + "\nx" + score.Multiplier + " multiplier";
        scoreText.text = "SCORE " + score.score + "\nBAMBOO " + score.bamboo + "\nBEST " + score.best;
    }
}