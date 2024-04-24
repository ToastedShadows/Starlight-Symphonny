using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialog
{
    [SerializeField] private List<string> lines;

    public List<string> Lines
    {
        get { return lines; }
        set { lines = value; } // Add a setter to allow setting the Lines list
    }
}