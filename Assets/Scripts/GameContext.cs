
using System;
using UnityEngine;

public class GameContext : MonoBehaviour
{
    public static GameContext Instance { get; private set; }

    public PlayerStatus PlayersTurn { get; private set; }

    public void ChangeTurn()
    {
        PlayersTurn = GetNextPlayer(PlayersTurn);
    }

    private PlayerStatus GetNextPlayer(PlayerStatus playerStatus) => playerStatus switch
    {
        PlayerStatus.Me => PlayerStatus.Opponent,
        PlayerStatus.Opponent => PlayerStatus.Me,
        _ => throw new InvalidOperationException("huh?")
    };
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            PlayersTurn = PlayerStatus.Me;
        }
        else
        {
            Destroy(Instance);
        }
    }
}
