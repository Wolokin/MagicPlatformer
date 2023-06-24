using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryRoyaleOverlayGradient : MonoBehaviour
{
    private float spawnTime = 0.0f;

    public float gradientTime;
    public float maxAlpha;

    private void SetAlphaRec(float alpha)
    {
        // Loop through each transform and update the alpha of its canvas renderer.
        foreach (Transform t in GetComponentsInChildren<Transform>())
        {
            if (t.TryGetComponent<CanvasRenderer>(out var renderer))
            {
                renderer.SetAlpha(alpha);
            }
        }
    }
    void Start()
    {
        SetAlphaRec(0.0f);
        spawnTime = Time.time;
    }

    void Update()
    {
        float alpha = Mathf.Min(maxAlpha, (Time.time - spawnTime) / gradientTime);
        SetAlphaRec(alpha);
    }
}
