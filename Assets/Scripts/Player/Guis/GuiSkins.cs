using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Serialization;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GuiSkins : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonClick()
    {
        
        string username = transform.parent.Find("UsernameField").Find("Text").GetComponent<Text>().text;
        
        if (username.Length > 16 || username.Length < 3)
        {
            transform.parent.Find("UsernameField").GetComponent<InputField>().text = "";
            transform.parent.Find("UsernameField").GetComponent<InputField>().placeholder.GetComponent<Text>().text = "Invalid username";
            
            return;
        }
        
        HttpClient http = new HttpClient();
        
        http.BaseAddress = new Uri("https://api.mojang.com/users/profiles/minecraft/");
        http.DefaultRequestHeaders.Clear();
        http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var response = http.GetAsync(username).Result;

        response.EnsureSuccessStatusCode();

        var resp = JsonUtility.FromJson<MinecraftPlayerNameUuid>(response.Content.ReadAsStringAsync().Result);
        if (resp.id == null)
        {
            transform.parent.Find("UsernameField").GetComponent<InputField>().text = "";
            transform.parent.Find("UsernameField").GetComponent<InputField>().placeholder.GetComponent<Text>().text = "Invalid username";
            
            return;
        }
        
        http.BaseAddress = new Uri("https://sessionserver.mojang.com/session/minecraft/profile/");
        http.DefaultRequestHeaders.Clear();
        http.CancelPendingRequests();

        var response2 = http.GetAsync(resp.id).Result;

        response2.EnsureSuccessStatusCode();

        var resp2 = response2.Content.ReadAsStringAsync().Result;

        resp2 = resp2.Substring(resp2.IndexOf("\"value\"", StringComparison.Ordinal));

        resp2 = resp2.Substring(0, resp2.Length - 9);

        resp2 = resp2.Substring(11);

        byte[] decodedBytes = Convert.FromBase64String(resp2);
        string decodedResp = Encoding.UTF8.GetString(decodedBytes);

        decodedResp = decodedResp.Substring(decodedResp.IndexOf("\"url\"", StringComparison.Ordinal));

        decodedResp = decodedResp.Substring(0, decodedResp.Length - 10);

        decodedResp = decodedResp.Substring(10);

        StartCoroutine(TextureStuff(decodedResp));
    }

    IEnumerator TextureStuff(string uri)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(uri);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Texture texture = ((DownloadHandlerTexture) www.downloadHandler).texture;
            
            GameObject.FindWithTag("Player").GetComponent<MeshRenderer>().material.SetTexture("_MainTex", texture);
            transform.parent.Find("RawImage").GetComponent<RawImage>().texture = texture;
        }
    }
}
