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
    public float skipPause = 0.1f;


    private string message;
    private Coroutine coroutine;
    private bool selectorOn = false;
    private InteractableObject targetDialog;
    private bool skip = false;
    private float skipTimer;

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

    private void Start()
    {
        skipTimer = skipPause;
    }
    

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
                if (skip)
                {
                    skip = false;
                    instance.inputField.text = phrase.message;
                    break;
                }

                yield return new WaitForSeconds(letterPause);
            }
            while (!skip)
            {
                yield return 0;
            }
            skip = false;
            yield return 0;
        }
        Hide();
        HeroControlls.instance.isTyping = false;
        HeroControlls.instance.canMove = true;
    }

    //IEnumerator TypeText2()
    //{
    //    instance.inputField.text = "";
    //    foreach (char letter in instance.message.ToCharArray())
    //    {
    //        instance.inputField.text += letter;
    //        //if (sound)
    //        //    audio.PlayOneShot(sound);
    //        yield return 0;
    //        yield return new WaitForSeconds(letterPause);
    //    }
    //}

    private void Update()
    {
        skipTimer -= Time.deltaTime;
        if (skipTimer < 0 && HeroControlls.instance.isTyping && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D)))
        {
            if (coroutine != null)
            {
                skip = true;
                skipTimer = skipPause;
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
