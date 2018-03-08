using System.Collections;
using System.Collections.Generic;
using IBM.Watson.DeveloperCloud.Connection;
using IBM.Watson.DeveloperCloud.Logging;
using IBM.Watson.DeveloperCloud.Services.ToneAnalyzer.v3;
using IBM.Watson.DeveloperCloud.Utilities;
using UnityEngine;
using UnityEngine.UI;
using WebSocketSharp;

public class ToneAnalizerDemo : MonoBehaviour 
{
    private string _username = null;
    private string _password = null;
    private string _url = null;

    private ToneAnalyzer _toneAnalyzer;
    private string _toneAnalyzerVersionDate = "2017-05-26";

    private string _stringToTestTone = "This service enables people to discover and understand, and revise the impact of tone in their content. It uses linguistic analysis to detect and interpret emotional, social, and language cues found in text.";
    private bool _analyzeToneTested = false;

	// Use this for initialization
	void Start () 
    {
        LogSystem.InstallDefaultReactors();

        //  Create credential and instantiate service
        Credentials credentials = new Credentials(_username, _password, _url);

        _toneAnalyzer = new ToneAnalyzer(credentials);
        _toneAnalyzer.VersionDate = _toneAnalyzerVersionDate;

        Runnable.Run(Examples());
	}
	
    public void Analize(string text)
    {
        if(!text.IsNullOrEmpty())
        {
            return;
        }
        _stringToTestTone = text;
        Runnable.Run(Examples());
    }

    private IEnumerator Examples()
    {
        //  Analyze tone
        if (!_toneAnalyzer.GetToneAnalyze(OnGetToneAnalyze, OnFail, _stringToTestTone))
            Log.Debug("ExampleToneAnalyzer.Examples()", "Failed to analyze!");

        while (!_analyzeToneTested)
            yield return null;

        Log.Debug("ExampleToneAnalyzer.Examples()", "Tone analyzer examples complete.");
    }

    private void OnGetToneAnalyze(ToneAnalyzerResponse resp, Dictionary<string, object> customData)
    {
        Log.Debug("ExampleToneAnalyzer.OnGetToneAnalyze()", "{0}", customData["json"].ToString());
        _analyzeToneTested = true;
    }

    private void OnFail(RESTConnector.Error error, Dictionary<string, object> customData)
    {
        Log.Error("ExampleRetrieveAndRank.OnFail()", "Error received: {0}", error.ToString());
    }
}
