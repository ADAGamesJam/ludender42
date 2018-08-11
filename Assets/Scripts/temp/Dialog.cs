using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour {

    public static Dialog instance;
    public GameObject dialogBox;
    public Text text;
    public float letterPause = 0.01f;

    private string message;
    private Coroutine coroutine;

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

    public static void Log(string message)
    {
        instance.message = message;
        Show();
        instance.StartTyping();
    }

    public static void Show()
    {
        instance.dialogBox.SetActive(true);
    }

    public static void Hide()
    {
        instance.dialogBox.SetActive(false);
    }

    public static void SetMessage(string message)
    {
        instance.text.text = message;
    }

    public void StartTyping()
    {
        coroutine = StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        instance.text.text = "";
        foreach (char letter in instance.message.ToCharArray())
        {
            instance.text.text += letter;
            //if (sound)
            //    audio.PlayOneShot(sound);
            yield return 0;
            yield return new WaitForSeconds(letterPause);
        }
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
                SetMessage(message);
                coroutine = null;
            }
        }
    }
}
