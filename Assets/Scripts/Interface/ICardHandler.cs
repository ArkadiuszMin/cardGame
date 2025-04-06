using Gameplay;
using UnityEngine;

namespace Interface
{
    public interface ICardHandler
    {
        public Vector3 GetPosition(Card card);
        public Transform GetTransform();
    }
}
