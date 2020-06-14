using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings
{
    public KeyCode KeyForward;
    public KeyCode KeyBackward;
    public KeyCode KeyLeft;
    public KeyCode KeyRight;
    public KeyCode KeyJump;
    public KeyCode KeySneak;
    public KeyCode KeySprint;
    public KeyCode KeyBlockBreak;
    public KeyCode KeyBlockPlace;

    public int RenderDistance;
    public int Fov;

    public Settings(KeyCode keyForward, KeyCode keyBackward, KeyCode keyLeft, KeyCode keyRight, KeyCode keyJump, KeyCode keySneak, KeyCode keySprint, KeyCode keyBlockBreak, KeyCode keyBlockPlace, int renderDistance, int fov)
    {
        KeyForward = keyForward;
        KeyBackward = keyBackward;
        KeyLeft = keyLeft;
        KeyRight = keyRight;
        KeyJump = keyJump;
        KeySneak = keySneak;
        KeySprint = keySprint;
        KeyBlockBreak = keyBlockBreak;
        KeyBlockPlace = keyBlockPlace;
    }
}
