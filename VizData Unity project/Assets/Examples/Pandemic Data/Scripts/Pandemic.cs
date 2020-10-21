public class Pandemic //Person
{
    public int id;
    public int index;
    public string pandemicName; //firstName
    public Family family;
    public bool isVirus;
    public int deathCount; //age
    //public string lastName;
    //public CovidRelationLevel covidRelationLevel;
    //public int postNumber;
    //public int cohabitantsCount;
    //public int steamGamesCount;
    //public int siblingCount;


    public enum Family
    {
        Poxviridae, Enterobacteriaceae, Vibrionaceae, Flaviviridae, Orthomyxoviridae, Retroviridae, Coronaviridae, Filoviridae, None
    }



    public Pandemic(int id, int index)
    {
        this.id = id;
        this.index = index;
    }
}
