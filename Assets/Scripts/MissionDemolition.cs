using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum GameMode
{
    idle,
    playing,
    levelEnd,
    gameOver // New mode for Game Over
}

public class MissionDemolition : MonoBehaviour
{
    static private MissionDemolition S;

    [Header("Inscribed")]
    public TMP_Text uitLevel;
    public TMP_Text uitShots;
    public Vector3 castlePos;
    public GameObject[] castles;

    [Header("Game Over UI")]
    public GameObject gameOverPanel;
    public Button playAgainButton;

    [Header("Dynamic")]
    public int level;
    public int levelMax;
    public int shotsTaken;
    public GameObject castle;
    public GameMode mode = GameMode.idle;
    public string showing = "Show Slingshot";

    void Start()
    {
        S = this;

        level = 0;
        shotsTaken = 0;
        levelMax = castles.Length;

        gameOverPanel.SetActive(false);

        StartLevel();
    }

    void StartLevel()
    {
        if (castle != null)
        {
            Destroy(castle);
        }

        Projectile.DESTROY_PROJECTILES();

        castle = Instantiate(castles[level]);
        castle.transform.position = castlePos;

        Goal.goalMet = false;

        UpdateGUI();

        mode = GameMode.playing;

        FollowCam.SWITCH_VIEW(FollowCam.eView.both);
    }

    void UpdateGUI()
    {
        uitLevel.text = "Level: " + (level + 1) + " of " + levelMax;
        uitShots.text = "Shots: " + shotsTaken;
    }

    void Update()
    {
        UpdateGUI();

        if ((mode == GameMode.playing) && Goal.goalMet)
        {
            mode = GameMode.levelEnd;

            FollowCam.SWITCH_VIEW(FollowCam.eView.both);

            Invoke("NextLevel", 2f);
        }
    }

    void NextLevel()
    {
        level++;
        if (level == levelMax)
        {
            ShowGameOver(); 
        }
        else
        {
            StartLevel();
        }
    }

    void ShowGameOver()
    {
        mode = GameMode.gameOver;
        gameOverPanel.SetActive(true);
        playAgainButton.onClick.AddListener(RestartGame); 
    }

    void RestartGame()
    {
        gameOverPanel.SetActive(false); 
        level = 0;
        shotsTaken = 0;
        StartLevel();
    }

    static public void SHOT_FIRED()
    {
        S.shotsTaken++;
    }

    static public GameObject GET_CASTLE()
    {
        return S.castle;
    }
}
