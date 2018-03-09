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
    public Dropdown DropDown;

    private DropDownCallback _dropDownCallback;
    private LanguageTranslator _languageTranslator;
    private string _translationModel = "en-ja";

    void Start()
    {
        LogSystem.InstallDefaultReactors();

        Credentials languageTranslatorCredential = new Credentials()
        {
            Username = "8cf97e2c-9786-43aa-a560-0562acf00229",
            Password = "xdXLIFUG6DHz",
            Url = "https://gateway.watsonplatform.net/language-translator/api"
        };

        _languageTranslator = new LanguageTranslator(languageTranslatorCredential);

        _dropDownCallback = DropDown.GetComponent<DropDownCallback>();
        SetEvent();
    }

    

    public void Translate(string text)
    {
        _languageTranslator.GetTranslation(OnTranslate, OnFail, text, _translationModel);
    }

    private void OnTranslate(Translations response, Dictionary<string, object> customData)
    {
        ResponseText.text = response.translations[0].translation;
    }

    private void OnFail(RESTConnector.Error error, Dictionary<string, object> customData)
    {
        Log.Debug("LanguageTranslatorDemo.OnFail()", "Error: {0}", error.ToString());
    }

    private void SetEvent()
    {
        if (_dropDownCallback == null)
        {
            return;
        }
        _dropDownCallback.ModelChangeEvent += model =>
        {
            _translationModel = model;
        };
    }
}
