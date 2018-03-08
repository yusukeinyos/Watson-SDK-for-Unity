using System.Collections;
using System.Collections.Generic;
using System.Linq;
using IBM.Watson.DeveloperCloud.Connection;
using IBM.Watson.DeveloperCloud.Logging;
using IBM.Watson.DeveloperCloud.Services.ToneAnalyzer.v3;
using IBM.Watson.DeveloperCloud.Utilities;
using UnityEngine;
using UnityEngine.UI;
using WebSocketSharp;

public class ToneAnalizerDemo : MonoBehaviour
{
    public Text ResultText;
    public UnitychanController UnitychanController;

    private string _username = "ca73cad6-cca8-4a5a-89f9-3ed2ec0b87b5";
    private string _password = "kBt4hhHpt6rI";
    private string _url = "https://gateway.watsonplatform.net/tone-analyzer/api";

    private ToneAnalyzer _toneAnalyzer;
    private string _toneAnalyzerVersionDate = "2017-05-26";

    private string _stringToTestTone = ""; //"This service enables people to discover and understand, and revise the impact of tone in their content. It uses linguistic analysis to detect and interpret emotional, social, and language cues found in text.";
    private bool _analyzeToneTested = false;

	// Use this for initialization
	void Start () 
    {
        LogSystem.InstallDefaultReactors();

        //  Create credential and instantiate service
        Credentials credentials = new Credentials(_username, _password, _url);

        _toneAnalyzer = new ToneAnalyzer(credentials);
        _toneAnalyzer.VersionDate = _toneAnalyzerVersionDate;

        // Runnable.Run(Examples());
	}
	
    public void Analize(string text)
    {
        if(text.IsNullOrEmpty())
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
        Tone[] emotionTone = null;
        foreach (var toneCategory in resp.document_tone.tone_categories)
        {
            if (toneCategory.category_name == "Emotion Tone")
            {
                emotionTone = toneCategory.tones.OrderByDescending(e => e.score).ToArray();
                break;
            }
        }

        var topScoreTone = "";
        if (emotionTone != null)
        {
            topScoreTone = emotionTone[0].tone_name;

            string resultText = "";
            foreach (var tone in emotionTone)
            {
                resultText += string.Format("[Tone: score={0}, tone_name={1}]", tone.score, tone.tone_name);
                resultText += "\n";
            }

            ResultText.text = resultText;
        }

        UnitychanController.ChangeEmotion(topScoreTone);

        Debug.Log(resp.sentences_tone[0]);
        Log.Debug("ExampleToneAnalyzer.OnGetToneAnalyze()", "{0}", customData["json"].ToString());
        _analyzeToneTested = true;
    }

    private void OnFail(RESTConnector.Error error, Dictionary<string, object> customData)
    {
        Log.Error("ExampleRetrieveAndRank.OnFail()", "Error received: {0}", error.ToString());
    }
}
