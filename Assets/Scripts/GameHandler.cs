using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum GameStates
{
    Menu = 0,
    Game = 1,
    Intermission = 2,
    End = 3,
}

public class GameHandler : MonoBehaviour
{
    #region Singleton
    public static GameHandler instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    [SerializeField] private CakeHealth cakeHealth;
    [SerializeField] private GameStates _states;

    [SerializeField, Space(10)] GameObject bugSpawner;
    public GameObject gameOverPanel, HUDPanel;

    public GameStates CurrentState
    {
        get => _states; 
        set => _states = value;
    }

    private void Start()
    {
        CurrentState = GameStates.Menu;

        bugSpawner.SetActive(false);
        cakeHealth.healthBar.transform.parent.gameObject.SetActive(false);
    }

    private void Update()
    {
        SwitchGameState();
    }

    private void SwitchGameState()
    {
        switch (_states)
        {
            case GameStates.Menu:
                bugSpawner.SetActive(false);
                cakeHealth.healthBar.transform.parent.gameObject.SetActive(false);
                break;
            case GameStates.Game:
                bugSpawner.SetActive(true);
                cakeHealth.healthBar.transform.parent.gameObject.SetActive(true);
                break;
            case GameStates.Intermission:
                cakeHealth.RegainHealth();
                break;
            case GameStates.End:
                HUDPanel.SetActive(false);
                gameOverPanel.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void SetGameState(int index)
    {
        _states = (GameStates)index;
    }

    public void QuitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
