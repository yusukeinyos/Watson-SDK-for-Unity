using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WebSocketSharp;

public class InputManager : MonoBehaviour
{
    public LanguageTranslatorDemo LanguageTranslator;
    public ToneAnalizerDemo ToneAnalizer;

    private InputField _inputField;

	// Use this for initialization
	void Start ()
	{
	    _inputField = GetComponent<InputField>();

	    Initialize();
	}

    public void OnTextInputEnd()
    {
        var text = _inputField.text;
        if (!text.IsNullOrEmpty())
        {
            if(LanguageTranslator != null)
            {
                LanguageTranslator.Translate(text);
            }
            if (ToneAnalizer != null)
            {
                ToneAnalizer.Analize(text);
            }
        }

        Initialize();
    }

    private void Initialize()
    {
//        _inputField.text = "";
        _inputField.ActivateInputField();
    }
	
}
