using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SquashNStretch : MonoBehaviour
{
    public Vector3 originScale;

    public float speed = 2.0f;

    Sequence sequence;

    private void Start()
    {
        originScale = transform.localScale;
    }

    public void Squash_N_Stretch()
    {
        if (sequence != null) sequence.Kill();

        Vector3 target_scale = new Vector3(
            originScale.x * 1.2f,
            originScale.y * 0.5f,
            originScale.z * 1.2f
            );

        sequence = DOTween.Sequence();
        sequence.Append(transform
            .DOScale(target_scale, 0.3f)
            .SetEase(Ease.OutBounce)
            .OnComplete(() =>
            {
                transform
                    .DOScale(originScale, 0.2f)
                    .SetEase(Ease.OutBounce);
                sequence = null;
            }));
    }

    public void Squash_N_Stretch(TweenCallback callback)
    {
        if (sequence != null) sequence.Kill();

        Vector3 target_scale = new Vector3(
            originScale.x * 1.2f,
            originScale.y * 0.5f,
            originScale.z * 1.2f
            );

        sequence = DOTween.Sequence();
        sequence.Append(transform
            .DOScale(target_scale, 0.3f)
            .SetEase(Ease.OutBounce)
            .OnComplete(() =>
            {
                transform
                    .DOScale(originScale, 0.2f)
                    .SetEase(Ease.OutBounce)
                    .OnComplete(() =>
                    {
                        sequence = null;

                        callback();
                    });
            }));
    }

    public void Squash()
    {
        transform
            .DOScale(originScale / 2, 0.5f)
            .SetEase(Ease.OutBounce);
    }

    public void RestoreOrigin()
    {
        transform
            .DOScale(originScale, 0.5f)
            .SetEase(Ease.OutBounce);
    }
}
