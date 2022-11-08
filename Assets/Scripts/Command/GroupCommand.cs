using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupCommand<T> : Command
{
    List<T> group = new List<T>();


    public override void Execute()
    {
        Selected();
    }

    public override void Cancle()
    {
        CancleSelect();
    }

    public void SetGroup(List<T> groupList)
    {
        group.AddRange(groupList);
    }


    void Selected()
    {
        Debug.Log("GroupCmd Selected()");
    }

    void CancleSelect()
    {
        Debug.Log("GroupCmd CancleSelect()");
    }
}
