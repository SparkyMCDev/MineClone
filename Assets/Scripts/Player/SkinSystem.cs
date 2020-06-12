using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinSystem : MonoBehaviour
{
    private Texture Skin;
    private string SkinFilePath = "PlayerSkin";
    private string DefaultPlayerSkinFilePath = "DefaultPlayerSkin";
    Renderer m_Renderer;
    private void Start()
    {
        m_Renderer = GetComponent<Renderer>();
        m_Renderer.material.SetTexture("_MainTex", getSkin());
    }

    private Texture getSkin()
    {
        Skin = Resources.Load<Texture>("textures/player/" + SkinFilePath);
        if (Skin == null)
        {
            return Resources.Load<Texture>("textures/player/" + DefaultPlayerSkinFilePath);
        }
        else { return Skin; }
    }
}
