using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MenuController : MonoBehaviour
{
    [SerializeField] private String nameOfNewGameScene;

    [SerializeField] private VisualTreeAsset _settingsButtonsTemplate;
    [SerializeField] private VisualElement _settingsButtons;
    
    
    private VisualElement _buttonsWrapper;
    
    private UIDocument _document;
    private Button _playButton;
    private Button _loadButton;
    private Button _settingsButton;
    private Button _quitButton;
    
    private void Awake()
    {
        _document = GetComponent<UIDocument>();
        
        _buttonsWrapper = _document.rootVisualElement.Q<VisualElement>("Menu");
        
        _playButton = _document.rootVisualElement.Q<Button>("NewGameButton");
        _loadButton = _document.rootVisualElement.Q<Button>("LoadGameButton"); 
        _settingsButton = _document.rootVisualElement.Q<Button>("OptionsButton"); 
        _quitButton = _document.rootVisualElement.Q<Button>("QuitButton");

        _playButton.clicked += PlayButtonClicked;
        _quitButton.clicked += ExitButtonClicked;
        _settingsButton.clicked += SettingsButtonClicked;

        _settingsButtons = _settingsButtonsTemplate.CloneTree();

        var backButton = _settingsButtons.Q<Button>("BackButton");
        backButton.clicked += BackButtonClicked;
    }

    private void PlayButtonClicked()
    {
        SceneManager.LoadScene(nameOfNewGameScene);
    }
    
    private void ExitButtonClicked()
    {
        Application.Quit();
    }

    private void SettingsButtonClicked()
    {
        _buttonsWrapper.Clear();
        _buttonsWrapper.Add(_settingsButtons);
    }

    private void BackButtonClicked()
    {
        _buttonsWrapper.Clear();
        _buttonsWrapper.Add(_playButton);
        _buttonsWrapper.Add(_loadButton);
        _buttonsWrapper.Add(_settingsButton);
        _buttonsWrapper.Add(_quitButton);
        
    }
}
