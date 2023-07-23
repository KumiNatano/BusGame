using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class MenuController : MonoBehaviour
{
    [SerializeField] private string nameOfNewGameScene;

    [SerializeField] private VisualTreeAsset settingsMenuTemplate;
    [SerializeField] private VisualElement _settingsMenu;

    [SerializeField] private AudioManager audioManager;
    
    
    private VisualElement _buttonsWrapper;
    
    private UIDocument _document;
    private Button _newGameButton;
    private Button _loadButton;
    private Button _settingsButton;
    private Button _quitButton;

    private Slider _musicSlider;
    private Slider _sfxSlider;
    private void Awake()
    {
        _document = GetComponent<UIDocument>();
        
        _buttonsWrapper = _document.rootVisualElement.Q<VisualElement>("Menu");
        
        _newGameButton = _document.rootVisualElement.Q<Button>("NewGameButton");
        _loadButton = _document.rootVisualElement.Q<Button>("LoadGameButton"); 
        _settingsButton = _document.rootVisualElement.Q<Button>("OptionsButton"); 
        _quitButton = _document.rootVisualElement.Q<Button>("QuitButton");

        _newGameButton.clicked += NewGameButtonClicked;
        _quitButton.clicked += ExitButtonClicked;
        _settingsButton.clicked += SettingsButtonClicked;

        _settingsMenu = settingsMenuTemplate.CloneTree();

        var backButton = _settingsMenu.Q<Button>("BackButton");
        backButton.clicked += BackButtonClicked;

        _musicSlider = _settingsMenu.Q<Slider>("MusicSlider");
        _musicSlider.RegisterValueChangedCallback(MusicSliderChanged);

        _sfxSlider = _settingsMenu.Q<Slider>("SFXSlider");
        _sfxSlider.RegisterValueChangedCallback(SfxSliderChanged);

    }

    //Music Slider
    private void MusicSliderChanged(ChangeEvent<float> evt)
    {
        audioManager.SetMusiCVolume(_musicSlider.value);
    }

    public void SetMusicSliderValue(float value)
    {
        _musicSlider.value = value;
    }
    
    //SFX Slider
    private void SfxSliderChanged(ChangeEvent<float> evt)
    {
        audioManager.SetSfxVolume(_sfxSlider.value);
    }

    public void SetSfxSliderValue(float value)
    {
        _sfxSlider.value = value;
    }
    
    //Play Button
    private void NewGameButtonClicked()
    {
        SceneManager.LoadScene(nameOfNewGameScene);
    }
    
    //Exit Button
    private void ExitButtonClicked()
    {
        Application.Quit();
    }

    //Settings
    private void SettingsButtonClicked()
    {
        _buttonsWrapper.Clear();
        _buttonsWrapper.Add(_settingsMenu);
    }

    private void BackButtonClicked()
    {
        _buttonsWrapper.Clear();
        _buttonsWrapper.Add(_newGameButton);
        _buttonsWrapper.Add(_loadButton);
        _buttonsWrapper.Add(_settingsButton);
        _buttonsWrapper.Add(_quitButton);
    }
}
