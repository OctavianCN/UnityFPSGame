using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LettersScript : MonoBehaviour
{
    [SerializeField] private Transform letterTemplate;
    [SerializeField] private Transform lettersContainer;
    private Transform letterImg;
    private bool isPicked;
    private static int setId;
    private int id = 0;
    private string text;
    void Awake()
    {
        setId = 0;
    }
    void Start()
    {
        id = setId;
        setId++;
        isPicked = false;
        text = LetterManager.instance.GetText(id);
        letterImg = Instantiate(letterTemplate, lettersContainer);
        letterImg.GetComponentInChildren<Text>().text = text;
    }
    public void SetPicked(bool x)
    {
        isPicked = x;
    }
    public void Destroy()
    {
        Destroy(this.gameObject);
    }
    public void ShowLetter()
    {
        letterImg.gameObject.SetActive(true);
    }
    public void HideLetter()
    {
        letterImg.gameObject.SetActive(false);
    }
}
