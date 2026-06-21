using TMPro;
using UnityEngine;

public class StatsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI fpsText;
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private TextMeshProUGUI lifeTimeText;
    [SerializeField] private Spawner spawner;

    private float timer;

    private void Update()
    {
        timer += Time.unscaledDeltaTime;

        if (timer < 0.5f)
            return;

        timer = 0f;

        float fps = 1f / Time.unscaledDeltaTime;

        fpsText.text = $"FPS: {fps:F0}";
        countText.text = $"Objects: {spawner.Count}";
        lifeTimeText.text = $"LifeTime: {spawner.LifeTime:F1}";
    }
}