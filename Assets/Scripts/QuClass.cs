using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class QuClass : MonoBehaviour
{
    public static QuClass s;

    private Unit currRed;
    private Unit currBlue;

    public int round;
    [SerializeField]
    Text roundText;
    [SerializeField]
    private float step = 0;
   
    [SerializeField]
    List<Unit> QueueUnits;          
   
    public List<Unit> beforeQueue;
    [SerializeField]
     List<Unit> memory;

    public List<Unit> _unitsOneRound { get { return QueueUnits; } }
  

    private void Awake()
    {
        if (s == null) { s = this; }
    }

    void Start()
    {
        MainLoadingGameLogic();
        UIManager.s.buttonUpdateText();
        Army.s.ReLoadArmy();
       
    }
    public void Loader()
    {
        MainLoadingGameLogic();
        UIManager.s.buttonUpdateText();
        Army.s.ReLoadArmy();
    }

    public void HitEnemy()
    {
        step++;
        if (step == 14) { roundText.text = "2"; round = 2; }

        currBlue = null;
        currRed = null;

        Army.s._DataArmy.Remove(Army.s.GetUnit(QueueUnits[1]));
        Army.s.ReLoadArmy();

        var count = Army.s.AllListCount() / 2 + 1;
        SortAllArmy();
       
        beforeQueue.Clear();
        QueueUnits.Clear();

        for (int i = 0; i <= count; i++)
        {
            SortAllArmy();
            Takeeeee(QueueUnits);
        }
        Army.s.ReLoadArmy();

        var counts = Army.s.AllListCount() / 2 + 1;
        for (int j = 0; j < counts; j++)
        {
            SortAllArmy();
            Takeeeee(QueueUnits);
        }
        
        Army.s.ReLoadArmy();
        UIManager.s.buttonUpdateText();

    }


    public void NullStep()
    {
        QueueUnits.RemoveAt(0);
        UIManager.s.buttonUpdateText();
        step++;
        if(step == 14) { roundText.text = "2"; }
    }
    


    public void MainLoadingGameLogic() {

        UIManager.s.MakeUI();
        SortList(Army.s.UnitsBlue());
        SortList(Army.s.UnitsRed());
        for (int i = 0; i < 7; i++)
        {
            Take(QueueUnits);
        }
        Army.s.ReLoadArmy();
        SortList(Army.s.UnitsBlue());
        SortList(Army.s.UnitsRed());
        for (int i = 0; i < 7; i++)
        {
            beforeQueue.Clear();
            GetFromBothArmyIntensive();
            GetAndSetToQueue(QueueUnits);
        }
        UIManager.s.buttonUpdateText();
    }

    // берет по индексу unita отправляет ссылку удаляет из основного списка
    // сортирует его, 
    // очищает очередь
    // отбирает в очередь
    // добавляет в очередь
    // обновляет UI


    public void Take( List <Unit> roundList)
    {
        beforeQueue.Clear();
        GetFromBothArmyIntensive();                        // взять приоритетного воина с одной армии  
        GetAndSetToQueue(roundList);                                // пройти проверку и добавить в очередь
       
    }
    public void Takeeeee(List<Unit> roundList)
    {
        beforeQueue.Clear();
        GetFromBothArmyIntensiveTwooo();                        // взять приоритетного воина с одной армии  
        GetAndSetToQueue(roundList);                                // пройти проверку и добавить в очередь
                      
    }


    // выбираю максимальную intensive и затем, если в одной армии несколько похожих, то вибираю приоритет по номеру;
    public Unit GetPriorityUnit(List<Unit> unit)
    {
        if (unit == null) return null;
        
             var r = unit.Max(x => x.inisiative);
            return unit.Find(x => x.inisiative == r);
    }


    public void PriorityUnit(List<Unit> unit)
    {
        if (unit == null) { return; }
        else { var r = unit.Max(x => x.inisiative);

            var u = unit.Find(x => x.inisiative == r);
            beforeQueue.Add(u);
        }
    }


    public void  GetAndSetToQueue( List<Unit> roundList)
    {
        if (beforeQueue != null ) 
        {
            

            if (beforeQueue.Count > 1)
            {
               

                if (beforeQueue[0].inisiative == beforeQueue[1].inisiative && beforeQueue[0].speed == beforeQueue[1].speed && round == 1)
                { roundList.Add(beforeQueue.Find(x => x.color == "red")); roundList.Add(beforeQueue.Find(x => x.color == "blue")); }

                if (beforeQueue[0].inisiative == beforeQueue[1].inisiative && beforeQueue[0].speed == beforeQueue[1].speed && round > 1)
                { roundList.Add(beforeQueue.Find(x => x.color == "blue")); roundList.Add(beforeQueue.Find(x => x.color == "red")); }

                if (beforeQueue[0].inisiative == beforeQueue[1].inisiative && beforeQueue[0].speed > beforeQueue[1].speed)
                { roundList.Add(beforeQueue[0]); roundList.Add(beforeQueue[1]); }

                if (beforeQueue[0].inisiative == beforeQueue[1].inisiative && beforeQueue[0].speed < beforeQueue[1].speed)
                { roundList.Add(beforeQueue[1]); roundList.Add(beforeQueue[0]); }

                if (beforeQueue[0].inisiative > beforeQueue[1].inisiative)
                { roundList.Add(beforeQueue[0]); roundList.Add(beforeQueue[1]); }

                if (beforeQueue[0].inisiative < beforeQueue[1].inisiative)
                { roundList.Add(beforeQueue[1]); roundList.Add(beforeQueue[0]); }

            }
            if (beforeQueue.Count == 1) { roundList.Add(beforeQueue[0]); }

        }
    }


    public List<Unit> SortList(List<Unit> unit)
    {
        var s = unit.OrderBy(x => x.inisiative);
        foreach (var item in s)
        {
            unit.Add(item);
        }
        unit.RemoveRange(0, unit.Count / 2);
        return unit;
    }

    public void SortAllArmy()
    {
        SortList(Army.s.UnitsBlue());
        SortList(Army.s.UnitsRed());
    }


  

    public void GetFromBothArmyIntensive()
    {
         currRed = GetPriorityUnit(Army.s.UnitsRed());
         currBlue = GetPriorityUnit(Army.s.UnitsBlue());
       
        if (currRed.inisiative > currBlue.inisiative) { beforeQueue.Add(currBlue); beforeQueue.Add(currRed);}
        if(currRed.inisiative < currBlue.inisiative) { beforeQueue.Add(currRed); beforeQueue.Add(currBlue);}
       
        if (currRed.inisiative == currBlue.inisiative) { beforeQueue.Add(currRed); beforeQueue.Add(currBlue); }
       
        Army.s.RemoveUnit(Army.s.GetUnit(currRed));
        Army.s.RemoveUnit(Army.s.GetUnit(currBlue));
    }

    public void GetFromBothArmyIntensiveTwooo()
    {
        if (beforeQueue.Count < 0) { beforeQueue.Clear(); }
      
        if (Army.s.UnitsRed().Count  != 0) { PriorityUnit(Army.s?.UnitsRed()); }
        if (Army.s.UnitsBlue().Count != 0) { PriorityUnit(Army.s?.UnitsBlue()); }
       
        currRed = beforeQueue.Find(x => x.color == "red");
        currBlue = beforeQueue.Find(x => x.color == "blue");
        
        if ( currRed != null && currBlue != null)
        {
            beforeQueue.Clear();
            if (currRed.inisiative > currBlue.inisiative)
            { beforeQueue.Add(currRed); beforeQueue.Add(currBlue); }

            if (currRed.inisiative < currBlue.inisiative) 
            { beforeQueue.Add(currBlue); beforeQueue.Add(currRed); }

            if (currRed.inisiative == currBlue.inisiative) 
            { beforeQueue.Add(currRed); beforeQueue.Add(currBlue); }

            Army.s.RemoveUnit(Army.s.GetUnit(currRed));
            Army.s.RemoveUnit(Army.s.GetUnit(currBlue));
        }
        if (currRed == null && currBlue != null)
        {
            beforeQueue.Clear();
            beforeQueue.Add(currBlue);
            Army.s.RemoveUnit(Army.s.GetUnit(currBlue));
        }

        if (currRed != null && currBlue == null)
        {
            beforeQueue.Clear();
            beforeQueue.Add(currRed); 
            Army.s.RemoveUnit(Army.s.GetUnit(currRed));
            
        }
       
    }
    
}



