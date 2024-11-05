using System.Collections;

#if UNITY_WEBGL
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;

public class WebGLInterface : MonoBehaviour
{
    //called from webpage
    public void send_CSV_credentials_to_Unity(string CSV_of_credentials)
    {
        Debug.Log("FROM UNITY | send_CSV_credentials_to_Unity");
        //var AuthenticationManager = FindAnyObjectByType<AdroitStudios.AuthenticationManager>();
        //if (AuthenticationManager == null)
        //{
        //    Debug.Log("FROM UNITY | could not find AuthenticationManager");

        //}
        //var listOfAllCredentials = CSV_of_credentials.Split("\n").ToList().Select(x => { return new List<string>(x.Split(",")).ToArray(); }).ToList();
        //var skipFirstLine = listOfAllCredentials[0][0].ToLower().Trim() == "username" || listOfAllCredentials[0][0].ToLower().Trim() == "user name";
        //var listOfCredentials = skipFirstLine ? listOfAllCredentials.Skip(1) : listOfAllCredentials;
        //var userData = new AdroitUserData();

        //userData.Users = listOfCredentials.Select(x => { return new AdroitUserData_User(x[0].TrimEnd('\r', '\n',',',' '), x[1].TrimEnd('\r', '\n', ',', ' ')); }).ToList();
        //AuthenticationManager.UserData = userData;
        //Debug.Log("FROM UNITY | updated AuthenticationManager | " + userData.Users.Count());
    }

    //called from webpage
    public void capture_profiler_data()
    {
        Debug.Log("FROM UNITY | capture_profiler_data");
        var AdroitProfilerLogger = FindAnyObjectByType<AdroitProfiler_Logger>();
        if (AdroitProfilerLogger != null)
        {
            AdroitProfilerLogger.SaveLogs();
        }
    }
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

}
#endif