using System.Collections;
using System.Collections.Generic;
using IBM.Watson.DeveloperCloud.Connection;
using IBM.Watson.DeveloperCloud.Logging;
using IBM.Watson.DeveloperCloud.Services.LanguageTranslator.v2;
using IBM.Watson.DeveloperCloud.Utilities;
using UnityEngine;
using UnityEngine.UI;

public class LanguageTranslatorDemo : MonoBehaviour
{
    public Text ResponseText;

    private LanguageTranslator _languageTranslator;
    private string _translationModel = "en-es";

    void Start()
    {
        LogSystem.InstallDefaultReactors();

        Credentials languageTranslatorCredential = new Credentials()
        {
            Username = "0f90562f-1863-4bbe-b9be-87276c087687",
            Password = "yfWQPlOjkuKx",
            Url = "https://gateway.watsonplatform.net/language-translator/api"
        };
        _languageTranslator = new LanguageTranslator(languageTranslatorCredential);

//        Translate("Where is the library?");
    }

    public void Translate(string text)
    {
        ResponseText.text = text;
//        _languageTranslator.GetTranslation(OnTranslate, OnFail, text, _translationModel);
    }

    private void OnTranslate(Translations response, Dictionary<string, object> customData)
    {
        ResponseText.text = response.translations[0].translation;
    }

    private void OnFail(RESTConnector.Error error, Dictionary<string, object> customData)
    {
        Log.Debug("LanguageTranslatorDemo.OnFail()", "Error: {0}", error.ToString());
    }
}
