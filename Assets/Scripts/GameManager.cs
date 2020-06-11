using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager Instance { get; set; }

    public static GameManager getInstance() { return Instance; }

    private void Awake()
    {
        Instance = this;
    }
}
