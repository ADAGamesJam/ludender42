using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour {

    public static Dialog instance;

    [Header("Panels for activation")]
    public GameObject panelE11;
    public GameObject panelHologram;
    public GameObject panelSentry;
    public GameObject panelIntercom;

    [Header("Text Fields")]
    public Text textE11;
    public Text textHologram;
    public Text textSentry;
    public Text textIntercom;

    [Header("E11")]
    public Image e11IconHolder;
    public List<Sprite> icons;

    [Header("Parameters")]
    public float letterPause = 0.01f;
    public float skipPause = 0.1f;

    [Header("Sounds")]
    public AudioClip e11Audio;
    public AudioClip aliceAudio;
    public AudioClip sentryAudio;
    public AudioClip intercomAudio;


    private string message;
    private Coroutine coroutine;
    private bool selectorOn = false;
    private InteractableObject targetDialog;
    private bool skip = false;
    private float skipTimer;
    private Text inputField;

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

    public void Hide()
    {
        panelE11.SetActive(false);
        panelHologram.SetActive(false);
        panelSentry.SetActive(false);
        panelIntercom.SetActive(false);
    }

    public void SetDialog(InteractableObject interactableOnject)
    {
        targetDialog = interactableOnject;
        StartTyping();
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
            
            Hide();
            if (phrase.icon == IconType.E11_Normal)
            {
                panelE11.SetActive(true);
                e11IconHolder.sprite = icons[0];
                inputField = textE11;
                //MusicaManager.instance.PlaySound(e11Audio);
            }
            if (phrase.icon == IconType.E11_Panic)
            {
                panelE11.SetActive(true);
                e11IconHolder.sprite = icons[1];
                inputField = textE11;
               // MusicaManager.instance.PlaySound(e11Audio);
            }
            if (phrase.icon == IconType.E11_Thinking)
            {
                panelE11.SetActive(true);
                e11IconHolder.sprite = icons[2];
                inputField = textE11;
                //MusicaManager.instance.PlaySound(e11Audio);
            }
            if (phrase.icon == IconType.Hologram)
            {
                panelHologram.SetActive(true);
                inputField = textHologram;
                //MusicaManager.instance.PlaySound(aliceAudio);
            }
            if (phrase.icon == IconType.Intercom)
            {
                panelIntercom.SetActive(true);
                inputField = textIntercom;
               // MusicaManager.instance.PlaySound(intercomAudio);
            }
            if (phrase.icon == IconType.Sentry)
            {
                panelSentry.SetActive(true);
                inputField = textSentry;
                //MusicaManager.instance.PlaySound(sentryAudio);
            }
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
