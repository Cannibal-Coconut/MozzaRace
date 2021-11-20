using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Networking;

public class ProfileInventory : MonoBehaviour
{
    public int premiumCoins { get; private set; }
    public int skinPoints { get; private set; }

    static ProfileInventory _instance;

    public List<Skin> skins;

    Action _onEconomyChange;

    string _username;
    string _password;
    bool _logged;

    const string DatabaseDomainURL = "https://mozzadb.000webhostapp.com";

    Coroutine _databaseCoroutine;
    bool _databaseCoroutineAvaliable = true;

    private void Awake()
    {
        //Make Sure there is only one of these.
        if (_instance)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            //DontDestroyOnLoad(this);

            _logged = false;
            _databaseCoroutineAvaliable = true;

            /*
             * LoadProfileData();

            AddOnEconomyChangeListener(SaveProfileData);
            */
        }
    }

    private void Start()
    {
        //StartCoroutine(SignIn("Testamon", "12345"));

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
            yield return www.Send();

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
        skins.Add(new Skin(Color.white, 0));
        var saveData = new ProfileSaveData(skinPoints, skins);

        form.AddField("loginData", JsonUtility.ToJson(saveData));

        using (UnityWebRequest www = UnityWebRequest.Post(DatabaseDomainURL + "/SignIn.php", form))
        {
            yield return www.Send();

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

        var saveData = new ProfileSaveData(skinPoints, skins);

        form.AddField("updateData", JsonUtility.ToJson(saveData));

        using (UnityWebRequest www = UnityWebRequest.Post(DatabaseDomainURL + "/UpdateUser.php", form))
        {
            yield return www.Send();

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
        skinPoints = data.points;
        skins = data.skins;
    }

    public void AddOnEconomyChangeListener(Action action)
    {
        _onEconomyChange += action;
    }

    public void AddPremiumCoins(int value)
    {
        premiumCoins += Mathf.Abs(value);


        if (_onEconomyChange != null)
        {
            _onEconomyChange.Invoke();
        }
    }

    public void RemovePremiumCoins(int value)
    {
        premiumCoins -= Mathf.Abs(value);

        if (premiumCoins < 0)
        {
            premiumCoins = 0;

        }

        if (_onEconomyChange != null)
        {
            _onEconomyChange.Invoke();
        }
    }

    public void AddSkinPoints(int value)
    {
        skinPoints += Mathf.Abs(value);


        if (_onEconomyChange != null)
        {
            _onEconomyChange.Invoke();
        }
    }

    public void RemoveSkinPoints(int value)
    {
        skinPoints -= Mathf.Abs(value);

        if (skinPoints < 0)
        {
            skinPoints = 0;

        }


        if (_onEconomyChange != null)
        {
            _onEconomyChange.Invoke();
        }


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
