using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

public class PlatformDetector
{
    [DllImport("__Internal")]
    private static extern bool IsMobile();
    public bool IsPlatformMobile()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
            return IsMobile();
#endif
        return false;
    }
}