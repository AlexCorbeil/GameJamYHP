using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;

public delegate void ApiResponse(JObject response);

public class XRController : MonoBehaviour
{
    [SerializeField] public const string appId = "0uilg8";
    [SerializeField] public const string apiUrl = "https://xr.wonderlab.cloud";
    [SerializeField] string sessionTicket;
    [SerializeField] string customId;

    public void Auth(string customId, ApiResponse OnSuccess) {
        Dictionary<string, string> data = new Dictionary<string, string>();
        data.Add("CustomId", customId);

        CallStack("auth", "LoginWithCustomID", data, OnSuccess);
    }

    public void Client(string endpoint, Dictionary<string, string> data, ApiResponse OnSuccess) {
        CallStack("client", endpoint, data, OnSuccess);
    }

    public void Server(string endpoint, Dictionary<string, string> data, ApiResponse OnSuccess) {
        CallStack("server", endpoint, data, OnSuccess);
    }

    void CallStack(string api, string endpoint, Dictionary<string, string> data, ApiResponse OnSuccess) {
        StartCoroutine(Call(api, endpoint, data, OnSuccess));
    }

    IEnumerator Call(string api, string endpoint, Dictionary<string, string> data, ApiResponse OnSuccess) {
        WWWForm form = new WWWForm();

        if (data != null) {
            foreach (KeyValuePair<string, string> kvp in data) {
                form.AddField(kvp.Key, kvp.Value);
            }
        }

        using (UnityWebRequest www = UnityWebRequest.Post(apiUrl + "/" + api + "/" + endpoint, form)) {
            www.SetRequestHeader("X-App-Id", appId);

            if(!string.IsNullOrEmpty(sessionTicket)) {
                www.SetRequestHeader("X-Authentication", sessionTicket);
            }

            yield return www.SendWebRequest();

            if(www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError) {
                Debug.LogError(www.error);
            } else {
                JObject response = JObject.Parse(www.downloadHandler.text);

                if(api == "auth") {
                    HandleAuthenticationResponse(response);
                }

                OnSuccess(response);
            }
        }
    }

    void HandleAuthenticationResponse(JObject response) {
        sessionTicket = response["data"]["LoginResult"]["SessionTicket"].Value<string>();
    }

}
