using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    public LanguageTranslatorDemo LanguageTranslator;

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
        LanguageTranslator.Translate(text);

        Initialize();
    }

    private void Initialize()
    {
        _inputField.text = "";
        _inputField.ActivateInputField();
    }
	
}
