using UnityEditor.Analytics;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private Transform pellets;
    [SerializeField] private Text gameOverText;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text livesText;

    private Pacman pacman;

    public int score = 0;
    public int lives = 3;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        NewGame();
    }

    private void Update()
    {
        // Always keep reference updated
        if (pacman == null)
        {
            pacman = FindAnyObjectByType<Pacman>();
        }

        if (lives <= 0 && Input.anyKeyDown)
        {
            NewGame();
        }
    }

    private void NewGame()
    {
        score = 0;
        lives = 3;

        UpdateUI();
        NewRound();
    }

    private void NewRound()
    {
        gameOverText.enabled = false;

        foreach (Transform pellet in pellets)
        {
            pellet.gameObject.SetActive(true);
        }

        ResetState();
    }

    private void ResetState()
    {
        if (pacman != null)
        {
            pacman.ResetState();
        }
    }

    public void PacmanEaten()
    {
        if (pacman != null)
        {
            pacman.DeathSequence();
        }

        lives--;
        UpdateUI();

        if (lives > 0)
        {
            Invoke(nameof(ResetState), 2f);
        }
        else
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        gameOverText.enabled = true;

        if (pacman != null)
        {
            pacman.gameObject.SetActive(false);
        }
    }

    public void PelletEaten(int points)
    {
        score += points;
        UpdateUI();
    }

    private void UpdateUI()
    {
        scoreText.text = score.ToString();
        livesText.text = "x" + lives;
    }



    public void PowerPelletEaten(PowerPellet pellet)
    {
        // Disabled for now
    }
}