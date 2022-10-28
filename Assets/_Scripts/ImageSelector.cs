using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ImageSelector : MonoBehaviour
{
    public List<GameObject> triggerImages = new List<GameObject>();
    [SerializeField] List<GameObject> chosenImages = new List<GameObject>();
    float initialXPos = 0f;

    private void Start() {
        TriggerImage.choseImage += ClearImages;
        TriggerImage.choseImage += RefreshImages;
    }

    public void ShowImageSet() {
        float imageOffset = 300f;

        chosenImages = ChooseThreeImages();

        foreach(GameObject image in chosenImages) {
            Debug.Log("Spawning Image");
            Instantiate(image, new Vector3(transform.parent.position.x + initialXPos, transform.position.y, transform.position.z), transform.rotation, gameObject.transform);
            initialXPos += imageOffset;
        }

        initialXPos = 0f;
    }

    List<GameObject> ChooseThreeImages() {
        int imgNum = 0;
        

        while (imgNum < 3) {
            int randIndex = Random.Range(0, triggerImages.Count);
            chosenImages.Add(triggerImages[randIndex]);
            triggerImages.RemoveAt(randIndex);
            imgNum++;
        }

        return chosenImages;
    }

    public void ClearImages() {
        chosenImages.Clear();
    }

    void DestroyInstantiatedChildren() {
        foreach(Transform child in gameObject.transform) {
            Destroy(child.gameObject);
        }
    }

    void RefreshImages() {
        DestroyInstantiatedChildren();
        ShowImageSet();
    }
}