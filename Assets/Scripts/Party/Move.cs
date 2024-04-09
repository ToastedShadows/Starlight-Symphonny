using UnityEngine;

public class Move
{
    public MoveBase Base { get; set; }
    public int PP { get; set; }

    public Move(MoveBase pBase, int pp)
    {
        Base = pBase;
        PP = pp; // Fixed assignment
    }
}