using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEditor.TextCore.Text;

public class CanvasFader : MonoBehaviour
{
    [SerializeField] CanvasGroup grp;
    public float speed = 0.2f;
    public bool isInteractive;
    public bool isVisibleOnStart;

    private void Awake() {
        grp = GetComponent<CanvasGroup>();

        if(!isVisibleOnStart) {
            Hide();
        }

        grp.interactable = isInteractive;
        grp.blocksRaycasts = isInteractive;
    }

    public void FadeIn(float delay = 0f, float speedOverride = 0f) {
        DOTween.Kill(grp);

        grp.DOFade(1f, speedOverride > 0f ? speedOverride : speed).SetDelay(delay);

        grp.interactable = isInteractive;
        grp.blocksRaycasts = isInteractive;
    }

    public void FadeOut(float delay = 0f, float speedOverride = 0f) {
        DOTween.Kill(grp);

        grp.DOFade(0f, speedOverride > 0f ? speedOverride : speed).SetDelay(delay);

        grp.interactable = isInteractive;
        grp.blocksRaycasts = isInteractive;
    }

    public void FadeTo(float alpha, float delay = 0f, float speedOverride = 0f) {
        DOTween.Kill(grp);

        grp.DOFade(alpha, speedOverride > 0f ? speedOverride : speed).SetDelay(delay);

        grp.interactable = isInteractive;
        grp.blocksRaycasts = isInteractive;
    }

    public void Show() {
        DOTween.Kill(grp);

        grp.alpha = 1f;

        grp.interactable = isInteractive;
        grp.blocksRaycasts = isInteractive;
    }

    public void Hide() {
        DOTween.Kill(grp);

        grp.alpha = 0f;

        grp.interactable = false;
        grp.blocksRaycasts = false;
    }
}
