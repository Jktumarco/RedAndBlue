using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Army : MonoBehaviour
{
    public static Army s;

    [SerializeField]
    List<Unit> DataArmy;

    [SerializeField]
     List<Unit> red;
    [SerializeField]
    List<Unit> blue;

    public List<Unit> _DataArmy {get { return DataArmy; } set { DataArmy = value; } }


    void Awake()
    {
        if (s == null) { s = this; }
        AnyLoadArmy("red", red);
        AnyLoadArmy("blue", blue);
    }
    
    public List<Unit> UnitsBlue()
    {
        if(blue == null) { return null; }
        return blue;
    }
    public List<Unit> UnitsRed()
    {
        if(red == null) { return null;  }
        return red;
    }
    public int AllListCount()
    {
       return UnitsBlue().Count + UnitsRed().Count;
    }
    public void ClearLists()
    {
        red.Clear();
        blue.Clear();
    }
    public void ReLoadArmy()               
    {
        
        ClearLists();
        AnyLoadArmy("red", red);
        AnyLoadArmy("blue", blue);
    }
    public void ReLoadArmyWithoutTwo(Unit unit)
    {
        ClearLists();                     
        AnyLoadArmy("red", red);
        AnyLoadArmy("blue", blue);
        RemoveUnit(GetUnit(unit));
    }

    public Unit GetUnit(Unit unit )
    {
        if(unit.color == "red") { return UnitsRed()?.Find(x => x == unit); }
        if (unit.color == "blue") { return UnitsBlue()?.Find(x => x == unit); }
        return null;

    }

    public void RemoveUnit(Unit currUnit)
    {
        if(currUnit.color == "red") {
            int indx = UnitsRed().IndexOf(currUnit);
            UnitsRed().RemoveAt(indx);
        }
        else UnitsBlue().RemoveAt(UnitsBlue().IndexOf(currUnit));

    }



    public void AnyLoadArmy(string color, List<Unit> unit)
    {
        var d = DataArmy.FindAll(x => x.color == color);
        unit.AddRange(d);
    }



}
[System.Serializable]
public class Unit
{
    public string color;
    public float namber;
    public float inisiative;
    public float speed;

   
}

