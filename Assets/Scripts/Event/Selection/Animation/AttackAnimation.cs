using System.Collections;
using System.Collections.Generic;
using Gameplay.Cards;
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

            float elapsed = 0;
            while (elapsed < duration / 2f)
            {
                attacker.transform.position = Vector3.Lerp(originalPosition, targetPosition, elapsed / (duration / 2f));
                elapsed += Time.deltaTime;
                yield return null;
            }
            attacker.transform.position = targetPosition;
            yield return new WaitForSeconds(0.1f);
            
            attacker.TakeDamage(target._attack);
            target.TakeDamage(attacker._attack);

            elapsed = 0;
            while (elapsed < duration / 2f)
            {
                attacker.transform.position = Vector3.Lerp(targetPosition, originalPosition, elapsed / (duration / 2f));
                elapsed += Time.deltaTime;
                yield return null;
            }
            attacker.transform.position = originalPosition;
        }
    }
}