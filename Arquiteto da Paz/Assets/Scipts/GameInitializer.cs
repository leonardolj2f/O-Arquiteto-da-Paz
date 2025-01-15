using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInitializer : MonoBehaviour
{
    public Pais p1;
    public Pais p2;
    public Pais p3;
    public Pais p4;
    public Pais p5;
    public Pais p6;
    public Pais p7;
    public Pais p8;

    public int day = 0;

    FlagManager flagManager;

    UiManager uiManager;

    public List<Pais> inimigos = new();

    private float timer = 0;
    
    private bool played = false;
    //Unity.Mathematics.Random rnd = new();
    public Uniao union;
    public bool isChoosing = false;

    public Pais currentCountry;

    public Pais attackedCountry;

    public Mats wantedMaterial;
    List<int> greens = new List<int>();
    List<int> reds = new List<int>();

    public List<string> possibleMats = new();
    // Start is called before the first frame update
    void Start()
    {
        inimigos.Add(p1);
        inimigos.Add(p2);
        inimigos.Add(p3);
        inimigos.Add(p4);
        inimigos.Add(p5);
        inimigos.Add(p6);
        inimigos.Add(p7);
        inimigos.Add(p8);
        p1.vizinhos.Add(p2);
        p2.vizinhos.Add(p1);
        p2.vizinhos.Add(p3);
        p2.vizinhos.Add(p4);
        p2.vizinhos.Add(p6);
        p3.vizinhos.Add(p2);
        p4.vizinhos.Add(p2);
        p4.vizinhos.Add(p5);
        p4.vizinhos.Add(p6);
        p4.vizinhos.Add(p7);
        p5.vizinhos.Add(p4);
        p5.vizinhos.Add(p7);
        p6.vizinhos.Add(p2);
        p6.vizinhos.Add(p4);
        p6.vizinhos.Add(p7);
        p6.vizinhos.Add(p8);
        p7.vizinhos.Add(p6);
        p7.vizinhos.Add(p4);
        p7.vizinhos.Add(p5);
        p7.vizinhos.Add(p8);
        p8.vizinhos.Add(p7);
        p8.vizinhos.Add(p6);
        possibleMats.Add("Carvão");
        possibleMats.Add("Madeira");
        possibleMats.Add("Ouro");
        possibleMats.Add("Petróleo");
        flagManager = GetComponent<FlagManager>();
        uiManager = GetComponent<UiManager>();
    }

    // Update is called once per frame
    void Update()
    {
        while (union.paises.Count<4 && !played){
            ChooseCountries();
        }
        if(!played){
            
            foreach (var p in union.paises.ToList()){
                GetGreens(p);
            }
            foreach (var p in inimigos.ToList()){
                GetReds(p);
            }
            flagManager.PlayAnims(greens, reds);
            played = true;
            greens.Clear();
            reds.Clear();
        }
        if(!isChoosing){
            timer += Time.deltaTime;
        }
        if (timer > 10.0f){
            uiManager.HideMatsAnims();
            uiManager.HideExpAnims();
            Debug.Log("Nova ação");
            NewAction();
            timer = 0.0f;
            union.CalcResCarvao();
            union.CalcResMadeira();
            union.CalcResOuro();
            union.CalcResPetroleo();
            union.UpdateMaterialImportance();
            day += 1;
            uiManager.UpdateSliders();
            uiManager.PlayMatsAnims();
            foreach(Pais p in union.paises){
                foreach( Pais i in union.paises){
                    if(i!=p){
                        p.respeito += CalcRespeitoAlterado(i)/2;
                    }
                }
            }
            foreach(Pais p in inimigos){
                foreach( Pais i in union.paises){
                    p.respeito += CalcRespeitoAlterado(i)/2;
                }
            }
        }
        if (inimigos.Count==0){
            uiManager.GameOver(0);
        }
        if (union.paises.Count==0){
            uiManager.GameOver(1);
        }
    }

    void ChooseCountries(){
        int x = UnityEngine.Random.Range(0,inimigos.Count);
        if (union.paises.Count == 0){
            Debug.Log("Oi");
            if(inimigos[x].respeito<50){
                inimigos[x].respeito = 55;
            }
            union.paises.Add(inimigos[x]);
            inimigos.RemoveAt(x);
        }
        else if (union.paises.Count>0 && union.paises.Count<4){
            int a = 0;
            foreach(Pais p in union.paises.ToList()){
                foreach(Pais v in p.vizinhos.ToList()){
                    if(v==inimigos[x]){
                        a++;
                    }
                }
            }
            if(a>0){
                union.paises.Add(inimigos[x]);
                inimigos.RemoveAt(x);
            }
        }
        
    }

    public void NewAction(){
        int prob = UnityEngine.Random.Range(0,101);
        
        if(prob < 80){
            foreach (Pais p in union.paises.ToList()){
                if (p.respeito<=20){
                    UpdateRespeito(p, 2);
                    break;
                }
            }
        }
        if(prob > 80){
            isChoosing = true;
            int x = UnityEngine.Random.Range(0,inimigos.Count);
            if(inimigos[x].respeito<30){
                //Pais inimigo vai atacar
                List<Pais> targets = new();
                foreach(Pais p in union.paises.ToList()){
                    foreach(Pais v in p.vizinhos.ToList()){
                        if(v==inimigos[x]){
                            targets.Add(p);
                        }
                    }
                }
                if(targets.Count>0){
                    int target = UnityEngine.Random.Range(0,targets.Count);
                    Mats mate;
                    int q = Mathf.Min((int)(inimigos[x].madeiraP*union.GetMadeira().baseImportance), Mathf.Min((int)(inimigos[x].petroleoP*union.GetPetroleo().baseImportance), Mathf.Min((int)(inimigos[x].carvaoP*union.GetCarvao().baseImportance), (int)(inimigos[x].ouroP*union.GetOuro().baseImportance))));
                    if(q==(int)(inimigos[x].madeiraP*union.GetMadeira().baseImportance)){
                        mate = union.GetMadeira();
                    }
                    else if(q==(int)(inimigos[x].carvaoP*union.GetCarvao().baseImportance)){
                        mate = union.GetCarvao();
                    }
                    else if(q==(int)(inimigos[x].petroleoP*union.GetPetroleo().baseImportance)){
                        mate = union.GetPetroleo();
                    }
                    else if(q==(int)(inimigos[x].ouroP*union.GetOuro().baseImportance)){
                        mate = union.GetOuro();
                    }
                    else {
                        mate = null;
                    }
                    uiManager.CountryFighting(inimigos[x].nome, targets[target].nome, mate.name);
                    currentCountry = inimigos[x];
                    attackedCountry = targets[target];
                    wantedMaterial = mate;
                    //UpdateRespeito(inimigos[x], 6, atacado: targets[target], mat: mate);
                }
            }
            else if(inimigos[x].respeito>75){
                //Pais inimigo vai pedir para se juntar
                Debug.Log("País "+ inimigos[x] + " quer juntar-se à união. O que vai fazer?");
                currentCountry = inimigos[x];
                uiManager.CountryJoining();
            }
        }

    }


    public void UpdateRespeito(Pais p, int action, Pais atacado = null, Mats mat = null, Mats troca = null){
        // action == 0 é quando um país se quer juntar à união e é aceite
        if(action==0){
            Debug.Log("Aceitaste!");
            int respeitoAlterado = CalcRespeitoAlterado(p);
            foreach(Pais pais in union.paises.ToList()){
                pais.respeito += respeitoAlterado;
            }
            union.paises.Add(p);
            inimigos.Remove(p);
            GetGreens(p);
            flagManager.PlayAnims(greens, reds);
            greens.Clear();
            Debug.Log("Respeito: " + respeitoAlterado);
            uiManager.CountryDecision(currentCountry.nome,0);
        }
        // action == 1 é quando um país se quer juntar à união e é recusado
        else if(action==1){
            Debug.Log("Recusaste!");
            int respeitoAlterado = CalcRespeitoAlterado(p);
            foreach(Pais pais in union.paises.ToList()){
                pais.respeito -= respeitoAlterado;
            }
            Debug.Log("Respeito: " + respeitoAlterado);
            uiManager.CountryDecision(currentCountry.nome,1);
        }
        // action == 2 é quando um país quer sair
        else if(action==2){
            isChoosing=true;
            int respeitoAlterado = CalcRespeitoAlterado(p);
            union.paises.Remove(p);
            inimigos.Add(p);
            foreach(Pais pais in union.paises.ToList()){
                pais.respeito -= respeitoAlterado;
            }
            uiManager.CountryLeaving(p.nome);
            GetReds(p);
            flagManager.PlayAnims(greens, reds);
            reds.Clear();
        }
        // action == 3 é quando um país é convidado
        else if(action==3){
            int respeitoAlterado = CalcRespeitoAlterado(p);
            if(p.respeito>50){
                foreach(Pais pais in union.paises.ToList()){
                    pais.respeito += respeitoAlterado;
                }
                union.paises.Add(p);
                inimigos.Remove(p);
                p.respeito+=5;
                Debug.Log("País aceita.");
                GetGreens(p);
                flagManager.PlayAnims(greens, reds);
                greens.Clear();
                uiManager.CountryDecision(currentCountry.nome,0);
            }
            else{
                foreach(Pais pais in union.paises.ToList()){
                    pais.respeito -= respeitoAlterado;
                }
                if(p.respeito<40){
                    p.respeito-=5;
                }
                Debug.Log("País recusa.");
                uiManager.CountryDecision(currentCountry.nome,1);
            }
        }
        
        else {
            int defesas = 0;
            foreach(Pais pais in union.paises.ToList()){
                defesas += (int)Math.Round(pais.defesas*0.10f);
            }
            defesas += (int)Math.Round(atacado.defesas*0.90f);
            // action == 4 é se oferece um material a um país em troco de nada
            if(action==4){
                if(defesas>=p.defesas){
                    foreach(Pais pais in union.paises.ToList()){
                        pais.respeito -= 10*union.materialImportance.IndexOf(mat);
                    }
                }
                else if(defesas<p.defesas){
                    foreach(Pais pais in union.paises.ToList()){
                        pais.respeito -= 5*union.materialImportance.IndexOf(mat);
                    }
                }
                int i = UnityEngine.Random.Range(0,11);
                if(mat.name=="ouro"){
                    union.RemoveFromReservaOuro((int)Math.Round(i/union.GetOuro().baseImportance));
                }
                else if(mat.name=="carvão"){
                    union.RemoveFromReservaCarvao(amount: (int)Math.Round(i/union.GetCarvao().baseImportance));
                }
                else if(mat.name=="petróleo"){
                    union.RemoveFromReservaPetroleo((int)Math.Round(i/union.GetPetroleo().baseImportance));
                }
                else if(mat.name=="madeira"){
                    union.RemoveFromReservaMadeira((int)Math.Round(i/union.GetMadeira().baseImportance));
                }
                uiManager.CountryGive();
            }
            // action == 5 é se retalia contra o país atacante
            else if (action == 5){
                if(defesas>=p.defesas){
                    foreach(Pais pais in union.paises.ToList()){
                        pais.respeito += 5*union.materialImportance.IndexOf(mat);
                        pais.defesas -= (int)Math.Round(pais.defesas*0.10f);
                    }
                    p.respeito += 10;
                    atacado.defesas=0;
                    p.carvaoP=0;
                    p.madeiraP=0;
                    p.ouroP=0;
                    p.petroleoP=0;
                    //adicionar outro país à união
                    uiManager.CountryFight(0);
                    uiManager.PlayExpAnims(p);
                }
                else if(defesas<p.defesas){
                    foreach(Pais pais in union.paises.ToList()){
                        pais.respeito -= 10*union.materialImportance.IndexOf(mat);
                        pais.defesas -= (int)Math.Round(pais.defesas*0.10f);
                    }
                    p.respeito += 5;
                    atacado.defesas=0;
                    atacado.carvaoP=0;
                    atacado.madeiraP=0;
                    atacado.ouroP=0;
                    atacado.petroleoP=0;
                    //remover país atacado da união
                    uiManager.CountryFight(1);
                    uiManager.PlayExpAnims(atacado);
                }
                
            }
            else if(action==6){
                if(union.materialImportance.IndexOf(mat)<union.materialImportance.IndexOf(troca)){
                    foreach(Pais pais in union.paises.ToList()) {
                        pais.respeito -= 5*union.materialImportance.IndexOf(troca);
                    }
                    uiManager.ShowEndTrading(0);
                }
                else if(union.materialImportance.IndexOf(mat)>union.materialImportance.IndexOf(troca)){
                    foreach(Pais pais in union.paises.ToList()) {
                        pais.respeito += 2*union.materialImportance.IndexOf(mat);
                    }
                    uiManager.ShowEndTrading(1);
                }
            }
        }
        
    }

    int CalcRespeitoAlterado(Pais p){
        int respeitoAlterado = 0;
        int c = 0;
        int o = 0;
        int m = 0;
        int pe = 0;
        if(p.carvaoP-p.carvaoG>0){
            c = 2;
        }
        else{
            c = -2;
        }
        if(p.ouroP-p.ouroG>0){
            o = 2;
        }
        else{
            o = -2;
        }
        if(p.petroleoP-p.petroleoG>0){
            pe = 2;
        }
        else{
            pe = -2;
        }
        if(p.madeiraP-p.madeiraG>0){
            m = 2;
        }
        else{
            m = -2;
        }
        respeitoAlterado = c*union.materialImportance.IndexOf(union.GetCarvao()) + o*union.materialImportance.IndexOf(union.GetOuro()) + pe*union.materialImportance.IndexOf(union.GetPetroleo()) + m*union.materialImportance.IndexOf(union.GetMadeira());
        return respeitoAlterado;
    }

    public void Invite(){
        isChoosing = true;
        uiManager.confirm.GetComponentInChildren<TMP_Text>().text = "Tem a certeza que pretende convidar " + currentCountry.nome + " para se juntar à união. Confirma?";
        uiManager.Confirm();
    }

    public void ConfirmInvite(){
        UpdateRespeito(currentCountry, 3);
        HideInvite();
    }

    public void HideInvite(){
        uiManager.HideConfirm();
    }

    public void AcceptCountry(){
        UpdateRespeito(currentCountry, 0);
        uiManager.HideCountryJoining();
        isChoosing=false;
    }

    public void RefuseCountry(){
        UpdateRespeito(currentCountry, 1);
        uiManager.HideCountryJoining();
        isChoosing=false;
    }
    
    public void Fight(){
        UpdateRespeito(currentCountry, 5,attackedCountry,wantedMaterial);
    }

    public void Trade(){
        uiManager.ShowTrading();
    }

    public void TradeB1(){
        if(uiManager.t1.GetComponentInChildren<TMP_Text>().text=="Ouro"){
            UpdateRespeito(currentCountry,6, attackedCountry, wantedMaterial, union.GetOuro());
        }
        else if(uiManager.t1.GetComponentInChildren<TMP_Text>().text=="Carvão"){
            UpdateRespeito(currentCountry,6, attackedCountry, wantedMaterial, union.GetCarvao());
        }
        else if(uiManager.t1.GetComponentInChildren<TMP_Text>().text=="Petróleo"){
            UpdateRespeito(currentCountry,6, attackedCountry, wantedMaterial, union.GetPetroleo());
        }
        else if(uiManager.t1.GetComponentInChildren<TMP_Text>().text=="Madeira"){
            UpdateRespeito(currentCountry,6, attackedCountry, wantedMaterial, union.GetMadeira());
        }
        
    }
    public void TradeB2(){
        if(uiManager.t2.GetComponentInChildren<TMP_Text>().text=="Ouro"){
            UpdateRespeito(currentCountry,6, attackedCountry, wantedMaterial, union.GetOuro());
        }
        else if(uiManager.t2.GetComponentInChildren<TMP_Text>().text=="Carvão"){
            UpdateRespeito(currentCountry,6, attackedCountry, wantedMaterial, union.GetCarvao());
        }
        else if(uiManager.t2.GetComponentInChildren<TMP_Text>().text=="Petróleo"){
            UpdateRespeito(currentCountry,6, attackedCountry, wantedMaterial, union.GetPetroleo());
        }
        else if(uiManager.t2.GetComponentInChildren<TMP_Text>().text=="Madeira"){
            UpdateRespeito(currentCountry,6, attackedCountry, wantedMaterial, union.GetMadeira());
        }
    }
    public void TradeB3(){
        if(uiManager.t3.GetComponentInChildren<TMP_Text>().text=="Ouro"){
            UpdateRespeito(currentCountry,6, attackedCountry, wantedMaterial, union.GetOuro());
        }
        else if(uiManager.t3.GetComponentInChildren<TMP_Text>().text=="Carvão"){
            UpdateRespeito(currentCountry,6, attackedCountry, wantedMaterial, union.GetCarvao());
        }
        else if(uiManager.t3.GetComponentInChildren<TMP_Text>().text=="Petróleo"){
            UpdateRespeito(currentCountry,6, attackedCountry, wantedMaterial, union.GetPetroleo());
        }
        else if(uiManager.t3.GetComponentInChildren<TMP_Text>().text=="Madeira"){
            UpdateRespeito(currentCountry,6, attackedCountry, wantedMaterial, union.GetMadeira());
        }
    }
    

    
    public void Give(){
        UpdateRespeito(currentCountry, 4,attackedCountry,wantedMaterial);
    }
    void GetGreens(Pais p){
        if (p.gameObject.name.Contains("1")){
            greens.Add(item: 0);
            p.ChangeOgColor(0);
        }
        else if (p.gameObject.name.Contains("2")){
            greens.Add(item: 1);
            p.ChangeOgColor(0);
        }
        else if (p.gameObject.name.Contains("3")){
            greens.Add(item: 2);
            p.ChangeOgColor(0);
        }
        else if (p.gameObject.name.Contains("4")){
            greens.Add(item: 3);
            p.ChangeOgColor(0);
        }
        else if (p.gameObject.name.Contains("5")){
            greens.Add(item: 4);
            p.ChangeOgColor(0);
        }
        else if (p.gameObject.name.Contains("6")){
            greens.Add(item: 5);
            p.ChangeOgColor(0);
        }
        else if (p.gameObject.name.Contains("7")){
            greens.Add(item: 6);
            p.ChangeOgColor(0);
        }
        else if (p.gameObject.name.Contains("8")){
            greens.Add(item: 7);
            p.ChangeOgColor(0);
        }
    }

    void GetReds(Pais p){
        if (p.gameObject.name.Contains("1")){
            reds.Add(item: 0);
            p.ChangeOgColor(1);
        }
        else if (p.gameObject.name.Contains("2")){
            reds.Add(item: 1);
            p.ChangeOgColor(1);
        }
        else if (p.gameObject.name.Contains("3")){
            reds.Add(item: 2);
            p.ChangeOgColor(1);
        }
        else if (p.gameObject.name.Contains("4")){
            reds.Add(item: 3);
            p.ChangeOgColor(1);
        }
        else if (p.gameObject.name.Contains("5")){
            reds.Add(item: 4);
            p.ChangeOgColor(1);
        }
        else if (p.gameObject.name.Contains("6")){
            reds.Add(item: 5);
            p.ChangeOgColor(1);
        }
        else if (p.gameObject.name.Contains("7")){
            reds.Add(item: 6);
            p.ChangeOgColor(1);
        }
        else if (p.gameObject.name.Contains("8")){
            reds.Add(item: 7);
            p.ChangeOgColor(1);
        }
    }

    public void End(){
        uiManager.endgame.SetActive(false);
        SceneManager.LoadScene(1);
    }
}
