using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsManager : MonoBehaviour
{

    public Settings settings;

    private static ControlsManager Instance { get; set; }

    public static ControlsManager getInstance() { return Instance; }
    private void Awake()
    {
        DontDestroyOnLoad(this);

        Instance = this;

        KeyCode KeyForward;
        KeyCode KeyBackward;
        KeyCode KeyLeft;
        KeyCode KeyRight;
        KeyCode KeyJump;
        KeyCode KeySneak;
        KeyCode KeySprint;
        KeyCode KeyBlockBreak;
        KeyCode KeyBlockPlace;
        int RenderDistance;
        int Fov;

        Enum.TryParse(PlayerPrefs.GetString("KeyForward", "W"), out KeyForward);
        Enum.TryParse(PlayerPrefs.GetString("KeyBackward", "S"), out KeyBackward);
        Enum.TryParse(PlayerPrefs.GetString("KeyLeft", "A"), out KeyLeft);
        Enum.TryParse(PlayerPrefs.GetString("KeyRight", "D"), out KeyRight);
        Enum.TryParse(PlayerPrefs.GetString("KeyJump", "Space"), out KeyJump);
        Enum.TryParse(PlayerPrefs.GetString("KeySneak", "LeftShift"), out KeySneak);
        Enum.TryParse(PlayerPrefs.GetString("KeySprint", "LeftControl"), out KeySprint);
        Enum.TryParse(PlayerPrefs.GetString("KeyBlockBreak", "Mouse0"), out KeyBlockBreak);
        Enum.TryParse(PlayerPrefs.GetString("KeyBlockPlace", "Mouse1"), out KeyBlockPlace);

        RenderDistance = PlayerPrefs.GetInt("RenderDistance", 10);
        Fov = PlayerPrefs.GetInt("Fov", 60);

        settings = new Settings(KeyForward, KeyBackward, KeyLeft, KeyRight, KeyJump, KeySneak, KeySprint, KeyBlockBreak, KeyBlockPlace, RenderDistance, Fov);
    }
}
