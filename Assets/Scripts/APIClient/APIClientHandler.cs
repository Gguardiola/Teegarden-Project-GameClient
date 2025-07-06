using System;
using UnityEngine;

public class APIClientHandler : MonoBehaviour
{
        public APIClient apiclient;
        public string jsonPayload;
        private bool loadSent = false;
        private void Start()
        {
                if (apiclient != null)
                {
                        StartCoroutine(apiclient.Login("local_debug_player", "password123"));
                        if (apiclient.IsLoggedIn)
                        {
                                StartCoroutine(apiclient.PostCombatLog(jsonPayload));  
                        }
                }
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
}