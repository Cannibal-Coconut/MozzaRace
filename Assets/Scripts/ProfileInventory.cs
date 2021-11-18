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

    const string FilePathName = "/MySaveData.dat";

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
            DontDestroyOnLoad(this);

            LoadProfileData();

            AddOnEconomyChangeListener(SaveProfileData);
        }
    }

    private void Start()
    {
        _onEconomyChange.Invoke();

        StartCoroutine(Login("juanito", "1000"));
    }

    IEnumerator GetText()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://localhost/Unity/GetUsers.php");
        yield return www.Send();

        if (www.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            Debug.Log(www.downloadHandler.text);

            // Or retrieve results as binary data
            byte[] results = www.downloadHandler.data;
        }
    }

    IEnumerator Login(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Unity/Login.php", form))
        {
            yield return www.Send();

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(www.error);
            }
            else
            {
                // Show results as text
                Debug.Log(www.downloadHandler.text);
            }
        }


    }

    void LoadProfileData()
    {
        if (File.Exists(Application.persistentDataPath + FilePathName))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + FilePathName, FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();

            premiumCoins = data.premiumCoins;
            skinPoints = data.skinPoints;
            skins = data.skins;

            Debug.Log("Game data loaded!");
        }

        else
        {
            premiumCoins = 5;
            skinPoints = 100000;
            skins = new List<Skin>();

            var whiteSkin = new Skin(Color.white, 0);
            whiteSkin.purchased = true;

            skins.Add(whiteSkin);

            SaveProfileData();
        }

    }

    void SaveProfileData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + FilePathName);

        SaveData data = new SaveData();
        data.premiumCoins = premiumCoins;
        data.skinPoints = skinPoints;
        data.skins = skins;

        bf.Serialize(file, data);
        file.Close();
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
    class SaveData
    {
        public int premiumCoins;
        public int skinPoints;

        public List<Skin> skins;

    }

}
