using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

[Serializable]
public class TriggerImage : MonoBehaviour
{
    //public delegate void OnImageChosen();
    //public static OnImageChosen choseImage;

    [SerializeField] string imgName;
    RectTransform imgTransform;

    private void Awake() {
        imgTransform = GetComponent<RectTransform>();
    }

    public void Select() {
        imgTransform.DOScale(1.2f, 0.5f);
    }

    public void Unselect() {
        imgTransform.DOScale(1f, 0.5f);
    }

    public void Choose() {
        Unselect();
        //choseImage();
        GameController.instance.SendMissionInput(imgName, GameController.instance.UpdateObjective());
        Debug.Log($"You've chosen {imgName}");
    }
}
