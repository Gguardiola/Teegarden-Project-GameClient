using System;
using TMPro;
using UnityEngine;

public class APIClientHandler : MonoBehaviour
{
        public APIClient apiclient;
        public string jsonPayload;
        private bool loadSent = false;
        public GameObject errorUIPopUp;
        public TextMeshProUGUI errorText;
        public bool isError = false;
        private void Start()
        {
                if (apiclient != null)
                {
                        StartCoroutine(apiclient.Login("local_debug_player", "password123")); //TODO: hacer que cargue desde un fichero, como su fuese uuid de la copia del juego...
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
        
        private void CheckIfLoggedIn()
        {
                if (apiclient != null && apiclient.IsLoggedIn && !loadSent)
                {
                        StartCoroutine(apiclient.PostCombatLog(jsonPayload));
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
