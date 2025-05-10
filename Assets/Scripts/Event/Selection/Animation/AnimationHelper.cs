using System.Collections;
using UnityEngine;

namespace Event.Selection.Animation
{
    public class AnimationHelper
    {
        public static IEnumerator MoveToPosition(Transform transform, Vector3 position, float duration)
        {
            float elapsed = 0;
            var originalPosition = transform.position;
            
            while (elapsed < duration / 2f)
            {
                transform.position = Vector3.Lerp(originalPosition, position, elapsed / (duration / 2f));
                elapsed += Time.deltaTime;
                yield return null;
            }
            transform.position = position;
        }
    }
}