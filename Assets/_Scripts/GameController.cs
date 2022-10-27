using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour {
    public static GameController instance;

    public XRController xrController;
    [SerializeField] TMP_InputField loginName;
    [SerializeField] TMP_Text errorText;

    string customId;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    public void Start() {
        customId = PlayerPrefs.GetString("CustomID");

        if (!string.IsNullOrEmpty(customId)) {
            loginName.text = customId;
        }

        errorText.text = string.Empty;
    }

    public void Login() {
        
        if(string.IsNullOrEmpty(loginName.text)) {
            errorText.text = "Please enter a LoginID!";
            return;
        }

        customId = loginName.text;

        Debug.Log(customId);
        xrController.Auth(customId, OnAuth);
    }

    void OnAuth(JObject response) {
        //TODO: Load Inventory
        //TODO: Load Missions
        errorText.text = $"Welcome {customId}!";
        PlayerPrefs.SetString("CustomID", customId);
        Debug.Log(response["data"]["LoginResult"]["PlayFabId"].Value<string>());
    }


    string GenerateID() {
        string id = System.Guid.NewGuid().ToString();
        PlayerPrefs.SetString("CustomID", id.ToString());
        return id;
    }
}