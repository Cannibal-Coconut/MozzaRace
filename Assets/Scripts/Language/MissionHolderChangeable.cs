using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MissionHolder))]
public class MissionHolderChangeable : LanguageChangeable
{
    MissionHolder _holder;

    private void Awake()
    {
        _holder = GetComponent<MissionHolder>();
    }

    public override void ChangeLanguage(Language language)
    {
        _holder.SetLanguage(language);
    }


}
