using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FPScount : MonoBehaviour
{
    float fps = 0.0f;
    public TextMeshPro t1;

    string text;

    public bool ModoFPS;

    //public GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        //gm = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ModoFPS)
        {
            fps += (Time.unscaledDeltaTime - fps) * 0.1f;
        }
        else
        {
            t1.text = "∞";
        }
    }

    private void OnGUI()
    {
        int w = Screen.width, h = Screen.height;

        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(0, 0, w, h * 2/100);
        style.alignment = TextAnchor.UpperCenter;
        style.fontSize = h * 3 / 100;
        style.normal.textColor = new Color(0.7f, 0.7f, 0.7f, 1.0f);
        float msec = fps * 1000.0f;
        float MostrarFps = 1.0f / fps;

        if (ModoFPS)
        {
            text = string.Format("{1:0.} fps", msec, MostrarFps);
            t1.text = text;
            GUI.Label(rect, text, style);
        }
        else
        {
            t1.text = "∞";
        }
    }
}
