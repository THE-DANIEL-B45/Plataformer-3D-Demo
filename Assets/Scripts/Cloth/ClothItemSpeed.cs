using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cloth
{
    public class ClothItemSpeed : ClothItemBase
    {
        public float targetSpeed = 50;

        public override void Collect()
        {
            base.Collect();

            Player.Instance.ChangeSpeed(targetSpeed, duration);
        }
    }
}

