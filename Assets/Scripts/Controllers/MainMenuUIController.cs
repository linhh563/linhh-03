using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIController : MonoBehaviour
{
    [SerializeField] private Dropdown languageDropdown;
    [SerializeField] private Slider volumeSlider;
    // Start is called before the first frame update
    void Start()
    {
        InitializeLanguageDropdown();
    }

    // private void Awake() {
    //     InitializeLanguageDropdown();
    // }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitializeLanguageDropdown()
    {
        if (languageDropdown == null)
        {
            Debug.LogError("LANGUAGE DROPDOWN IS NOT SET !!!");
            return;
        }

        languageDropdown.options.Clear();
        languageDropdown.options.Add(new Dropdown.OptionData("TIẾNG VIỆT"));
        languageDropdown.options.Add(new Dropdown.OptionData("ENGLISH"));
    }
}
