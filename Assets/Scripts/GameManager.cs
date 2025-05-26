using System.Collections;
using Event;
using UnityEngine;
using Gameplay;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Player player;
    public Player opponent;
    public GameBoard board;
    public Canvas gameOverScreen;
    public Canvas gameStartScreen;

    private void Awake()
    {
        Instance = this;
    }

    public void StartGame()
    {
        StartCoroutine(InitializeGame());
        gameStartScreen.gameObject.SetActive(false);
    }
    private void InitializePlayers()
    {
        player.Initialize(PlayerStatus.Me);
        opponent.Initialize(PlayerStatus.Opponent);
    }
    private void InitializeBoard()
    {
        board.Initilize();
    }

    private IEnumerator InitializeGame()
    {
        InitializePlayers();
        InitializeBoard();
        yield return new WaitForSeconds(0.4f);

        for (int i = 0; i < 3; i++)
        {
            player.DrawCard();
            opponent.DrawCard();
            yield return new WaitForSeconds(0.2f);
        }

        yield return new WaitForSeconds(0.2f);
        GameEvents.PlayerEvents.newTurnStarted.Invoke(PlayerStatus.Me);
    }

    public void EndGame(PlayerStatus winner)
    {
        gameOverScreen.gameObject.SetActive(true);
        player.endGame();
        opponent.endGame();
        player.enabled = false;
        opponent.enabled = false;
        board.enabled = false;
    }
}

public enum PlayerStatus
{
    Me, Opponent
}