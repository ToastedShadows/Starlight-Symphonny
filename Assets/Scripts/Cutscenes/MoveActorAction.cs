using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MoveActorAction : CutsceneAction
{
    [SerializeField] Character character;
    [SerializeField] List<Vector2> movePattterns;

    //public override IEnumerator Play()
    //{
        //foreach (var moveVec in movePattterns)
        //{
            //yield return character.Move(moveVec);
        //}
    //}
}
