using System.Collections.Generic;
using DG.Tweening;
using Gameplay;
using Gameplay.Cards;
using Interface;
using UnityEngine;

public class Graveyard : MonoBehaviour, ICardHandler
{
    private Player _owner;
    private List<Card> _cards;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void Initialize(Player owner)
    {
        _owner = owner;
        _cards = new List<Card>();
    }

    public void AddCard(Card card)
    {
        _cards.Add(card);
        CardStatusChanger.Change(card, CardStatus.InGraveyard, this);
        card.transform.DORotate(new Vector3(90, 90, 0), 0.4f);
    }

    public Vector3 GetPosition(Card card)
    {
        return Vector3.zero;
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
