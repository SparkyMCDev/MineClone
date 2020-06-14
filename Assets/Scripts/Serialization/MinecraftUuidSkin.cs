using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Serialization
{
    [Serializable]
    public class MinecraftUuidSkin
    {
        public string id;
        public string name;
        public MinecraftUuidSkin properties;
    }

    [Serializable]
    public class MinecraftUuidSkinList
    {
        public string list;
    }
}