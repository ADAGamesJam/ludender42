using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour {

    public static Dialog instance;

    [Header("Panels for activation")]
    public GameObject panel;
    public GameObject textHolder;

    [Header("Other")]
    public Text inputField;
    //public GameObject opitonSelector1;
    //public GameObject opitonSelector2;
    //public GameObject option1;
    //public GameObject option2;
    public Image iconHolder;
    public List<Sprite> icons;

    [Header("Parameters")]
    public float letterPause = 0.01f;


    private string message;
    private Coroutine coroutine;
    private bool selectorOn = false;
    private InteractableObject targetDialog;
    private bool breakCurrentPhrase = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        if (instance != this)
        {
            Destroy(this);
        }
    }

    //public void AttackDialog()
    //{
        
    //    textHolder.SetActive(false);
    //    attackOptions.SetActive(true);
    //    selectorOn = true;
    //}

    public void SetMessage(string message, IconType icon)
    {
        this.message = message;
        SetIcon(icon);
        Show();
        StartTyping();
    }

    public void Show()
    {
        textHolder.SetActive(true);
        panel.SetActive(true);
    }

    public void Hide()
    {
        panel.SetActive(false);

        textHolder.SetActive(false);
    }

    public void SetDialog(InteractableObject interactableOnject)
    {
        targetDialog = interactableOnject;
        Show();
        StartTyping();
    }

    public void SetIcon(IconType iconType)
    {
        iconHolder.sprite = icons[(int)iconType];
    }

    public void StartTyping()
    {
        HeroControlls.instance.canMove = false;
        HeroControlls.instance.isTyping = true;
        coroutine = StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        foreach (var phrase in targetDialog.phrases)
        {
            SetIcon(phrase.icon);
            inputField.text = "";
            foreach (char letter in phrase.message.ToCharArray())
            {
                
                instance.inputField.text += letter;
                //if (sound)
                //    audio.PlayOneShot(sound);
                //yield return 0;
                //if (Input.GetKeyDown(KeyCode.E))
                //{
                //    breakCurrentPhrase = false;
                //    instance.inputField.text = phrase.message;
                //    continue;
                //}

                yield return new WaitForSeconds(letterPause);
            }
            while (!Input.GetKeyDown(KeyCode.E))
            {
                yield return 0;
            }
        }
        Hide();
        HeroControlls.instance.isTyping = false;
        HeroControlls.instance.canMove = true;
    }

    IEnumerator TypeText2()
    {
        instance.inputField.text = "";
        foreach (char letter in instance.message.ToCharArray())
        {
            instance.inputField.text += letter;
            //if (sound)
            //    audio.PlayOneShot(sound);
            yield return 0;
            yield return new WaitForSeconds(letterPause);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (coroutine != null)
            {
                breakCurrentPhrase = true;
            }
        }
        //if (selectorOn)
        //{
        //    if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
        //    {
        //        opitonSelector1.SetActive(!opitonSelector1.activeSelf);
        //        opitonSelector2.SetActive(!opitonSelector2.activeSelf);
        //    }
        //    if (Input.GetKeyDown(KeyCode.Space))
        //    {
        //        if (opitonSelector1.activeSelf)
        //        {
        //            //Attack
        //        }
        //        if (opitonSelector1.activeSelf)
        //        {
        //            //Surrender
        //        }
        //    }
        //}
    }
}
