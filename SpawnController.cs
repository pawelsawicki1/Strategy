using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum entityType
{
    building, //0 - main, 1 - tower
    node, //0 - gold, 1 - wood, 2 - iron
    unit,//0 - worker, 1 - warrior, 2 - archer
}
public class SpawnController : MonoBehaviour
{
    //List is filled manually with the scriptableObjects in Editor mode
    public  ResourceNode[] resourceNodes;
    public Building[] buildingList;
    public Unit[] unitList;
    public Vector2[] BaseSpawnPoints;
    public void Spawn(entityType whatToSpawn, int whatToSpawnNumber, Vector3 position, out GameObject GO)
    {
        ResourceNode RN = Instantiate(resourceNodes[whatToSpawnNumber]);
        RN.name = "data";
        GO = Instantiate(RN.prefab, position, new Quaternion(0, 0, 0, 0));
        GO.GetComponent<ResourceNodeDataHolder>().resource_info = RN;
        GO.name = RN.resourceName;
    }
    public void Spawn(entityType whatToSpawn, int whatToSpawnNumber, Vector3 position)
    {
        ResourceNode RN = Instantiate(resourceNodes[whatToSpawnNumber]);
        RN.name = "data";
        GameObject GO = Instantiate(RN.prefab, position, new Quaternion(0, 0, 0, 0));
        GO.GetComponent<ResourceNodeDataHolder>().resource_info = RN;
        GO.name = RN.resourceName;
    }
    public void Spawn(entityType whatToSpawn, int whatToSpawnNumber, Vector3 position, int team)
    {
        if (whatToSpawn == entityType.building)
        {
            Building RN = Instantiate(buildingList[whatToSpawnNumber]);
            RN.name = "data";
            GameObject GO = Instantiate(RN.prefab, position, new Quaternion(0, 0, 0, 0));
            GO.GetComponent<BuildingDataHolder>().building_info = RN;
            GO.name = RN.building_name;
            RN.team = team;
            AddToTeam(GO, RN);
        }
        else if(whatToSpawn==entityType.unit)
        {
            Unit RN = Instantiate(unitList[whatToSpawnNumber]);
            RN.name = "data";
            GameObject GO = Instantiate(RN.prefab, position, new Quaternion(0, 0, 0, 0));
            GO.GetComponent<UnitDataHolder>().unit_info = RN;
            GO.name = RN.unit_name;
            RN.team = team;
            AddToTeam(GO, RN);
        }
    }
    private void Start()
    {
        BuildStartingVillages();
        Spawn(entityType.unit, 0, Vector3.zero, 1);
    }
    public void BuildStartingVillages()
    {
        for(int team = 1; team < 5; team++)
        {
            GameObject GO = new GameObject();
            GO.name = "Team " + team;
            GO.transform.SetParent(GameObject.Find("GameController").transform);
            GO = new GameObject();
            GO.name = "Buildings";
            GO.transform.SetParent(GameObject.Find("Team " + team).transform);
            GO = new GameObject();
            GO.name = "Units";
            GO.transform.SetParent(GameObject.Find("Team " + team).transform);
            float x;
            float y;
            float rand = Random.Range(-1, 1);
            if (rand < 0)
            {
                 x = Random.Range(-10, -5);
            }
            else
                 x = Random.Range(5, 10);
            rand = Random.Range(-1, 1);
            Spawn(entityType.building, 0, BaseSpawnPoints[team-1], team);
            for (int i = 0; i < 1; i++)
            {
                Spawn(entityType.unit, 0, BaseSpawnPoints[team-1]+new Vector2(-2+1*i,-1), team);
            }
            for(int i=0;i<3;i++)
            {
                if (rand < 0)
                    y = Random.Range(4, 7);
                else
                    y = Random.Range(-7, -4);
                Spawn(entityType.node, i, BaseSpawnPoints[team - 1]+new Vector2(x+x*i,y));
            }
        }
        StartCoroutine(ManageTeams());
    }
    private IEnumerator ManageTeams()
    {
        while(true)
        {
            yield return new WaitForSeconds(.1f);
            TeamManager(0);
            yield return new WaitForSeconds(.1f);
            TeamManager(1);
            yield return new WaitForSeconds(.1f);
            TeamManager(2);
            yield return new WaitForSeconds(.1f);
            TeamManager(3);
        }
    }
    public void AddToTeam(GameObject GO, Unit GOU)
    {
        switch(GOU.team)
        {
            case 1:
                GO.transform.SetParent(GameObject.Find("Team 1/Units").transform);
                //StartCoroutine(UpdateAfter1Sec());
                GameObject.Find("GameController").GetComponent<Team>().UnitCount1++;
                GameObject.Find("GameController").GetComponent<Team>().UnitList1.Add(GOU);
                GameObject.Find("GameController").GetComponent<Team>().UnitGO1.Add(GO);
                GOU.index = GameObject.Find("GameController").GetComponent<Team>().UnitList1.LastIndexOf(GOU);
                break;
            case 2:
                GO.transform.SetParent(GameObject.Find("Team 2/Units").transform);
                //StartCoroutine(UpdateAfter1Sec());
                GameObject.Find("GameController").GetComponent<Team>().UnitCount2++;
                GameObject.Find("GameController").GetComponent<Team>().UnitList2.Add(GOU);
                GameObject.Find("GameController").GetComponent<Team>().UnitGO2.Add(GO);
                GOU.index = GameObject.Find("GameController").GetComponent<Team>().UnitList2.LastIndexOf(GOU);
                break;
            case 3:
                GO.transform.SetParent(GameObject.Find("Team 3/Units").transform);
                //StartCoroutine(UpdateAfter1Sec());
                GameObject.Find("GameController").GetComponent<Team>().UnitCount3++;
                GameObject.Find("GameController").GetComponent<Team>().UnitList3.Add(GOU);
                GameObject.Find("GameController").GetComponent<Team>().UnitGO3.Add(GO);
                GOU.index = GameObject.Find("GameController").GetComponent<Team>().UnitList3.LastIndexOf(GOU);
                break;
            case 4:
                GO.transform.SetParent(GameObject.Find("Team 4/Units").transform);
                //StartCoroutine(UpdateAfter1Sec());
                GameObject.Find("GameController").GetComponent<Team>().UnitCount4++;
                GameObject.Find("GameController").GetComponent<Team>().UnitList4.Add(GOU);
                GameObject.Find("GameController").GetComponent<Team>().UnitGO4.Add(GO);
                GOU.index = GameObject.Find("GameController").GetComponent<Team>().UnitList4.LastIndexOf(GOU);
                break;
        }
        //UpdateTeam();
    }
    public void AddToTeam(GameObject GO, Building GOB)
    {

        GO.transform.SetParent(GameObject.Find("Team " + GOB.team + "/Buildings").transform);
        StartCoroutine(UpdateAfter1Sec());
        //UpdateTeam();
    }
    public void RemoveFromTeam(GameObject GO)
    {
        Unit GOU = GO.GetComponent<UnitDataHolder>().unit_info;
        int counter = GOU.index;
        switch (GOU.team)
        {
            case 1:
                //StartCoroutine(UpdateAfter1Sec());
                GameObject.Find("GameController").GetComponent<Team>().UnitCount1--;
                GameObject.Find("GameController").GetComponent<Team>().UnitGO1.RemoveAt(GOU.index);
                GameObject.Find("GameController").GetComponent<Team>().UnitList1.RemoveAt(GOU.index);
                for(int i=counter;i< GameObject.Find("GameController").GetComponent<Team>().UnitCount1;i++)
                    GameObject.Find("GameController").GetComponent<Team>().UnitList1[i].index--;
                break;
            case 2:
                //StartCoroutine(UpdateAfter1Sec());
                GameObject.Find("GameController").GetComponent<Team>().UnitCount2--;
                GameObject.Find("GameController").GetComponent<Team>().UnitGO2.RemoveAt(GOU.index);
                GameObject.Find("GameController").GetComponent<Team>().UnitList2.RemoveAt(GOU.index);
                for (int i = counter; i < GameObject.Find("GameController").GetComponent<Team>().UnitCount2; i++)
                    GameObject.Find("GameController").GetComponent<Team>().UnitList2[i].index--;
                break;
            case 3:
                //StartCoroutine(UpdateAfter1Sec());
                GameObject.Find("GameController").GetComponent<Team>().UnitCount3--;
                GameObject.Find("GameController").GetComponent<Team>().UnitGO3.RemoveAt(GOU.index);
                GameObject.Find("GameController").GetComponent<Team>().UnitList3.RemoveAt(GOU.index);
                for (int i = counter; i < GameObject.Find("GameController").GetComponent<Team>().UnitCount3; i++)
                    GameObject.Find("GameController").GetComponent<Team>().UnitList3[i].index--;
                break;
            case 4:
                //StartCoroutine(UpdateAfter1Sec());
                GameObject.Find("GameController").GetComponent<Team>().UnitCount4--;
                GameObject.Find("GameController").GetComponent<Team>().UnitGO4.RemoveAt(GOU.index);
                GameObject.Find("GameController").GetComponent<Team>().UnitList4.RemoveAt(GOU.index);
                for (int i = counter; i < GameObject.Find("GameController").GetComponent<Team>().UnitCount4; i++)
                    GameObject.Find("GameController").GetComponent<Team>().UnitList4[i].index--;
                break;
        }
        Destroy(GO.GetComponent<UnitDataHolder>().unit_info);

        Destroy(GO);

        //StartCoroutine(UpdateAfter1Sec());
        //UpdateTeam();
    }
    private void UpdateTeam()
    {
        GameObject GO = GameObject.Find("Team 1/Units");
        //int counter = 0;
        foreach(UnitDataHolder children in GO.transform.GetComponentsInChildren(typeof(UnitDataHolder)))
        {
            //children.unit_info.index = counter;
            //counter++;
        }
        //GameObject.Find("GameController").GetComponent<Team>().UnitCount=counter;
    }
    private IEnumerator UpdateAfter1Sec()
    {
        yield return new WaitForEndOfFrame();
        UpdateTeam();
    }
    public void TeamManager(int team)
    {
        GameObject TeamList = GameObject.Find("GameController");
        int counter = 0, counter1 = 0,counter2=0, counter3=0, counter4=0;
        Debug.Log("Updating Team: " + team);
        switch(team)
        {
            case 1:
                foreach(Unit children in TeamList.GetComponent<Team>().UnitList1)
                {
                    if (children.unit == Eunit.worker && children.behaviour == EWBehaviour.idle)
                        counter++;
                }
                TeamList.GetComponent<Team>().Idle1 = counter;
                counter = 0;
                foreach (Unit children in TeamList.GetComponent<Team>().UnitList1)
                {
                    if (children.unit == Eunit.worker && children.behaviour == EWBehaviour.gathering_gold)
                        counter++;
                    else if (children.unit == Eunit.worker && children.behaviour == EWBehaviour.gathering_wood)
                        counter1++;
                    else if (children.unit == Eunit.worker && children.behaviour == EWBehaviour.gathering_iron)
                        counter2++;
                }
                TeamList.GetComponent<Team>().Gathering1[0] = counter;
                TeamList.GetComponent<Team>().Gathering1[1] = counter1;
                TeamList.GetComponent<Team>().Gathering1[2] = counter2;
                break;
            case 2:
                foreach (Unit children in TeamList.GetComponent<Team>().UnitList2)
                {
                    if (children.unit == Eunit.worker && children.behaviour == EWBehaviour.idle)
                        counter++;
                }
                TeamList.GetComponent<Team>().Idle2 = counter;
                counter = 0;
                foreach (Unit children in TeamList.GetComponent<Team>().UnitList2)
                {
                    if (children.unit == Eunit.worker && children.behaviour == EWBehaviour.gathering_gold)
                        counter++;
                    else if (children.unit == Eunit.worker && children.behaviour == EWBehaviour.gathering_wood)
                        counter1++;
                    else if (children.unit == Eunit.worker && children.behaviour == EWBehaviour.gathering_iron)
                        counter2++;
                    else if (children.unit == Eunit.warrior)
                        counter3++;
                    else if (children.unit == Eunit.archer)
                        counter4++;

                }
                TeamList.GetComponent<Team>().Gathering2[0] = counter;
                TeamList.GetComponent<Team>().Gathering2[1] = counter1;
                TeamList.GetComponent<Team>().Gathering2[2] = counter2;
                TeamList.GetComponent<Team>().SoldiersCount1[0] = counter3;
                TeamList.GetComponent<Team>().SoldiersCount1[1] = counter4;
                break;
            case 3:
                foreach (Unit children in TeamList.GetComponent<Team>().UnitList3)
                {
                    if (children.unit == Eunit.worker && children.behaviour == EWBehaviour.idle)
                        counter++;
                }
                TeamList.GetComponent<Team>().Idle3 = counter;
                counter = 0;
                foreach (Unit children in TeamList.GetComponent<Team>().UnitList2)
                {
                    if (children.unit == Eunit.worker && children.behaviour == EWBehaviour.gathering_gold)
                        counter++;
                    else if (children.unit == Eunit.worker && children.behaviour == EWBehaviour.gathering_wood)
                        counter1++;
                    else if (children.unit == Eunit.worker && children.behaviour == EWBehaviour.gathering_iron)
                        counter2++;
                }
                TeamList.GetComponent<Team>().Gathering3[0] = counter;
                TeamList.GetComponent<Team>().Gathering3[1] = counter1;
                TeamList.GetComponent<Team>().Gathering3[2] = counter2;
                break;
            case 4:
                foreach (Unit children in TeamList.GetComponent<Team>().UnitList4)
                {
                    if (children.unit == Eunit.worker && children.behaviour == EWBehaviour.idle)
                        counter++;
                }
                TeamList.GetComponent<Team>().Idle4 = counter;
                counter = 0;
                foreach (Unit children in TeamList.GetComponent<Team>().UnitList4)
                {
                    if (children.unit == Eunit.worker && children.behaviour == EWBehaviour.gathering_gold)
                        counter++;
                    else if (children.unit == Eunit.worker && children.behaviour == EWBehaviour.gathering_wood)
                        counter1++;
                    else if (children.unit == Eunit.worker && children.behaviour == EWBehaviour.gathering_iron)
                        counter2++;
                }
                TeamList.GetComponent<Team>().Gathering4[0] = counter;
                TeamList.GetComponent<Team>().Gathering4[1] = counter1;
                TeamList.GetComponent<Team>().Gathering4[2] = counter2;
                break;
        }
    }
}
