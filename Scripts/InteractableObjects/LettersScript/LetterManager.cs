using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Xml.Linq; 


public class LetterManager : MonoBehaviour
{
    public static LetterManager instance { get; private set; }
    private List<string> letters;
    void Awake()
    {
        instance = this;
        letters = new List<string>();
        this.ParseFile();
       

    }
    public string GetText(int index)
    {
        
        return letters[index];
    }
    private void ParseFile()
    {
        TextAsset txtXml = Resources.Load<TextAsset>("LetterText");
        var document = XDocument.Parse(txtXml.text);
        var allLetters = document.Element("document").Elements("letter"); 
        foreach(var letter in allLetters)
        {
            string text = letter.ToString().Replace("<letter>", "").Replace("</letter>", "");
            letters.Add(text);
        }
    }
}
