﻿using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Gameplay.Cards;
using UI;
using UnityEngine;


namespace Event.Selection.Animation
{

    public static class AttackAnimation
    {
        public static float duration = 0.6f;

        public static IEnumerator Execute(Card attacker, Card target)
        {
            Vector3 targetPosition = target.transform.position;
            Vector3 originalPosition = attacker.transform.position;

            yield return AnimationHelper.MoveToPosition(attacker.transform, targetPosition, duration / 2);

            yield return new WaitForSeconds(0.1f);

            attacker.TakeDamage(target._attack);
            target.TakeDamage(attacker._attack);

            yield return AnimationHelper.MoveToPosition(attacker.transform, originalPosition, duration / 2);

            attacker.ExecuteActions();
            target.ExecuteActions();
        }

        public static IEnumerator Execute(Card attacker, PlayerStatusUI target)
        {
            Vector3 targetPosition = target.transform.position;
            Vector3 originalPosition = attacker.transform.position;

            yield return AnimationHelper.MoveToPosition(attacker.transform, targetPosition, duration / 2);

            yield return new WaitForSeconds(0.1f);

            target.OnDamage(attacker._attack);

            yield return AnimationHelper.MoveToPosition(attacker.transform, originalPosition, duration / 2);

            attacker.ExecuteActions();
            target.ExecuteActions();
        }
    }
}