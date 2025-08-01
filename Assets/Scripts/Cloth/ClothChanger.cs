using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cloth;

namespace Cloth
{
    public class ClothChanger : MonoBehaviour
    {
        public SkinnedMeshRenderer[] meshs;

        public Texture2D texture;

        public string shaderIdName = "_EmissionMap";

        private Texture2D defaultTexture;

        public ClothSetup currentClothSetup;

        private void Awake()
        {
            defaultTexture = (Texture2D)meshs[0].sharedMaterials[0].GetTexture(shaderIdName);
        }

        public void ChangeTexture(ClothSetup setup)
        {
            currentClothSetup = setup;

            foreach (var mesh in meshs)
            {
                mesh.materials[0].SetTexture(shaderIdName, setup.texture);
            }
        }

        public void ResetTexture()
        {
            currentClothSetup = new ClothSetup { clothType = ClothType.Base };

            foreach (var mesh in meshs)
            {
                mesh.materials[0].SetTexture (shaderIdName, defaultTexture);
            }

        }
    }

}