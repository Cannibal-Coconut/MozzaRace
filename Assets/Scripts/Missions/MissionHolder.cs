using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionHolder : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Image _icon;
    [SerializeField] TextMeshProUGUI _title;
    [SerializeField] TextMeshProUGUI _description;
    [SerializeField] TextMeshProUGUI _reward;
    [SerializeField] TextMeshProUGUI _skipPrice;

    [Space(5)]
    [SerializeField] Button _skipButton;

    Mission _mission;

    ProfileInventory _profileInventory;
    MissionWatcher _watcher;

    Language _currentLanguage;

    private void Awake()
    {
        _profileInventory = FindObjectOfType<ProfileInventory>();
        _watcher = FindObjectOfType<MissionWatcher>();

        _skipButton.onClick.AddListener(() =>
        {
            if (_profileInventory.points >= _mission.skipPrice)
            {
                _profileInventory.RemovePoints(_mission.skipPrice);

                _watcher.SkipMission(_mission);
            }
        });
    }

    public void SetMission(Mission mission)
    {

        _icon.sprite = mission.icon;
        _skipPrice.text = mission.skipPrice.ToString();
        _reward.text = mission.points.ToString();
        _mission = mission;

        switch (_currentLanguage)
        {
            case Language.English:
                SetEnglishTextMission(mission);
                break;
            case Language.Spanish:
                SetSpanishTextMission(mission);
                break;
            default:
                break;
        }

    }

    void SetEnglishTextMission(Mission mission)
    {
        _title.text = mission.missionNameEnglish;
        _description.text = mission.missionDescriptionEnglish;
    }

    void SetSpanishTextMission(Mission mission)
    {
        _title.text = mission.missionNameSpanish;
        _description.text = mission.missionDescriptionSpanish;
    }

    public void SetLanguage(Language language)
    {
        _currentLanguage = language;

        if (_mission != null)
        {
            SetMission(_mission);
        }


    }
}
