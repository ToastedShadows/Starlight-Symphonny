using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameEnums
{
    public enum BattleState 
    {
        Start,
        PlayerAction,
        PlayerMove,
        EnemyMove,
        Busy
    }

    public enum Stat
    {
        MAttack,
        RAttack,
        MDefense,
        RDefense,
        MaxHp,
        MaxMana
    }
}
