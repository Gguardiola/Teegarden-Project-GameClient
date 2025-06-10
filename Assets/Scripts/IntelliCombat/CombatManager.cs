using System;
using System.Collections;
using System.Collections.Generic;
using IntelliCombat.MenuButtons;
using TMPro;
using UnityEngine;
using Action = IntelliCombat.Actions.Action;

public class CombatManager : MonoBehaviour
{
    public Camera sceneCamera;
    public Action currentAction;
    public EnemyDecisionMaker enemyDecisionMaker;
    private TurnResolver turnResolver;
    [Header("Combat stats")] 
    public String lastActionMessage = ". . .";
    public bool isPlayerTurn = true;
    public bool isCombatOver = false;
    public bool playerWon = false;
    [Header("Combat UI elements")] 
    public GameObject waitingTurnLabel;
    public GameObject playerTurnTagIndicator;
    public GameObject enemyTurnTagIndicator;
    public GameObject inActionButtons;
    public GameObject actionButtons;
    public GameObject combatOverButton;
    public TextMeshProUGUI lastActionMessageText;
    public TextMeshProUGUI playerHealthText;
    public TextMeshProUGUI playerEnergyText;
    public TextMeshProUGUI enemyHealthText;
    public TextMeshProUGUI enemyEnergyText;
    public GameObject popUpMessageBox;
    public bool popUpMessageEnabled = false;
    public GameObject PlayerAvatarModel;
    public GameObject EnemyAvatarModel;
    public ParticleSystem playerShieldParticleSystem;
    private CombatOverButton combatOverButtonComponent;
    [HideInInspector]
    public Avatar playerAvatar;
    public Avatar enemyAvatar;

    
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

