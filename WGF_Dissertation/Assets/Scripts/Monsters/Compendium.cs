using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Compendium : MonoBehaviour
{
    public GameObject comp;
    public static bool running;
    public Text desc;
    public Text mName;
    public int index;
    //Dictionary<int, string> monNames = new Dictionary<int, string>();
    Dictionary<int, string> monDescr = new Dictionary<int, string>();
    // Start is called before the first frame update
    void Start()
    {
        string descPath = "Assets/Resources/MonDesc.txt";
        index = 1;
        ReadFileToDic(descPath);
    }

    // Update is called once per frame
    void Update()
    {
        Comp();
        ChangeMonInfo(index);


    }




    public void Comp()
    {

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (running == false)
            {
                OpenComp();


            }
            else if (running == true)
            {
                CloseComp();
            }

        }

    }

    void OpenComp()
    {
        comp.SetActive(true);
        Time.timeScale = 0f;
        running = true;
    }
    public void CloseComp()
    {
        comp.SetActive(false);
        Time.timeScale = 1f;
        running = false;
    }

    public void ChangeMonInfo(int nIndex)
    {
        if (running)
        {
            switch (nIndex)
            {
                case 1:
                    mName.text = "Chimera";
                    desc.text = monDescr[1];
                    break;
                case 2:
                    mName.text = "Harpy";
                    desc.text = monDescr[2];
                    break;
                case 3:
                    mName.text = "Medusa";
                    desc.text = monDescr[3];
                    break;
                case 4:
                    mName.text = "Minotaur";
                    desc.text = monDescr[4];
                    break;
                case 5:
                    mName.text = "Revenant";
                    desc.text = monDescr[5];
                    break;
                case 6:
                    mName.text = "Spartae";
                    desc.text = monDescr[6];
                    break;
                case 7:
                    mName.text = "Stymphalian Birds";
                    desc.text = monDescr[7];
                    break;
            }
        }


    }
    public void SetIndex(int nIndex)
    {
        index = nIndex;
    }

    void ReadFileToDic(string path)
    {

        StreamReader streamReader = new StreamReader(path);
        string line;
        int counter = 1;
        do
        {
            line = streamReader.ReadLine();
            if (line != null)
            {
                monDescr.Add(counter, line);

                counter++;
            }


        } while (line != null);

        streamReader.Close();

    }
}
