using System.IO;
using System.Collections.Generic;
using UnityEngine;
using static Draw;

public class PandemicDataExample : MonoBehaviour
{
    public string dataCsvFileName = "";
    public GameObject textObjectPrefab = null;
    public GameObject textObjectDeathCount = null;
    public Material virusMaterial = null;
    public Material bacteriaMaterial = null;
    Vector2[] objectTargets = null;
    public GameObject percentageTypeText;
    public GameObject percentageFamilyText;



    List<Pandemic> _pandemics = new List<Pandemic>();
    Dictionary<int, GameObject> _mainObjectLookup = new Dictionary<int, GameObject>();
    int _deathMin, _deathMax;



    void Awake()
    {
        // Parse.
        string csvFilePath = Application.streamingAssetsPath + "/" + dataCsvFileName;
        string csvContent = File.ReadAllText(csvFilePath);
        Parse(csvContent);

        // Filter.
        Filter();

        // Mine.
        Mine();

        // Represent.
        Represent();

        //Interact
        AddInteraction();

        //Show death count
        AddDeathCountInteraction();


        Debug.Log("_pandemic.Count:" + _pandemics.Count);
    }


    void Parse(string csvText)
    {
        // Split by new lines in rows.
        string[] rowContents = csvText.Split('\n');

        // For each row.
        for (int r = 1; r < rowContents.Length; r=r+1)
        {
            string rowContent = rowContents[r];
            string[] fieldContents = rowContent.Split(',');

            //Debug.Log(rowContent);

            Pandemic pandemic = new Pandemic(r,r-1);

            // For each field in this row.
            for (int f = 0; f < fieldContents.Length; f=f+1)
            {
                string fieldContent = fieldContents[f];

                switch (f)
                {
                    case 0:
                        // Pandemic Name
                        pandemic.pandemicName = fieldContent;
                        //Debug.Log(fieldContent);
                        break;

                    case 2:
                        // type - bacteria or virus
                        if (fieldContent == "Virus") pandemic.isVirus = true;
                        else pandemic.isVirus = false;
                        //Debug.Log(pandemic.isVirus);
                        break;

                    case 3:
                        // Deathcount
                        int deathCount;
                        bool parseSucceeded = int.TryParse(fieldContent, out deathCount);
                        if (parseSucceeded) pandemic.deathCount = deathCount;
                        //Debug.Log(deathCount);
                        break;

                    case 1: //Send help very enum much wow. En enum er en drop down menu og ikke free text!!!
                        if (fieldContent == Pandemic.Family.Coronaviridae.ToString()) pandemic.family = Pandemic.Family.Coronaviridae;
                        else if (fieldContent == Pandemic.Family.Filoviridae.ToString()) pandemic.family = Pandemic.Family.Filoviridae;
                        else if (fieldContent == Pandemic.Family.Retroviridae.ToString()) pandemic.family = Pandemic.Family.Retroviridae;
                        else if (fieldContent == Pandemic.Family.Orthomyxoviridae.ToString()) pandemic.family = Pandemic.Family.Orthomyxoviridae;
                        else if (fieldContent == Pandemic.Family.Flaviviridae.ToString()) pandemic.family = Pandemic.Family.Flaviviridae;
                        else if (fieldContent == Pandemic.Family.Vibrionaceae.ToString()) pandemic.family = Pandemic.Family.Vibrionaceae;
                        else if (fieldContent == Pandemic.Family.Enterobacteriaceae.ToString()) pandemic.family = Pandemic.Family.Enterobacteriaceae;
                        else if (fieldContent == Pandemic.Family.Poxviridae.ToString()) pandemic.family = Pandemic.Family.Poxviridae;
                    
                        break;

                        
                }
                //Debug.Log(pandemic.families); 
               

            }

            _pandemics.Add(pandemic);

        }
    }


    void Filter()
    {
        for (int p = _pandemics.Count - 1; p >= 0; p--)
        {

            Pandemic pandemic = _pandemics[p];
            

            if (pandemic.deathCount < 100 || pandemic.deathCount > 1000000000)
            { // If not enough deaths (|or|) unrealistic deaths
                _pandemics.RemoveAt(p);

               

            }
   

        }
       
    }

