using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KeyAction {
    MOVE_CAM_UP, MOVE_CAM_DOWN, MOVE_CAM_LEFT, MOVE_CAM_RIGHT,
    HERO_GROUP_1, HERO_GROUP_2, HERO_GROUP_3, HERO_GROUP_4,
    KEYCOUNT
}

public static class KeySetting
{
    public static Dictionary<KeyAction, KeyCode> keys = new Dictionary<KeyAction, KeyCode>();  
}


public class KeyManager : MonoBehaviour
{
    public static KeyManager Instance { private set; get; }

    KeyAction[] keyActions = new KeyAction[]    {KeyAction.MOVE_CAM_UP, KeyAction.MOVE_CAM_DOWN, KeyAction.MOVE_CAM_LEFT, KeyAction.MOVE_CAM_RIGHT,
                                                KeyAction.HERO_GROUP_1, KeyAction.HERO_GROUP_2, KeyAction.HERO_GROUP_3, KeyAction.HERO_GROUP_4 };

    KeyCode[] defaultKeyCodes = new KeyCode[]   {KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D,
                                                KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4};


    bool isKeySetting = false;
    int currentKey = -1;



    private void Awake() {
        Instance = this;

        SetKey();
    }


    void SetKey()
    {
        for (int i = 0; i < (int)KeyAction.KEYCOUNT; i++)
        {
            KeySetting.keys.Add(keyActions[i], defaultKeyCodes[i]);
        }
    }

    private void OnGUI() 
    {
        if (!isKeySetting)
            return;

        Event keyEvent = Event.current;

        if (keyEvent.isKey)
        {   
            KeySetting.keys[(KeyAction)currentKey] = keyEvent.keyCode;
            
            UIManager.Instance.SetText();
            currentKey = -1;
            isKeySetting = false;
        }    
    }


    public void ChangeKey(int num)
    {
        currentKey = num;
        isKeySetting = true;
        UIManager.Instance.SetFocus(num, true);
    }
}
