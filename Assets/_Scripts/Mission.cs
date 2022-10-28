using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Mission {
    public string id;
    public string title;
    public List<string> objIds = new List<string>();
}
