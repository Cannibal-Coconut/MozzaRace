using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ProfileInventory : MonoBehaviour, ILiveListener
{
    public int points { get; private set; }
    public int matchPoints { get; private set; }

    static ProfileInventory _instance;

    public List<Skin> skins;

    Action _onEconomyChange;

    string _username;
    string _password;
    bool _logged;

    const string DatabaseDomainURL = "https://mozzadb.000webhostapp.com";

    Coroutine _databaseCoroutine;
    bool _databaseCoroutineAvaliable = true;

    public Action onUpdateCurrentPoints;

    private void Awake()
    {
        //Make Sure there is only one of these.
        if (_instance)
        {
            _instance.SetListeners();
            DestroyImmediate(gameObject);
        }
        else
        {
            skins = new List<Skin>();
            _instance = this;
            transform.parent = null;
            DontDestroyOnLoad(this);

            _logged = false;
            _databaseCoroutineAvaliable = true;

            SetListeners();
        }
    }

    private void Start()
    {
        if (_onEconomyChange != null)
        {
            _onEconomyChange.Invoke();
        }

    }

    public void AddBoughtSkin(Skin skin)
    {
        skin.purchased = true;
        
        skins.Add(skin);

        UpdateInventoryInDatabase();
    }

    public void LogInFromDatabase(string username, string password, Action successCallback, Action failureCallback)
    {
        if (_databaseCoroutineAvaliable)
        {
            Action<ProfileSaveData> callback = SetProfileFromData;

            if (successCallback != null)
            {
                callback += (data) =>
                {
                    successCallback.Invoke();
                };
            }

            _databaseCoroutine = StartCoroutine(LogIn(username, password, callback, failureCallback));
        }

    }

    IEnumerator LogIn(string username, string password, Action<ProfileSaveData> successCallback, Action failureCallback)
    {
        _databaseCoroutineAvaliable = false;

        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);

        using (UnityWebRequest www = UnityWebRequest.Post(DatabaseDomainURL + "/LogIn.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(www.error);
            }
            else
            {
                // Show results as text
                try
                {
                    ProfileSaveData data = JsonUtility.FromJson<ProfileSaveData>(www.downloadHandler.text);

                    _username = username;
                    _password = password;
                    _logged = true;

                    if (successCallback != null)
                    {
                        successCallback.Invoke(data);
                    }
                }
                catch
                {
                    Debug.LogWarning("Check Password and Username: " + www.downloadHandler.text);

                    if (failureCallback != null)
                    {
                        failureCallback.Invoke();
                    }
                }
            }
        }

        _databaseCoroutineAvaliable = true;
    }

    public void SignInFromDatabase(string username, string password, Action successCallback, Action failureCallback)
    {
        if (_databaseCoroutineAvaliable)
        {
            _databaseCoroutine = StartCoroutine(SignIn(username, password, successCallback, failureCallback));
        }
    }

    IEnumerator SignIn(string username, string password, Action successCallback, Action failureCallback)
    {
        _databaseCoroutineAvaliable = false;

        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);

        skins = new List<Skin>();
        skins.Add(new Skin(SkinEnum.DEFAULT, 0));
        var saveData = new ProfileSaveData(points, skins);

        form.AddField("loginData", JsonUtility.ToJson(saveData));

        using (UnityWebRequest www = UnityWebRequest.Post(DatabaseDomainURL + "/SignIn.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(www.error);

            }
            else
            {

                if (www.downloadHandler.text == "OK")
                {

                    SetProfileFromData(saveData);
                    _username = username;
                    _password = password;
                    _logged = true;

                    if (successCallback != null)
                    {
                        successCallback.Invoke();
                    }
                }
                else
                {

                    Debug.LogWarning(www.downloadHandler.text);
                    if (failureCallback != null)
                    {
                        failureCallback.Invoke();

                    }

                }


            }
        }

        _databaseCoroutineAvaliable = true;
    }

    public void UpdateInventoryInDatabase(Action successCallback, Action failureCallback)
    {
        if (_logged)
        {
            StartCoroutine(UpdateProfile(_username, _password, successCallback, failureCallback));
        }
    }

    public void UpdateInventoryInDatabase()
    {
        UpdateInventoryInDatabase(null, null);
    }

    IEnumerator UpdateProfile(string username, string password, Action successCallback, Action failureCallback)
    {
        _databaseCoroutineAvaliable = false;

        WWWForm form = new WWWForm();
        form.AddField("updateUser", username);
        form.AddField("updatePass", password);

        var saveData = new ProfileSaveData(points, skins);

        form.AddField("updateData", JsonUtility.ToJson(saveData));

        using (UnityWebRequest www = UnityWebRequest.Post(DatabaseDomainURL + "/UpdateUser.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(www.error);

            }
            else
            {

                if (www.downloadHandler.text == "OK")
                {
                    if (successCallback != null)
                    {
                        successCallback.Invoke();
                    }
                }
                else
                {

                    Debug.LogWarning(www.downloadHandler.text);
                    if (failureCallback != null)
                    {
                        failureCallback.Invoke();

                    }

                }


            }
        }

        _databaseCoroutineAvaliable = true;
    }

    void SetProfileFromData(ProfileSaveData data)
    {
        points = data.points;
        skins = data.skins;
    }

    public void AddOnEconomyChangeListener(Action action)
    {
        _onEconomyChange += action;
    }

    public void AddPoints(int value)
    {
        points += Mathf.Abs(value);


        if (_onEconomyChange != null)
        {
            _onEconomyChange.Invoke();
        }
    }

    public void AddMatchPoints(int value)
    {
        matchPoints += Mathf.Abs(value);

        if (onUpdateCurrentPoints != null)
        {
            onUpdateCurrentPoints.Invoke();
        }

        if (_onEconomyChange != null)
        {
            _onEconomyChange.Invoke();
        }
    }

    public void PassMatchPointsToSkinPoints()
    {
        AddPoints(matchPoints);

        matchPoints = 0;

        if (_onEconomyChange != null)
        {
            _onEconomyChange.Invoke();
        }
    }

    public void RemoveMatchPoints(int value)
    {
        matchPoints -= Mathf.Abs(value);

        if (points < 0)
        {
            points = 0;

        }


        if (_onEconomyChange != null)
        {
            _onEconomyChange.Invoke();
        }
    }

    public void RemovePoints(int value)
    {
        points -= Mathf.Abs(value);

        if (points < 0)
        {
            points = 0;

        }


        if (_onEconomyChange != null)
        {
            _onEconomyChange.Invoke();
        }


    }

    public void OnLive()
    {
        UpdateInventoryInDatabase();
    }

    public void OnDead()
    {

    }

    public void SetListeners()
    {
        var player = FindObjectOfType<Health>();
        if (player)
        {
            player.AddLiveListener(OnLive);
            player.AddDeadListener(OnDead);
        }
    }

    [ContextMenu("Add a lot of points")]
    void LotsOfPoints()
    {
        AddPoints(99999);
    }

    [Serializable]
    public class ProfileSaveData
    {
        public int points;

        public List<Skin> skins;

        public ProfileSaveData(int points, List<Skin> skins)
        {
            this.points = points;
            this.skins = skins;

        }
    }

}
