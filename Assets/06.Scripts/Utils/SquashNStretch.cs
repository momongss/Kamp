using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class SquashNStretch : MonoBehaviour
{
    public Vector3 originScale;

    public float speed = 2.0f;

    Sequence sequence;

    private void Start()
    {
        originScale = transform.localScale;

        Squash_N_Stretch();
    }

    public void UI_Scaling_Show()
    {
        transform.localScale = new Vector3(
            originScale.x * 0.05f,
            originScale.y * 0.05f,
            originScale.z * 0.05f
            ); ;

        transform
            .DOScale(originScale, 0.25f)
            .SetEase(Ease.OutBounce);
    }

    public void UI_Scaling_Hide(UnityAction callback = null)
    {
        transform
            .DOScale(originScale * 0.6f, 0.25f)
            .SetEase(Ease.InOutBounce)
            .OnComplete(() =>
            {
                transform
                    .DOScale(Vector3.zero, 0.25f)
                    .OnComplete(() =>
                    {
                        if (callback != null) callback();
                    });
            });
    }

    public void Squash_N_Stretch(float scaling_x = 1.4f, float scaling_y = 0.5f, float scaling_z = 1.4f)
    {
        if (sequence != null) sequence.Kill();

        Vector3 squash_scale = new Vector3(
            originScale.x * scaling_x,
            originScale.y * scaling_y,
            originScale.z * scaling_z
            );

        sequence = DOTween.Sequence();
        sequence.Append(transform
            .DOScale(squash_scale, 0.3f)
            .SetEase(Ease.OutBounce)
            .OnComplete(() =>
            {
                Vector3 stretch_scale = new Vector3(
                    originScale.x * 0.9f,
                    originScale.y * 1.2f,
                    originScale.z * 0.9f
                    );

                transform
                    .DOScale(stretch_scale, 0.1f)
                    .OnComplete(() =>
                    {
                        transform
                            .DOScale(originScale, 0.1f)
                            .SetEase(Ease.OutBounce);
                    });
                sequence = null;
            }));
    }

    public void Squash_N_Stretch(TweenCallback callback)
    {
        if (sequence != null)
        {
            sequence.Kill();
        }

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
                sequence = null;
            }));
    }

    public void Stretch()
    {
        if (sequence != null) sequence.Kill();

        sequence = DOTween.Sequence();
        sequence.Append(transform
            .DOScale(originScale, 0.2f)
            .SetEase(Ease.OutBounce)
            .OnComplete(() =>
            {
                sequence = null;
            }));
    }
}
