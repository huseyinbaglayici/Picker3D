using System;
using UnityEngine;

namespace Scipts.Data.ValueObjects
{
    [Serializable]
    public struct InputData
    {
        public float HorizontalInputSpeed;
        public Vector2 ClampValues;
        public float ClampSpeed;
    }
}