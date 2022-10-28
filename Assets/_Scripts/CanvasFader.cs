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
    public bool isVisibleOnStart;

    private void Awake() {
        grp = GetComponent<CanvasGroup>();

        if(!isVisibleOnStart) {
            Hide();

            grp.interactable = false;
            grp.blocksRaycasts = false;
        }        
    }

    public void FadeIn(float delay = 0f, float speedOverride = 0f) {
        DOTween.Kill(grp);

        grp.DOFade(1f, speedOverride > 0f ? speedOverride : speed).SetDelay(delay);

        grp.interactable = true;
        grp.blocksRaycasts = true;
    }

    public void FadeOut(float delay = 0f, float speedOverride = 0f) {
        DOTween.Kill(grp);

        grp.DOFade(0f, speedOverride > 0f ? speedOverride : speed).SetDelay(delay);

        grp.interactable = false;
        grp.blocksRaycasts = false;
    }

    public void FadeTo(float alpha, float delay = 0f, float speedOverride = 0f) {
        DOTween.Kill(grp);

        grp.DOFade(alpha, speedOverride > 0f ? speedOverride : speed).SetDelay(delay);

        if (alpha > 0f) {
            grp.interactable = true;
            grp.blocksRaycasts = true;
        } else {

            grp.interactable = false;
            grp.blocksRaycasts = false;
        }
    }

    public void Show() {
        DOTween.Kill(grp);

        grp.alpha = 1f;

        grp.interactable = true;
        grp.blocksRaycasts = true;
    }

    public void Hide() {
        DOTween.Kill(grp);

        grp.alpha = 0f;

        grp.interactable = false;
        grp.blocksRaycasts = false;
    }

    public void MoveTo(float startX, float startY, float endX, float endY, float delay = 0f, float speedOverride = 0f) {
        DOTween.Kill(grp);

        Vector3 startPos = new Vector3(startX, startY);
        Vector3 endPos = new Vector3(endX, endY);

        gameObject.GetComponent<RectTransform>().localPosition = startPos;

        gameObject.GetComponent<RectTransform>().DOLocalMove(endPos, speedOverride > 0f ? speedOverride : speed).SetDelay(delay);
    }
}
