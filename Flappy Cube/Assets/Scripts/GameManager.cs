using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GameManager : MonoBehaviour
{
    enum UIPanelID
    {
        None = 0,
        Startscreen = 1,
        EnterNamescreen = 2,
        Endscreen = 3

    }
    public LevelScriptableObject level;
    PipeSpawner pipeSpawner;
    FlappyController flapper;
    int score = 0;
    bool isGameStarted = false;
    bool isGameEnded = true;


    public Text scoreText;
    public Text highscoreText;
    public InputField nameInput;
    public GameObject startScreen;
    public GameObject endScreen;
    public GameObject enterNameScreen;

    public Button navToMenu;
    public Button reset;


    void Start()
    {
        if (LevelLoader.levelToLoad != null)
        {
            level = LevelLoader.levelToLoad;
        }
        Init(level);

        navToMenu.onClick.AddListener(() => LevelLoader.LoadMainMenu());
        reset.onClick.AddListener(Reset);
    }

    void Init(LevelScriptableObject level)
    {
        ShowUI(UIPanelID.Startscreen);
        nameInput.onEndEdit.AddListener((s) => CreateNewHighscoreEntry());

        flapper = Instantiate<FlappyController>(level.flappy);
        flapper.onCollideWithPipe += KillPlayer;
        flapper.onOutOfBounds += KillPlayer;
        flapper.onClearGap += AddToScore;
        flapper.GetComponent<Rigidbody>().isKinematic = true;
        pipeSpawner = this.GetComponent<PipeSpawner>();
        pipeSpawner.isSpawning = false;
        flapper.acceptInput = false;
        isGameEnded = false;

        score = 0;
        scoreText.text = score.ToString();
    }

    void StartGame()
    {
        ShowUI(UIPanelID.None);
        flapper.GetComponent<Rigidbody>().isKinematic = false;
        isGameStarted = true;
        flapper.acceptInput = true;
        pipeSpawner.isSpawning = true;

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isGameStarted == false && isGameEnded == false)
        {
            StartGame();
        }

        if (Input.GetKeyDown("r"))
        {
            if (isGameEnded == false)
            {
                Reset();
            }
        }
    }

    void KillPlayer()
    {
        if (isGameEnded == false)
        {
            flapper.acceptInput = false;
            pipeSpawner.isSpawning = false;
            isGameStarted = false;
            isGameEnded = true;

            var spawnedPipes = GameObject.FindGameObjectsWithTag("Pipe");
            foreach (var pipe in spawnedPipes)
            {
                Destroy(pipe.gameObject);
            }

            if (level.highscores.IsPlayerNotANoob(score))
            {
                ShowUI(UIPanelID.EnterNamescreen);
            }
            else
            {
                ShowUI(UIPanelID.Endscreen);
                PopulateEndscreen();

            }
        }

    }

    void AddToScore()
    {
        if (isGameStarted && isGameEnded == false)
        {
            score++;
            scoreText.text = score.ToString();
        }
    }

    void Reset()
    {
        Destroy(flapper.gameObject);
        var spawnedPipes = GameObject.FindGameObjectsWithTag("Pipe");
        foreach (var pipe in spawnedPipes)
        {
            Destroy(pipe.gameObject);
        }
        isGameStarted = false;
        Init(level);
    }

    void ShowUI(UIPanelID panelID)
    {
        startScreen.gameObject.SetActive(panelID == UIPanelID.Startscreen);
        enterNameScreen.gameObject.SetActive(panelID == UIPanelID.EnterNamescreen);
        endScreen.gameObject.SetActive(panelID == UIPanelID.Endscreen);
    }

    void PopulateEndscreen()
    {
        highscoreText.gameObject.SetActive(true);
        foreach (var entry in level.highscores.entries)
        {
            Text generatedText = Instantiate<Text>(highscoreText, highscoreText.transform.parent);
            generatedText.GetComponentInChildren<Text>().text = $"{entry.playerName}         {entry.playerScore}";
        }
        highscoreText.gameObject.SetActive(false);
    }

    void CreateNewHighscoreEntry()
    {
        Highscores.HighscoreEntry entry = new Highscores.HighscoreEntry();
        entry.playerName = nameInput.text;
        entry.playerScore = score;

        level.highscores.entries.Add(entry);
        level.highscores.entries = level.highscores.entries.OrderByDescending((e) => e.playerScore).ToList();
        ShowUI(UIPanelID.Endscreen);
        PopulateEndscreen();

    }
}
