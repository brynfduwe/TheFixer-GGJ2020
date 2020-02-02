using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DialogTheStory : MonoBehaviour
{
    bool m_inDialog = false;
    int m_dialogIter = -1;
    [SerializeField] string m_theStoryFilePath = null;
    [SerializeField] Goon[] m_goons = null;

    [System.Serializable]
    public class DialogData
    {
        public Dialog[] items;
    }
    [System.Serializable]
    public class Dialog
    {
        public string name;
        public string line;
    }

    DialogData dialogdata = null;

    void Awake()
    {
        //Load default egg names
        string filePath = Path.Combine(Application.streamingAssetsPath, m_theStoryFilePath);
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            dialogdata = JsonUtility.FromJson<DialogData>(json);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(m_inDialog)
        {
            if(Input.GetKeyUp(KeyCode.Return))
            {
                doDialog();
            }
        }
    }

    public void Init()
    {
        if (!m_inDialog)
        {
            m_inDialog = true;
            m_dialogIter = -1;       
            doDialog();
        }
    }

    public void doDialog()
    {
        m_inDialog = true;
        m_dialogIter++;

        if (m_dialogIter >= 0)
        {
            if (m_dialogIter < dialogdata.items.Length)
            {
                UIManager.instance.NewSubtitle(dialogdata.items[m_dialogIter].name, dialogdata.items[m_dialogIter].line);

                foreach (var goon in m_goons)
                {
                    if (goon.getName() == dialogdata.items[m_dialogIter].name)
                        goon.highlightSprite(true);
                    else
                        goon.highlightSprite(false);
                }
            }
            else
            {
                UIManager.instance.NewSubtitle("", "");
                m_inDialog = false;
                UIManager.instance.RaiseSubMenu(true, UIManager.SubMenus.GoonChoices);

                foreach (var goon in m_goons)
                {
                    goon.highlightSprite(false);
                }
            }
        }
    }
}
