using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class APIClientHandler : MonoBehaviour
{
        public APIClient apiclient;
        public string jsonPayload;
        private bool loadSent = false;
        public GameObject errorUIPopUp;
        public TextMeshProUGUI errorText;
        public bool isError = false;
        public PauseMenu pauseMenu;
        private void Start()
        {
                if (apiclient != null)
                {
                        StartCoroutine(apiclient.Login("local_debug_player", "password123"));
                }
        }

        public void CleanErrorMessage()
        {
                isError = false;
                errorUIPopUp.SetActive(false);
        }
        
        void Update()
        {
                CheckIfLoggedIn();
        }

        public void CheckAPIError(InputActionMap currentGameplayMap, InputActionMap menu, bool isLockedFreeze, bool isLockedUnfreeze)
        {
                if (isError)
                {
                        pauseMenu.FreezeGame(currentGameplayMap, menu, isLockedFreeze);
                }
                else if (!isError && !pauseMenu.isPaused())
                {
                        pauseMenu.UnFreezeGame(currentGameplayMap, menu, isLockedUnfreeze);
                } 
        }
        
        private void CheckIfLoggedIn()
        {
                if (apiclient != null && apiclient.IsLoggedIn && !loadSent)
                {
                        //StartCoroutine(apiclient.PostCombatLog(jsonPayload));
                        loadSent = true;
                }
        }
        
        public void ShowErrorPopUp(string errMsg)
        {
                if (errorUIPopUp != null)
                {
                        errorText.text = "ERROR: " + errMsg + ". Using local defaults.";
                        errorUIPopUp.SetActive(true);
                        isError = true;
                }
        }

}