    void Mine()
    {
        _deathMin = int.MaxValue;
        _deathMax = int.MinValue;
        foreach (Pandemic pandemic in _pandemics)
        {
            if (pandemic.deathCount > _deathMax) _deathMax = pandemic.deathCount;
            else if (pandemic.deathCount < _deathMin) _deathMin = pandemic.deathCount;
            
        }

        Debug.Log("Min:" + _deathMin + ",Max:" + _deathMax);
        
      
       
    }


   
    void Represent()
    {

        //we sort by deathcount. math stuff comparing deathcount.
        _pandemics.Sort((a, b) => a.deathCount - b.deathCount);
        objectTargets = new Vector2[_pandemics.Count];
        for (int p = 0; p < _pandemics.Count; p++)


        {
            Pandemic pandemic = _pandemics[p];

            float mainX = p;
            float deathAreas = Mathf.InverseLerp(0, _deathMax, pandemic.deathCount)*10;
            float deathScale = Mathf.Sqrt(deathAreas / Mathf.PI) * 2;
            
            //float barY = deathScale * 0.5f;
            //float barwidth = 0.5f;

            GameObject mainObject = new GameObject(pandemic.id + " " + pandemic.pandemicName);
            mainObject.transform.SetParent(transform);
            mainObject.transform.localPosition = new Vector3(0, 0, 0);

        
            GameObject sphereObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphereObject.transform.SetParent(mainObject.transform);
            sphereObject.transform.localPosition = new Vector3(0, 0, 0);
            //sphereObject.transform.localPosition = Random.insideUnitCircle;
            sphereObject.transform.localScale = new Vector3(deathScale, deathScale, 0.1f);
      



           if (pandemic.isVirus == true)
            {
                sphereObject.GetComponent<MeshRenderer>().material = virusMaterial;
          
            }
            if (pandemic.isVirus == false)
            {
                sphereObject.GetComponent<MeshRenderer>().material = bacteriaMaterial;

            }

            _mainObjectLookup.Add(pandemic.id, mainObject);

            


        }


    } 


    
    void AddInteraction()
    {
        //Add labels to bars
        foreach (Pandemic pandemic in _pandemics)
        {
            GameObject mainObject = _mainObjectLookup[pandemic.id];

            GameObject textObject = Instantiate(textObjectPrefab); //grab that object
            textObject.SetActive(true);
            textObject.transform.SetParent(mainObject.transform); //set position to parent object
            textObject.transform.localPosition = new Vector3(0,0,1);

            textObject.transform.Rotate(0, 0, -45); //make labels tilt out of existence
            textObject.GetComponent<TextMesh>().text = pandemic.pandemicName;
            

            //Show pandemic name when hovered
            /*Collider barCollider = mainObject.GetComponentInChildren<Collider>();
            ShowText showText = barCollider.gameObject.AddComponent<ShowText>(); //slap a script on the objects with collider
            showText.textObject = textObject;*/

            // Dab cause it works

            

        }
        
    } 

    void AddDeathCountInteraction()
    {
        //Add death count at top
        foreach (Pandemic pandemic in _pandemics)
        {
            GameObject mainObject = _mainObjectLookup[pandemic.id];

            GameObject textObject    = Instantiate(textObjectDeathCount); //grab that object
            textObject.SetActive(true);
            textObject.transform.localPosition = new Vector3(15.5f, -10f, 0);
            //textObject.transform.SetParent(mainObject.transform);
            textObject.GetComponent<TextMesh>().text = pandemic.deathCount.ToString();

            Collider barCollider = mainObject.GetComponentInChildren<Collider>();
            ShowText showText = barCollider.gameObject.AddComponent<ShowText>(); //slap a script on the objects with collider
            showText.textObject = textObject;

            //Debug.Log(mainObject.name);
        }
    }

    [Range (1,100)]public int randomness = 88;
    [System.Serializable] public enum SortingMode {ByType, ByFamily }
    public SortingMode SortingChoice = new SortingMode ();

    void Update()
    {
        Random.InitState(randomness);

        foreach (Pandemic pandemic in _pandemics)
        {

            

            switch (SortingChoice) {

                case SortingMode.ByType:
                    Debug.Log("YOU PICKED THE SECOND SORTING MODE!!");

                    percentageTypeText.SetActive(true);
                    percentageFamilyText.SetActive(false);

                    if (pandemic.isVirus == true)
                    {
                        objectTargets[pandemic.index] = Random.insideUnitCircle + new Vector2(5, 0);
                    }

                    else
                    {
                        objectTargets[pandemic.index] = Random.insideUnitCircle + new Vector2(29, 0);
                    }

                    break;

                case SortingMode.ByFamily:

                    float x = (int)pandemic.family;
                    objectTargets[pandemic.index] = Random.insideUnitCircle + new Vector2(x * 5, 0);
                    Debug.Log("YOU PICKED THE FIRST SORTING MODE!!");

                    percentageFamilyText.SetActive(true);
                    percentageTypeText.SetActive(false);

                    break;


                  
            }

        }

        foreach (Pandemic pandemic in _pandemics)
        {
            GameObject mainObject = _mainObjectLookup[pandemic.id];
            Vector2 objectTarget = objectTargets[pandemic.index];
            mainObject.transform.position = Vector3.Lerp(mainObject.transform.position, objectTarget, Time.deltaTime);
        }


    }
    /*void Update ()

       
    {
        
        Random.InitState (randomness);

        foreach ( Pandemic pandemic in _pandemics){
            float x = (int)pandemic.family;
            objectTargets[pandemic.index] = Random.insideUnitCircle + new Vector2(x*5,0);
        }



        foreach (Pandemic pandemic in _pandemics){
            GameObject mainObject = _mainObjectLookup[pandemic.id];
            Vector2 objectTarget = objectTargets[pandemic.index];
            mainObject.transform.position = Vector3.Lerp(mainObject.transform.position, objectTarget,Time.deltaTime);
        }


    }*/
}