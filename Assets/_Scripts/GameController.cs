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
    [SerializeField] public TMP_Text welcomeText;

    [SerializeField] CanvasFader loginMenuFader;
    [SerializeField] CanvasFader mainMenuFader;
    [SerializeField] ImageSelector imgSelector;

    public List<Mission> missions = new List<Mission>();
    public List<Badge> badges = new List<Badge>();

    public string actualObj;
    int counter = 0;

    string customId;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }


    }

    void Start() {
        customId = PlayerPrefs.GetString("CustomID");

        if (!string.IsNullOrEmpty(customId)) {
            loginName.text = customId;
        }

        errorText.text = string.Empty;
        welcomeText.text = string.Empty;
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
        welcomeText.text = $"Welcome {customId}!";
        PlayerPrefs.SetString("CustomID", customId);
        Debug.Log(response["data"]["LoginResult"]["PlayFabId"].Value<string>());

        loginMenuFader.FadeOut(0f, 1f);
        mainMenuFader.MoveTo(0f, -40f, 0f, 0f, 0f, 1.5f);
        mainMenuFader.FadeIn(0f, 1.5f);
    }

    public void LoadMissions() {
        xrController.Client("GetMissionInventory", null, OnMissionsLoaded);
    }

    void OnMissionsLoaded(JObject response) {

        missions.Clear();
        
        foreach (JObject job in response["data"]["missions"]["PlayerMissions"]) {
            Mission mission = new Mission();

            mission.id = job["itemId"].Value<string>();
            mission.title = job["playfab"]["DisplayName"].Value<string>();
            
            foreach(JObject jobObj in job["objectives"]) {
                mission.objIds.Add(jobObj["id"].Value<string>());
            }
            missions.Add(mission);

            Debug.Log(mission.id + " " + mission.title);
        }
    }

    public void LoadInventory() {
        xrController.Client("GetItemInventory", null, OnInventoryLoaded);
    }

    void OnInventoryLoaded(JObject response) {
        badges.Clear();

        foreach(JObject job in response["data"]["items"]) {
            Badge badge = new Badge();

            badge.id = job["itemId"].Value<string>();
            badge.title = job["publicData"]["Title"].Value<string>();

            badges.Add(badge);

            Debug.Log(badge.id + " " + badge.title);
        }
    }

    public void SendMissionInput(string objInput, string objId) {
        Dictionary<string, string> param = new Dictionary<string, string>();
        param.Add("ItemId", "m-1666904646832-494");
        param.Add("Input", objInput);
        param.Add("ObjectiveId", objId);
        xrController.Client("SendMissionInput", param, OnMissionInputSent);
    }

    void OnMissionInputSent(JObject response) {
        LoadInventory();
        LoadMissions();
        imgSelector.RefreshImages();
    }

    public string UpdateObjective() {

        string objectiveId = string.Empty;

        foreach (Mission m in missions) {
            if (m.id == "m-1666904646832-494") {
                objectiveId = m.objIds[counter];
            }

        }

        counter++;

        Debug.Log(objectiveId);

        return objectiveId;
    }
}
