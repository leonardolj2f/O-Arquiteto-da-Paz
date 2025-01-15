using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Uniao : MonoBehaviour
{
    

    public List<Pais> paises = new();

    private Mats ouro = new Mats("ouro",1);
    private Mats carvao = new Mats("carvão",0.0001f);
    private Mats petroleo = new Mats("petróleo",0.001f);
    private Mats madeira = new Mats("madeira",0.00001f);
    private int reservaOuro;
    private int reservaCarvao;
    private int reservaPetroleo;
    private int reservaMadeira;

    public UiManager uiManager;

    //ouro=11
    //carvao=12
    //petroleo=13
    //madeira=14

    public List<Mats> materialImportance = new();

    public void CalcResOuro(){
        for (int i = 0; i < paises.Count; i++){
            reservaOuro = reservaOuro + paises[i].ouroP - paises[i].ouroG;
        }
    }

    public void CalcResCarvao(){
        for (int i = 0; i < paises.Count; i++){
            reservaCarvao = reservaCarvao + paises[i].carvaoP - paises[i].carvaoG;
        }
    }

    public void CalcResPetroleo(){
        for (int i = 0; i < paises.Count; i++){
            reservaPetroleo = reservaPetroleo + paises[i].petroleoP - paises[i].petroleoG;
        }
    }

    public void CalcResMadeira(){
        for (int i = 0; i < paises.Count; i++){
            reservaMadeira = reservaMadeira + paises[i].madeiraP - paises[i].madeiraG;
        }
    }

    public void RemoveFromReservaOuro(int amount){
        reservaOuro -= amount;
    }
    public void RemoveFromReservaCarvao(int amount){
        reservaCarvao -= amount;
    }
    public void RemoveFromReservaPetroleo(int amount){
        reservaPetroleo -= amount;
    }
    public void RemoveFromReservaMadeira(int amount){
        reservaMadeira -= amount;
    }

    public Mats GetOuro(){
        return ouro;
    }
    public Mats GetCarvao(){
        return carvao;
    }
    public Mats GetPetroleo(){
        return petroleo;
    }
    public Mats GetMadeira(){
        return madeira;
    }

    public int GetReservaOuro(){
        return reservaOuro;
    }

    public int GetReservaCarvao(){
        return reservaCarvao;
    }

    public int GetReservaPetroleo(){
        return reservaPetroleo;
    }

    public int GetReservaMadeira(){
        return reservaMadeira;
    }

    public void UpdateMaterialImportance(){
        float totalImportance = reservaOuro*ouro.baseImportance + reservaCarvao*carvao.baseImportance + reservaPetroleo*petroleo.baseImportance + reservaMadeira*madeira.baseImportance;
        ouro.normalizedImportance = Mathf.Abs(reservaOuro*ouro.baseImportance/totalImportance);
        carvao.normalizedImportance = Mathf.Abs(reservaCarvao*carvao.baseImportance/totalImportance);
        petroleo.normalizedImportance = Mathf.Abs(reservaPetroleo*petroleo.baseImportance/totalImportance);
        madeira.normalizedImportance = Mathf.Abs(reservaMadeira*madeira.baseImportance/totalImportance);
        // Debug.Log($"reservaOuro: {reservaMadeira}");
        // Debug.Log($"madeira.baseImportance: {madeira.baseImportance}");
        // Debug.Log($"totalImportance: {totalImportance}");
        // Debug.Log(reservaOuro*madeira.baseImportance/totalImportance);
        materialImportance.Sort((a, b) => b.normalizedImportance.CompareTo(a.normalizedImportance));
        // for (int i = 0; i < materialImportance.Count; i++){
        //     Debug.Log("Item " + i + ": " + materialImportance[i].name + " importance" +materialImportance[i].normalizedImportance);
        // }
    }
    // Start is called before the first frame update
    void Start()
    {
        materialImportance.Add(ouro);
        materialImportance.Add(petroleo);
        materialImportance.Add(carvao);
        materialImportance.Add(madeira);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown(){
        uiManager.ClearHoverText();
        uiManager.button.gameObject.SetActive(false);
    }
}


public class Mats{
    public string name;
    public float baseImportance;
    public float normalizedImportance;
    public Mats(string name, float baseImportance){
        this.name = name;
        this.baseImportance = baseImportance;
        this.normalizedImportance = 0f;
    }
}