        playerAvatar = new Avatar();
        enemyAvatar = new Avatar();
        turnResolver = new TurnResolver(this);
        combatOverButtonComponent = combatOverButton.GetComponent<CombatOverButton>();
        SetPlayerStats();
        SetEnemyStats();
        SetLastActionMessage(lastActionMessage);
        SetTurn();
        UpdateBuffs();
    }

    void Update()
    {
        CheckDangerHealth();
        CheckDangerEnergy();
        CheckIfEndgame();
    }
    
    private void CheckIfEndgame()
    {
        if (playerAvatar.GetHealth() <= 0)
        {
            isCombatOver = true;
            SetLastActionMessage("You lost the combat!");
            playerWon = false;
            CombatOverSetup();
        }
        else if (enemyAvatar.GetHealth() <= 0)
        {
            isCombatOver = true;
            SetLastActionMessage("You won the combat!");
            playerWon = true;
            CombatOverSetup();
        }
    }

    public void SetLastActionMessage(string message)
    {
        lastActionMessage = message;
        lastActionMessageText.text = lastActionMessage;
    }
    
    public void SetTemporalLastActionMessage(string message)
    {
        string previousMessage = lastActionMessage;
        lastActionMessage = message;
        lastActionMessageText.text = lastActionMessage;
        StartCoroutine(TemporalLastActionMessage(previousMessage));

    }
    
    private IEnumerator TemporalLastActionMessage(string previousMessage)
    {
        lastActionMessage = previousMessage;
        yield return new WaitForSeconds(5f);
        lastActionMessage = previousMessage;
        lastActionMessageText.text = lastActionMessage;
        
        
    }

    public void HandleTurn()
    {
        if (currentAction != null)
        {

            if (!currentAction.ResolveAction(turnResolver)) return;
            if (!currentAction.EndTurnAction()) return;
            if (isCombatOver)
            {
                return;
            }
            isPlayerTurn = !isPlayerTurn;
            SetTurn();
            /*currentAction = null;
            
            StartCoroutine(DEBUG_PlayerTurn());*/
        }


    }

    private IEnumerator DEBUG_EnemyTurn()
    {
        Debug.Log("Enemy turn started");

        bool decisionReady = false;
        Action decidedAction = null;

        enemyDecisionMaker.Decide((action) =>
        {
            decidedAction = action;
            decisionReady = true;
        });

        yield return new WaitUntil(() => decisionReady);

        Debug.Log("Enemy action decided: " + decidedAction.name);

        currentAction = decidedAction;
        HandleTurn();
    }
    private IEnumerator DEBUG_PlayerTurn(){
        yield return new WaitForSeconds(2f);
        SetLastActionMessage("Player turn");
        isPlayerTurn = true;
        SetTurn();
    }
    private void SetTurn()
    {
        if (isPlayerTurn)
        {
            //starting players turn
            enemyTurnTagIndicator.SetActive(false);
            playerTurnTagIndicator.SetActive(true);
            waitingTurnLabel.SetActive(false);
            actionButtons.SetActive(true);
            inActionButtons.SetActive(false);
            UpdateBuffs();
        }
        else
        {
            //ending players turn
            enemyTurnTagIndicator.SetActive(true);
            playerTurnTagIndicator.SetActive(false);
            waitingTurnLabel.SetActive(true);
            actionButtons.SetActive(false);
            inActionButtons.SetActive(false);
            UpdateBuffs();
            StartCoroutine(DEBUG_EnemyTurn());

        }
    }
    
    private void UpdateBuffs()
    {
        if (playerAvatar.isShielded && !isPlayerTurn)
        {
            playerShieldParticleSystem.Play();
        }
        else
        {
            playerShieldParticleSystem.Stop();
            playerAvatar.isShielded = false;
        }
        if (enemyAvatar.isShielded && !isPlayerTurn)
        {
            enemyAvatar.isShielded = false;
        }
    }
    
    private void CombatOverSetup()
    {
        enemyTurnTagIndicator.SetActive(false);
        playerTurnTagIndicator.SetActive(false);
        waitingTurnLabel.SetActive(false);
        actionButtons.SetActive(false);
        inActionButtons.SetActive(false);
        combatOverButton.SetActive(true);
        combatOverButtonComponent.SetWinner(playerWon);
        if (playerWon)
        {
            EnemyAvatarModel.SetActive(false);
            
        }
        else
        {
            PlayerAvatarModel.SetActive(false);
        }
    }
    
    public void SetPlayerStats()
    {
        playerHealthText.text = playerAvatar.GetHealth().ToString() + "/" + playerAvatar.GetMaxHealth().ToString();
        playerEnergyText.text = playerAvatar.GetEnergy().ToString() + "/" + playerAvatar.GetMaxEnergy().ToString();
    }
    
    public void SetEnemyStats()
    {
        enemyHealthText.text = enemyAvatar.GetHealth().ToString() + "/" + enemyAvatar.GetMaxHealth().ToString();
        enemyEnergyText.text = enemyAvatar.GetEnergy().ToString() + "/" + enemyAvatar.GetMaxEnergy().ToString();
    }

    private void GoBack()
    {
        if (currentAction != null && currentAction.isActionInProgress)
        {
            currentAction.CancelAction();
            currentAction = null;
        }
    }
    public void Click()
    {
        Vector3 mousePos = Input.mousePosition;
        if (sceneCamera != null)
        {
            Ray ray = sceneCamera.ScreenPointToRay(mousePos);
            bool raycastHit = Physics.Raycast(ray, out RaycastHit hit);
            if (raycastHit && hit.collider != null && hit.collider.CompareTag("CombatAction"))
            {
                currentAction = hit.collider.GetComponent<Action>();
                if (currentAction != null) currentAction.PerformAction();
            }
            
            if (raycastHit && hit.collider != null && hit.collider.CompareTag("CombatMenuButton"))
            {
                MenuButton menuButton = hit.collider.GetComponent<MenuButton>();
                if (menuButton != null && menuButton.Name == "GoBackButton" && !popUpMessageEnabled)
                {
                    GoBack();
                }

                if (menuButton != null && menuButton.Name == "EndTurnButton" && !popUpMessageEnabled)
                {
                    HandleTurn();
                }
                
                if (menuButton != null && menuButton.Name == "CombatOverButton" && !popUpMessageEnabled)
                {
                    combatOverButtonComponent.ContinueNextScene();
                }

                if (menuButton != null && menuButton.Name == "SelectAbilityButton" && !popUpMessageEnabled)
                {
                    currentAction.FirstReactorEvent(((SelectAbilityButton)menuButton).abilityData);
                }
            }

            if (raycastHit && hit.collider != null && hit.collider.CompareTag("ShowPopup") && !popUpMessageEnabled)
            {
                popUpMessageEnabled = true;
                popUpMessageBox.SetActive(true);
            }
            if (raycastHit && hit.collider != null && hit.collider.CompareTag("ClosePopup"))
            {
                Debug.Log("Close Popup");
                popUpMessageEnabled = false;
                popUpMessageBox.SetActive(false);
            }
        }

    }
    
    private void CheckDangerEnergy()
    {
        if (playerAvatar.GetEnergy() < 20f)
        {
            playerEnergyText.color = Color.red;
        }
        else
        {
            playerEnergyText.color = new Color32(24, 187, 190, 255);
        }
        if (enemyAvatar.GetEnergy() < 20f)
        {
            enemyEnergyText.color = Color.red;
        }
        else
        {
            enemyEnergyText.color = new Color32(24, 187, 190, 255);
        }
    }
    
    private void CheckDangerHealth()
    {
        if (playerAvatar.GetHealth() < 20f)
        {
            playerEnergyText.color = Color.red;
        }
        else
        {
            playerEnergyText.color = new Color32(27, 190, 24, 212);
        }
        
        if (enemyAvatar.GetHealth() < 20f)
        {
            enemyHealthText.color = Color.red;
        }
        else
        {
            enemyHealthText.color = new Color32(27, 190, 24, 212);
        }
    }
}
