using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class DialogBody : MonoBehaviour
{
    bool m_inDialog = false;
    int m_dialogIter = -1;
    [SerializeField] string m_theBodyFilePath = null;
    [SerializeField] Goon[] m_goons = null;

    [System.Serializable]
    public class BodyTalkData
    {
        public BodyTalk[] Dumpster;
        public BodyTalk[] Barrel;
        public BodyTalk[] Dock;
    }
    [System.Serializable]
    public class BodyTalk
    {
        public string name;
        public string line;
    }
    BodyTalkData bodyDialogData = null;

    void Awake()
    {
        //Load default egg names
        string filePath = Path.Combine(Application.streamingAssetsPath, m_theBodyFilePath);
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            bodyDialogData = JsonUtility.FromJson<BodyTalkData>(json);
        }
    }

    public void Init()
    {
        UIManager.instance.NewSubtitle("", "");
        UIManager.instance.RaiseSubMenu(true, UIManager.SubMenus.HidingTheBody);
        foreach (var goon in m_goons)
        {
            goon.highlightSprite(false);
        }
    }

    void UpdateMenu()
    {
        
    }
}
