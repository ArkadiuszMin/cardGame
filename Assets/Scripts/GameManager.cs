using UnityEngine;
using Gameplay;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Player player;
    public Player opponent;
    public GameBoard board;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InitializePlayers();
        InitializeBoard();
    }
    private void InitializePlayers(){
        player.Initialize(PlayerStatus.Me);
        opponent.Initialize(PlayerStatus.Opponent);
    }
    private void InitializeBoard(){
        board.Initilize();
    }
}

public enum PlayerStatus
{
    Me, Opponent
}