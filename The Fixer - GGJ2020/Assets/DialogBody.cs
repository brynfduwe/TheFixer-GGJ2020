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
    [SerializeField] GameObject m_bodyObj = null;

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
    BodyTalk[] selectedDialog = null;

    [SerializeField] GameObject m_dumpsterButton = null;
    [SerializeField] GameObject m_barrelButton = null;
    [SerializeField] GameObject m_dockButton = null;


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
        UpdateMenuOptions();

        UIManager.instance.NewSubtitle("", "");
        UIManager.instance.RaiseSubMenu(true, UIManager.SubMenus.HidingTheBody);
        foreach (var goon in m_goons)
        {
            goon.highlightSprite(false);
        }
    }

    void UpdateMenuOptions()
    {
        m_dumpsterButton.SetActive(false);
        m_barrelButton.SetActive(false);
        m_dockButton.SetActive(false);

        if (GameManager.instance.m_bodyProgress.knownOptions.Contains("Dumpster"))
        {
            m_dumpsterButton.SetActive(true);
        }
        if (GameManager.instance.m_bodyProgress.knownOptions.Contains("Barrel"))
        {
            m_barrelButton.SetActive(true);
        }
        if (GameManager.instance.m_bodyProgress.knownOptions.Contains("Dock") && GameManager.instance.m_bodyProgress.knownOptions.Contains("Cinderblock"))
        {
            m_dockButton.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_inDialog)
        {
            if (Input.GetKeyUp(KeyCode.Return))
            {
                doDialog();
            }
        }
    }

    public void doDialog()
    {
        m_dialogIter++;

        if (m_dialogIter >= 0)
        {
            if (m_dialogIter < selectedDialog.Length)
            {
                UIManager.instance.NewSubtitle(selectedDialog[m_dialogIter].name, selectedDialog[m_dialogIter].line);
                foreach (var goon in m_goons)
                {
                    if (goon.getName() == selectedDialog[m_dialogIter].name)
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

    public void bodyChoice(string _choice)
    {
        m_bodyObj.SetActive(false);

        GameManager.instance.TryBody(_choice);
        UIManager.instance.m_bodyMenuStartButton.SetActive(false);

        UIManager.instance.RaiseSubMenu(false, UIManager.SubMenus.GoonChoices);
   //     UIManager.instance.NewSubtitle("", "");

        if(_choice == "Barrel")
        {
            selectedDialog = bodyDialogData.Barrel;
        }
        if (_choice == "Dumpster")
        {
            selectedDialog = bodyDialogData.Dumpster;
        }
        if (_choice == "Dock")
        {
            selectedDialog = bodyDialogData.Dock;
        }

        m_inDialog = true;
        m_dialogIter = 0;
        UIManager.instance.NewSubtitle(selectedDialog[m_dialogIter].name, selectedDialog[m_dialogIter].line);
        foreach (var goon in m_goons)
        {
            if (goon.getName() == selectedDialog[m_dialogIter].name)
                goon.highlightSprite(true);
            else
                goon.highlightSprite(false);
        }
    }

    public void Back()
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
