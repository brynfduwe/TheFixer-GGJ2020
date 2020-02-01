using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DialogAlibi : MonoBehaviour
{
    bool m_inDialog = false;
    int m_dialogIter = 0;
    int m_alibiSectionIter = 0;
    [SerializeField] string m_alibiFilePath = null;
    [SerializeField] Goon[] m_goons = null;
    PlayerMovement m_playerMove = null;

    [System.Serializable]
    public class AlibiData
    {
        public Alibi[] alibi_pt1;
        public Alibi[] alibi_pt2;
        public Alibi[] alibi_pt3;
    }
    [System.Serializable]
    public class Alibi
    {
        public string name;
        public string line;
    }
    AlibiData m_alibiData = null;
    Alibi[] m_currentAlibiPart = null;

    void Awake()
    {
        //Load default egg names
        string filePath = Path.Combine(Application.streamingAssetsPath, m_alibiFilePath);
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            m_alibiData = JsonUtility.FromJson<AlibiData>(json);
        }
        m_currentAlibiPart = m_alibiData.alibi_pt1;

        //foreach(Alibi albi in m_alibiData.alibi_pt1)
        //{
        //    Debug.Log("1 - " + albi.name + ": " + albi.line);
        //}
        //foreach (Alibi albi in m_alibiData.alibi_pt2)
        //{
        //    Debug.Log("2 - " + albi.name + ": " + albi.line);
        //}
        //foreach (Alibi albi in m_alibiData.alibi_pt3)
        //{
        //    Debug.Log("3 - " + albi.name + ": " + albi.line);
        //}
    }

    public void Init()
    {
        m_inDialog = true;
        m_dialogIter = 0;
        m_alibiSectionIter = 0;
        m_currentAlibiPart = m_alibiData.alibi_pt1;
        UIManager.instance.NewSubtitle(m_currentAlibiPart[m_dialogIter].name, m_currentAlibiPart[m_dialogIter].line);
        UIManager.instance.RaiseSubMenu(true, UIManager.SubMenus.AlibiGame);
        foreach (var goon in m_goons)
        {
            if (goon.getName() == m_currentAlibiPart[m_dialogIter].name)
                goon.highlightSprite(true);
            else
                goon.highlightSprite(false);
        }
    }

    public void alibiChoice(bool _accept)
    {
        if (m_inDialog)
        {
            if (_accept) //user wants to go with that choice
            {
                m_alibiSectionIter++;
                m_dialogIter = 0;

                switch (m_alibiSectionIter)
                {
                    case 0:
                        m_currentAlibiPart = m_alibiData.alibi_pt1;
                        break;
                    case 1:
                        m_currentAlibiPart = m_alibiData.alibi_pt2;
                        break;
                    case 2:
                        m_currentAlibiPart = m_alibiData.alibi_pt3;
                        break;
                    case 3:
                        End();
                        return;
                        break;
                }
            }
            else //user wants to hear more ideas
            {
                m_dialogIter++;
                if (m_dialogIter >= m_currentAlibiPart.Length)
                    m_dialogIter = 0;
            }

            UIManager.instance.NewSubtitle(m_currentAlibiPart[m_dialogIter].name, m_currentAlibiPart[m_dialogIter].line);
            foreach (var goon in m_goons)
            {
                if (goon.getName() == m_currentAlibiPart[m_dialogIter].name)
                    goon.highlightSprite(true);
                else
                    goon.highlightSprite(false);
            }
        }
    }

    public void End()
    {
        UIManager.instance.NewSubtitle("", "");
        UIManager.instance.RaiseSubMenu(true, UIManager.SubMenus.GoonChoices);
        m_inDialog = false;
        foreach (var goon in m_goons)
        {
            goon.highlightSprite(false);
        } 
    }
}
