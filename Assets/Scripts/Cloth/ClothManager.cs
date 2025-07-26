using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;

namespace Cloth
{
    public enum ClothType
    {
        Base,
        SPEED,
        Strong
    }

    public class ClothManager : Singleton<ClothManager>
    {
        public List<ClothSetup> clothSetups;

        public ClothSetup GetSetupByType(ClothType cloth)
        {
            ClothSetup target = new ClothSetup();

            foreach(ClothSetup c in clothSetups)
            {
                if(c.clothType == cloth)
                {
                    target = c;
                    break;
                }
            }

            return target;
        }
    }

    [System.Serializable]
    public class ClothSetup
    {
        public ClothType clothType;
        public Texture2D texture;
    }
}
