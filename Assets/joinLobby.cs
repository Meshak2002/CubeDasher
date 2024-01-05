using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class joinLobby : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    public GameObject Player, content, prefabcall;
    public Vector3 rt, rr;
    public AppIdInput AppInputConfig;
    public TOKEN t;
    public Text CLannam;
    public string data;
    public GameObject eventt;
    public GameObject Camera, returnp;
    public void OnEnable()
    {
        //CreateRoom();   
        StartCoroutine(getText());
    }
    IEnumerator getText()
    {
        yield return new WaitForSeconds(1);
        Debug.Log("http://localhost:8080/access_token?channelName=" + CLannam.text + "&role=subscriber&userid=1234&expiryTime=6400");
        using (UnityWebRequest www = UnityWebRequest.Get("http://localhost:8080/access_token?channelName=" + CLannam.text + "&role=subscriber"))
        {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                data = www.downloadHandler.text;
                t = JsonUtility.FromJson<TOKEN>(data);
            }
        }

    }
    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(clan.instance.d.clan);
        Debug.Log("Created");
    }
    public void OnLeaveButtonClicked()
    {
        StartCoroutine(UnloadSceneAsync());
    }
    public IEnumerator UnloadSceneAsync()
    {
        AsyncOperation async = SceneManager.UnloadSceneAsync("BasicAudioCallScene");
        yield return async;
        eventt.gameObject.SetActive(true);
        returnp.SetActive(false);
        Camera.GetComponent<AudioListener>().enabled = true;
    }
    public void JoinRoom()
    {

        this.AppInputConfig.channelName = CLannam.text;
        this.AppInputConfig.token = t.token;
        var sceneName = "BasicAudioCallScene";
        eventt.gameObject.SetActive(false);

        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        Camera.GetComponent<AudioListener>().enabled = false;
        //PhotonNetwork.JoinRoom(clan.instance.d.clan);
        Debug.Log("Joined");
        returnp.SetActive(true);
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("JoinedRoom");
        PhotonNetwork.Instantiate(Player.name, Vector3.zero, Quaternion.identity);
        rt = prefabcall.GetComponent<RectTransform>().anchoredPosition;
        rr = prefabcall.GetComponent<RectTransform>().localScale;
        GameObject g = PhotonNetwork.Instantiate(prefabcall.name, Vector3.zero, Quaternion.identity);
        g.transform.parent = content.transform.Find(clan.instance.d.nane);
        g.GetComponent<RectTransform>().anchoredPosition = rt;
        g.GetComponent<RectTransform>().localScale = rr;

    }
}
