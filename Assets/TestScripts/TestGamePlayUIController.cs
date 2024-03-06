using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestGamePlayUIController : MonoBehaviour
{
    [SerializeField] private GameObject takePebblePanel;
    [SerializeField] private GameObject btn1Border;
    [SerializeField] private GameObject btn2Border;
    [SerializeField] private GameObject btn3Border;
    private Image btn1BorderImg;
    private Image btn2BorderImg;
    private Image btn3BorderImg;
    private float hightLightTime;
    private int btnIsHightLighted;

    private void Awake() {
        hightLightTime = 0f;
        btnIsHightLighted = 0;

        GetButtonBorderImage();
        SetGetPebbleButtonBorderColor(1, 1f, 1f, 1f);
        SetGetPebbleButtonBorderColor(2, 1f, 1f, 1f);
        SetGetPebbleButtonBorderColor(3, 1f, 1f, 1f);
    }

    private void Update() {
        HightLightCounter();
    }

    private void HightLightCounter()
    {
        if (btnIsHightLighted != 0)
        {
            hightLightTime += Time.deltaTime;
            if (hightLightTime >= 0.75f)
            {
                SetGetPebbleButtonBorderColor(btnIsHightLighted, 1f, 1f, 1f);
                hightLightTime = 0;
                btnIsHightLighted = 0;
            }
        }
    }

    private void GetButtonBorderImage()
    {
        btn1BorderImg = btn1Border.GetComponent<Image>();
        btn2BorderImg = btn2Border.GetComponent<Image>();
        btn3BorderImg = btn3Border.GetComponent<Image>();
    }

    private void SetGetPebbleButtonBorderColor(int buttonOrder, float r, float g, float b)
    {
        switch (buttonOrder)
        {
            case 1:
                btn1BorderImg.color = new Color(r, g, b);
                break;
            case 2:
                btn2BorderImg.color =  new Color(r, g, b);
                break;
            case 3:
                btn3BorderImg.color = new Color(r, g, b);
                break;
        }

        hightLightTime = 0f;
    }

    public void HightLightButton(int btnOrder)
    {
        SetGetPebbleButtonBorderColor(btnOrder, 1f, 0.9987818f, 0.06289297f);
        btnIsHightLighted = btnOrder;
    }

    public void EnableTakePebblePanel(bool state)
    {
        takePebblePanel.SetActive(state);
    }
}
