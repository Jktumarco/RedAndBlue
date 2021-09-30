using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;
using System;
using UnityEngine.UI;
using System.Linq;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    Color[] color;
    [SerializeField]
    List<TablText> listButtons;

    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    Canvas canvas;
    

    public static UIManager s;

    private TablText[] tablTexts;

    public List<TablText> _listButtons => listButtons; 

    private void Awake()
    {
        if(s == null) { s = this; }
       
    }
    

    public void buttonUpdateText() {
        if(QuClass.s?._unitsOneRound.Count > 20) {
            ButtonClier();
            for (int i = 0; i < 20; i++)
            {
                if (QuClass.s?._unitsOneRound != null) { dataTextToButton(QuClass.s?._unitsOneRound, i); }
                else return;
            }
        }
        if (QuClass.s?._unitsOneRound.Count < 20)
        {
            ButtonClier();
            for (int i = 0; i < QuClass.s?._unitsOneRound.Count; i++)
            {
                if (QuClass.s?._unitsOneRound != null) { dataTextToButton(QuClass.s?._unitsOneRound, i); }
                else return;
            }
        }




    }

    public void dataTextToButton( List <Unit> dataUnits, int indx)
    {
        if(dataUnits[indx] == null) { return; }
        Unit unit = dataUnits[indx];
        TablText button = listButtons[indx];
        var color = unit.color;
        var i = unit.inisiative.ToString();
        var s = unit.speed.ToString();
        var n = unit.namber.ToString();
        button.text.text = "Существо: " + n + " : " + " Инициатива - " + i + " Скорость - " + s;
        button.text.fontSize = 8;
        button.text.alignment = TextAnchor.LowerLeft;
        button.text.alignment = TextAnchor.MiddleCenter;
        if (color == "red") { listButtons[indx].panel.GetComponent<Image>().color = this.color[1]; }
        else { listButtons[indx].panel.GetComponent<Image>().color = this.color[0]; }
    }

    public void MakeUI()
    {
        
        for (int i = 0; i < 16; i++)
        {
            if (i == 15)
            {
                GameObject go = Instantiate(prefab, prefab.transform.position + new Vector3(0, -20 * 14, 0), Quaternion.identity);
                go.transform.GetChild(1).GetComponent<Text>().text = "Round 2";
                go.transform.GetChild(0).GetComponent<Text>().text = "";
                go.transform.GetChild(0).GetComponent<Text>().alignment = TextAnchor.LowerLeft;
                go.transform.SetParent(canvas.transform);
                for (int j = 15; j < 21; j++)
                {
                    GameObject g = Instantiate(prefab, prefab.transform.position + new Vector3(0, -20 * j, 0), Quaternion.identity); g.transform.SetParent(canvas.transform);
                    var s = g.GetComponent<TablText>();
                    s.nomber.text = (j).ToString();
                    listButtons.Add(s);
                }

            }
            if (i < 14)
            {
                GameObject g = Instantiate(prefab, prefab.transform.position + new Vector3(0, -20 * i, 0), Quaternion.identity); g.transform.SetParent(canvas.transform);
                var s = g.GetComponent<TablText>();
                s.nomber.text = (i + 1).ToString();
                listButtons.Add(s);
            }

        }
    }

    public void ButtonClier()
    { if (listButtons.Any(x => x.text.text != null))
        {
            foreach (var item in listButtons)
            {

                item.text.text = "";
                item.panel.GetComponent<Image>().color = this.color[2];
            }
        }
        else return;
    }
  
}
