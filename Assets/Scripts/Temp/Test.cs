//================================
//===Author: easy
//===Email: gopiny@live.com
//===Date: 2017-11-13 09:45
//================================

using System;
using UnityEngine;
using Asobimo.Pachinko;
using Easy.FrameUnity.ESNetwork;

public class Test : MonoBehaviour
{
    private string _id = "1";
    private string _stateType = "1";
    private string _type = "1";

    private void OnGUI()
    {
        GUIStyle gs = GUI.skin.textArea;
        gs.fontSize = 30;
        gs.alignment = TextAnchor.MiddleCenter;

        GUI.Label(new Rect(300, 0, 100, 60), "Id:", gs);
        _id = GUI.TextField(new Rect(400, 0, 100, 60), _id, 10, gs);

        GUI.Label(new Rect(520, 0, 100, 60), "State:", gs);
        _stateType = GUI.TextField(new Rect(620, 0, 100, 60), _stateType, 10, gs);

        GUI.Label(new Rect(300, 65, 100, 60), "Type:", gs);
        _type = GUI.TextField(new Rect(400, 65, 100, 60), _type, 10, gs);

        if(GUI.Button(new Rect(520, 65, 200, 60), "Change", gs))
        {
            this.TestChangePachinkoState();
        }
    }

    private void TestChangePachinkoState()
    {
        var data = new PachinkoStateDataCast();
        data.Id = int.Parse(_id);
        data.StateType = (PachinkoStateType)int.Parse(_stateType);
        data.Type = (PachinkoType)int.Parse(_type);
        PanelMain.Inst.ChangePachinkoState(data);
    }
}
