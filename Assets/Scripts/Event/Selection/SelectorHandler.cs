using System;
using Event;
using Event.Selection.Animation;
using Gameplay.Cards;
using JetBrains.Annotations;
using UI;
using UnityEngine;
using UnityEngine.Serialization;

public class SelectorHandler : MonoBehaviour
{ 
    [CanBeNull] public static SelectorHandler Instance { get; private set; }
    [CanBeNull] public Card SelectedCard;

    private void HandleSelect(Card card)
    {
        if (AreBothCardsAreOnBoard(card, SelectedCard) && AreDifferentPlayersCard(card, SelectedCard))
        {
            SelectedCard.Unselect();
            StartCoroutine(AttackAnimation.Execute(SelectedCard, card));
            SelectedCard.CanAttack = false;
            SelectedCard = null;
        }
        else
        {
            HandleCardSelection(card);
        }
        
    }

    private void HandleCardSelection(Card card)
    {
        var curTurn = GameContext.Instance.PlayersTurn;
        var cardsPlayer = card.GetOwnerStatus();

        if (curTurn != cardsPlayer)
        {
            return;
        }
        
        if (!card.CanBeSelected())
        {
            return;
        }
        
        if (SelectedCard == null)
        {
            SelectedCard = card;
            card.Select();
        }
        else if (card.Equals(SelectedCard))
        {
            SelectedCard.Unselect();
            SelectedCard = null;
        }
        else
        {
            SelectedCard.Unselect();
            card.Select();
            SelectedCard = card;
        }
    }

    private bool AreBothCardsAreOnBoard(Card card1, Card card2)
    {
        return card1.Status == CardStatus.InGame && card2?.Status == CardStatus.InGame;
    }

    private bool AreDifferentPlayersCard(Card card1, Card card2)
    {
        return card1.GetOwnerStatus() != card2?.GetOwnerStatus();
    }
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }

    private void HandlePlayerSelect(PlayerStatusUI playerStatusUI)
    {
        if (SelectedCard != null && SelectedCard.GetOwnerStatus() != playerStatusUI.getPlayerStatus())
        {
            SelectedCard.Unselect();
            StartCoroutine(AttackAnimation.Execute(SelectedCard, playerStatusUI));
            SelectedCard.CanAttack = false;
            SelectedCard = null;
        }
    }

    private void Start()
    {
        GameEvents.CardEvents.cardClicked += HandleSelect;
        GameEvents.CardEvents.cardPlayed += OnCardPlayed;
        GameEvents.PlayerEvents.newTurnStarted += OnNewTurn;
        GameEvents.PlayerEvents.playerClicked += HandlePlayerSelect;
    }

    private void OnNewTurn(PlayerStatus status)
    {
        SelectedCard = null;
    }

    private void OnCardPlayed(Card card)
    {
        if (SelectedCard != null && SelectedCard.Equals(card))
        {
            SelectedCard.Unselect();
            SelectedCard = null;
        }
    }
}
