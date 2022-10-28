using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ResultImage : MonoBehaviour
{
    public List<ImageDetails> resultImages = new List<ImageDetails>();

    private void Start() {
        gameObject.SetActive(false);
    }

    public void SetResultImage() {
        foreach(ImageDetails img in resultImages) {
            if(img.title == GameController.instance.resultImageName) {
                GetComponent<Image>().sprite = img.image;
            }
        }

        gameObject.SetActive(true);
    }
}

[Serializable]
public class ImageDetails {
    public string title;
    public Sprite image;
}
