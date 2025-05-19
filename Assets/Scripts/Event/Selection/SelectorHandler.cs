using System;
using Event;
using Event.Selection.Animation;
using Gameplay.Cards;
using JetBrains.Annotations;
using UnityEngine;

public class SelectorHandler : MonoBehaviour
{ 
    [CanBeNull] public static SelectorHandler Instance { get; private set; }
    [CanBeNull] private Card _selectedCard;

    private void HandleSelect(Card card)
    {
        if (AreBothCardsAreOnBoard(card, _selectedCard) && AreDifferentPlayersCard(card, _selectedCard))
        {
            _selectedCard.Unselect();
            StartCoroutine(AttackAnimation.Execute(_selectedCard, card));
            _selectedCard.HasAttacked = true;
            _selectedCard = null;
        }
        else
        {
            HandleCardSelection(card);
        }
        
    }

    private void HandleCardSelection(Card card)
    {
        if (!card.CanBeSelected())
        {
            return;
        }
        
        if (_selectedCard == null)
        {
            _selectedCard = card;
            card.Select();
        }
        else if (card.Equals(_selectedCard))
        {
            _selectedCard.Unselect();
            _selectedCard = null;
        }
        else
        {
            _selectedCard.Unselect();
            card.Select();
            _selectedCard = card;
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

    private void Start()
    {
        GameEvents.CardEvents.cardClicked += HandleSelect;
        GameEvents.CardEvents.cardPlayed += OnCardPlayed;
    }

    private void OnCardPlayed(Card card)
    {
        if (_selectedCard != null && _selectedCard.Equals(card))
        {
            _selectedCard.Unselect();
            _selectedCard = null;
        }
    }
}
