using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class UIControllerLogin : MonoBehaviour
{

    public static string inputText = "";
    public InputField m_InputField;
    private bool stateMayus = false;
    public GameObject allCharacter;
    void Start()
    {
        GameObject[] buttonKeyboards = GameObject.FindGameObjectsWithTag("ButtonKeyboard");
        foreach(var key in buttonKeyboards)
        {
            string textKey = key.GetComponentInChildren<Text>().text;
            key.GetComponent<Button>().onClick.AddListener(delegate { ClickButton(textKey); });
        }
    }


    private void changeMayus()
    {
      

        foreach (Transform key in allCharacter.transform)
        {
            string textKey = key.gameObject.GetComponentInChildren<Text>().text;
            int isCharacter = Regex.Matches(textKey, @"[a-zA-Z]").Count;
            if(isCharacter > 0)
            {
                key.gameObject.GetComponentInChildren<Text>().text = stateMayus ? textKey.ToUpper() : textKey.ToLower();
            }
        }
        
    }
 
    private void ClickButton(string key)
    {
        switch (key)
        {
            case "Mayus":
                stateMayus = !stateMayus;
                changeMayus();
                break;
            case "Espacio":
                m_InputField.text += " ";
                break;
            case "Borrar":
                m_InputField.text = m_InputField.text.Remove(m_InputField.text.Length-1);
                break;
            case "Ingresar":
                Debug.Log("Erdaa");
                break;
            default:
                m_InputField.text += stateMayus ? key.ToUpper() : key.ToLower(); ; 
                break;
        }
    }
        
}
