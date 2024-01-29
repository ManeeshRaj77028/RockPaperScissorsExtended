using UnityEngine;

public sealed class SimpleScaleAnimation : MonoBehaviour
{
    [SerializeField] private Vector3 fromScale, toScale;
    [SerializeField] private float duration;

    public void OnEnable()
    {
        LeanTween.scale(gameObject, toScale, duration).setEaseOutSine().setLoopPingPong();
    }

    private void OnDisable()
    {
        LeanTween.cancel(gameObject);
    }
}
