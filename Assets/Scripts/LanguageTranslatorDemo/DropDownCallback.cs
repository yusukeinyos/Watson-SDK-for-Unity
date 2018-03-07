using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DropDownCallback : MonoBehaviour
{
    public event Action<string> ModelChangeEvent;

    public void OnValueChanged(int result)
    {
        if (ModelChangeEvent == null) return;
        switch (result)
        {
            case 0:
                ModelChangeEvent.Invoke("ja-en");
                break;
            case 1:
                ModelChangeEvent.Invoke("en-ja");
                break;
        }
    }
}
