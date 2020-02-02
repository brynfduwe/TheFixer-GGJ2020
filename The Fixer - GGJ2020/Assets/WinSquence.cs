using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;


public class WinSquence : MonoBehaviour
{
    [SerializeField] PlayerMovement m_playerMovement = null;
    [SerializeField] GameObject[] m_disableAtEnd = null;
    public static WinSquence instance = null;

    bool m_inDialog = false;
    int m_dialogIter = -1;
    [SerializeField] string m_theBodyFilePath = null;

    [System.Serializable]
    public class EndingData
    {
        public EndingDialog[] Bad;
        public EndingDialog[] Good;
        public EndingDialog[] Perfect;
    }
    [System.Serializable]
    public class EndingDialog
    {
        public string name;
        public string line;
    }
    EndingData m_endingData = null;
    EndingDialog[] m_playersEnding = null;


    void Awake()
    {
        //Load default egg names
        string filePath = Path.Combine(Application.streamingAssetsPath, m_theBodyFilePath);
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            m_endingData = JsonUtility.FromJson<EndingData>(json);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void EndGame(int _score)
    {
        if(_score <= 1)
        {
            m_playersEnding = m_endingData.Bad;
        }
        if (_score == 2)
        {
            m_playersEnding = m_endingData.Good;
        }
        if (_score == 3)
        {
            m_playersEnding = m_endingData.Perfect;
        }

        m_playerMovement.canMove(false);
        UIManager.instance.NewSubtitle("", "");
        UIManager.instance.RaiseSubMenu(false, UIManager.SubMenus.none);
        UIManager.instance.FadeForeground(1F, false);

        foreach(var obj in m_disableAtEnd)
        {
            obj.SetActive(false);
        }

        StartCoroutine(delayEndSubs());
    }

    IEnumerator delayEndSubs()
    {
        yield return new WaitForSeconds(3);  
        m_dialogIter = 0;
        UIManager.instance.NewSubtitle(m_playersEnding[m_dialogIter].name, m_playersEnding[m_dialogIter].line);
        yield return new WaitForSeconds(1);
        m_inDialog = true;
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

    void doDialog()
    {
        m_dialogIter++;
        if (m_dialogIter >= 0)
        {
            if (m_dialogIter < m_playersEnding.Length)
            {
                UIManager.instance.NewSubtitle(m_playersEnding[m_dialogIter].name, m_playersEnding[m_dialogIter].line);
            }
            else
            {
                UIManager.instance.NewSubtitle("", "");
                m_inDialog = false;

                StartCoroutine(EndAndGoToMenu());
            }
        }
    }

    IEnumerator EndAndGoToMenu()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Menu");
    }

}
