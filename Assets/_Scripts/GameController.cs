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

    [SerializeField] CanvasFader loginMenuFader;

    public List<Mission> missions = new List<Mission>();
    public List<Badge> badges = new List<Badge>();

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
        LoadInventory();
        LoadMissions();
        errorText.text = $"Welcome {customId}!";
        PlayerPrefs.SetString("CustomID", customId);
        Debug.Log(response["data"]["LoginResult"]["PlayFabId"].Value<string>());
    }

    void LoadMissions() {
        xrController.Client("GetMissionInventory", null, OnMissionsLoaded);
    }

    void OnMissionsLoaded(JObject response) {
        
        foreach (JObject job in response["data"]["missions"]["PlayerMissions"]) {
            Mission mission = new Mission();

            mission.id = job["itemId"].Value<string>();
            mission.title = job["playfab"]["DisplayName"].Value<string>();

            missions.Add(mission);

            Debug.Log(mission.id + " " + mission.title);
        }
    }

    void LoadInventory() {
        xrController.Client("GetItemInventory", null, OnInventoryLoaded);
    }

    void OnInventoryLoaded(JObject response) {
        foreach(JObject job in response["data"]["items"]) {
            Badge badge = new Badge();

            badge.id = job["itemId"].Value<string>();
            badge.title = job["publicData"]["Title"].Value<string>();

            badges.Add(badge);

            Debug.Log(badge.id + " " + badge.title);
        }
    }
}
