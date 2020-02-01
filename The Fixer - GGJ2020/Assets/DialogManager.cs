using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DialogManager : MonoBehaviour
{
    bool m_inDialog = false;
    int m_dialogIter = -1;
    [SerializeField] string m_dialogFile = null;
    List<string> m_dialogSpeaker = new List<string>();
    List<string> m_dialogLine = new List<string>();
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
        string filePath = Path.Combine(Application.streamingAssetsPath, m_dialogFile);
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
            if(Input.GetKeyUp(KeyCode.E))
            {
                doDialog();
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        doDialog();
    }

    public void doDialog()
    {
        m_inDialog = true;
        m_dialogIter++;
        UIManager.instance.NewSubtitle(dialogdata.items[m_dialogIter].name, dialogdata.items[m_dialogIter].line);
        foreach(var goon in m_goons)
        {
            if (goon.getName() == dialogdata.items[m_dialogIter].name)
                goon.highlightGoon(true);
            else
                goon.highlightGoon(false);
        }
    }
}
