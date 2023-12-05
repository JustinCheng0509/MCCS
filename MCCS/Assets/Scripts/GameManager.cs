using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }
    public GameState State;
    public int selectedLevelNumber;
    public Button startButton;

    public int roundNum = 1;
    private float preRoundDelay = 5;
    private float endScreenDelay = 15;

    public enum GameState {
        MainMenu,
        PreRound,
        Round,
        EndScreen
    }

    private void Awake()
    {
        //check to see if gameManager already exists
        if (Instance == null)
        {
            //gameManager hasn't been created yet. Set it to this object and don't destroy it
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("Created GameManager");
        }
        else {
            //gameManager already exists, destory thiss
            Destroy(gameObject);
        }
    }

    public void UpdateGameState(GameState newState) {
        State = newState;

        switch (newState) {
            case GameState.MainMenu:
                ShowMainMenu();
                break;
            case GameState.PreRound:
                LoadPreRound();
                break;
            case GameState.Round:
                SceneManager.LoadScene(roundNum);
                break;
            case GameState.EndScreen:
                PlayWin();
                break;
            default:
                break;
        }
    }

    public void ShowMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadPreRound() {
        SceneManager.LoadScene("PreRound");
        StartCoroutine(PlayPreRoundVideo());
    }

    public void EndGame() {
        SceneManager.LoadScene(0);
    }

	// Start is called before the first frame update
	void Start()
    {
        UpdateGameState(GameState.MainMenu);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RoundComplete() { 
    
    }

    public void PlayWin() {
        SceneManager.LoadScene("Victory");
        StartCoroutine(PlayEndVideo());
    }

    private IEnumerator PlayEndVideo() {
        yield return new WaitForSeconds(endScreenDelay);
        roundNum = 1;
        UpdateGameState(GameState.MainMenu);
    }

    private IEnumerator PlayPreRoundVideo()
    {
        yield return new WaitForSeconds(preRoundDelay);
        roundNum++;
        UpdateGameState(GameState.Round);
    }
}
