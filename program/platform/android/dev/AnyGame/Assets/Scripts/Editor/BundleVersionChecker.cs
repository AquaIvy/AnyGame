using UnityEngine;
using System.Collections;
using UnityEditor;
using System;

[InitializeOnLoad]
public class BundleVersionChecker
{

    static BundleVersionChecker()
    {
        //Version v = new Version(PlayerSettings.bundleVersion);
        //Version newVer = new Version(v.Major, v.Minor, v.Build, v.Revision + 1);
        //PlayerSettings.bundleVersion = newVer.ToString();

#if UNITY_IPHONE
        PlayerSettings.iOS.buildNumber = newVer.ToString();
#endif
    }

}
