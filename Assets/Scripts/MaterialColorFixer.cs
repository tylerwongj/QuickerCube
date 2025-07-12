using UnityEngine;

namespace MarioWorld
{
    public class MaterialColorFixer : MonoBehaviour
    {
        [Header("Materials to Fix")]
        public Material brownGroundMaterial;
        public Material brickMaterial;
        public Material greenPipeMaterial;
        public Material yellowQuestionMaterial;
        public Material marioRedMaterial;
        public Material goldCoinMaterial;
        
        [Header("Auto-find Materials")]
        public bool autoFindMaterials = true;
        
        void Start()
        {
            if (autoFindMaterials)
            {
                FindMaterials();
            }
            
            FixAllColors();
        }
        
        void FindMaterials()
        {
            // Find materials by name
            brownGroundMaterial = Resources.Load<Material>("Materials/BrownGround");
            brickMaterial = Resources.Load<Material>("Materials/BrickMaterial");
            greenPipeMaterial = Resources.Load<Material>("Materials/GreenPipe");
            yellowQuestionMaterial = Resources.Load<Material>("Materials/YellowQuestion");
            marioRedMaterial = Resources.Load<Material>("Materials/MarioRed");
            goldCoinMaterial = Resources.Load<Material>("Materials/GoldCoin");
            
            // Alternative: Find by searching in Assets
            if (brownGroundMaterial == null)
            {
                Material[] allMaterials = Resources.FindObjectsOfTypeAll<Material>();
                foreach (Material mat in allMaterials)
                {
                    if (mat.name == "BrownGround") brownGroundMaterial = mat;
                    else if (mat.name == "BrickMaterial") brickMaterial = mat;
                    else if (mat.name == "GreenPipe") greenPipeMaterial = mat;
                    else if (mat.name == "YellowQuestion") yellowQuestionMaterial = mat;
                    else if (mat.name == "MarioRed") marioRedMaterial = mat;
                    else if (mat.name == "GoldCoin") goldCoinMaterial = mat;
                }
            }
        }
        
        void FixAllColors()
        {
            // Fix Brown Ground - dark brown
            if (brownGroundMaterial != null)
            {
                SetMaterialColor(brownGroundMaterial, new Color(0.4f, 0.2f, 0.1f, 1f));
                Debug.Log("Fixed Brown Ground color");
            }
            
            // Fix Brick Material - orange/brick red
            if (brickMaterial != null)
            {
                SetMaterialColor(brickMaterial, new Color(0.8f, 0.4f, 0.2f, 1f));
                Debug.Log("Fixed Brick Material color");
            }
            
            // Fix Green Pipe - bright green
            if (greenPipeMaterial != null)
            {
                SetMaterialColor(greenPipeMaterial, new Color(0.0f, 0.8f, 0.2f, 1f));
                Debug.Log("Fixed Green Pipe color");
            }
            
            // Fix Yellow Question Block - golden yellow
            if (yellowQuestionMaterial != null)
            {
                SetMaterialColor(yellowQuestionMaterial, new Color(1.0f, 0.8f, 0.0f, 1f));
                Debug.Log("Fixed Yellow Question color");
            }
            
            // Fix Mario Red - bright red
            if (marioRedMaterial != null)
            {
                SetMaterialColor(marioRedMaterial, new Color(0.8f, 0.1f, 0.1f, 1f));
                Debug.Log("Fixed Mario Red color");
            }
            
            // Fix Gold Coin - shiny gold
            if (goldCoinMaterial != null)
            {
                SetMaterialColor(goldCoinMaterial, new Color(1.0f, 0.8f, 0.0f, 1f));
                goldCoinMaterial.SetFloat("_Metallic", 0.8f);
                goldCoinMaterial.SetFloat("_Smoothness", 0.9f);
                Debug.Log("Fixed Gold Coin color");
            }
        }
        
        void SetMaterialColor(Material material, Color color)
        {
            // Try different property names that might work
            if (material.HasProperty("_BaseColor"))
                material.SetColor("_BaseColor", color);
            if (material.HasProperty("_Color"))
                material.SetColor("_Color", color);
            if (material.HasProperty("_MainColor"))
                material.SetColor("_MainColor", color);
            if (material.HasProperty("_Albedo"))
                material.SetColor("_Albedo", color);
        }
        
        [ContextMenu("Fix Colors Again")]
        public void FixColorsManually()
        {
            FindMaterials();
            FixAllColors();
        }
    }
}