using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] CanvasFader mainMenuButtons;
    [SerializeField] CanvasFader imageSelectionMenu;
    [SerializeField] ImageSelector imageSelector;

    public void ShowImageSelection() {
        mainMenuButtons.FadeOut(0f, 1f);
        imageSelectionMenu.FadeIn(0f, 1f);

        imageSelector.ShowImageSet();
        GameController.instance.welcomeText.text = "Choose a first image.";
    }

    public void ShowMainMenu() {
        mainMenuButtons.FadeIn(0f, 1f);
        imageSelectionMenu.FadeOut(0f, 1f);
    }
}
