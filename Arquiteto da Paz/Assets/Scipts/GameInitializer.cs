using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

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

    FlagManager flagManager;

    private List<Pais> inimigos = new();

    private float timer = 0;
    
    private bool played = false;
    //Unity.Mathematics.Random rnd = new();
    public Uniao union;
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
        flagManager = GetComponent<FlagManager>();
    }

    // Update is called once per frame
    void Update()
    {
        while (union.paises.Count<4){
            ChooseCountries();
        }
        if(!played){
            List<int> greens = new List<int>();
            List<int> reds = new List<int>();
            foreach (var p in union.paises){
                if (p.gameObject.name.Contains("1")){
                    
                    greens.Add(item: 0);
                }
                else if (p.gameObject.name.Contains("2")){
                    greens.Add(item: 1);
                }
                else if (p.gameObject.name.Contains("3")){
                    greens.Add(item: 2);
                }
                else if (p.gameObject.name.Contains("4")){
                    greens.Add(item: 3);
                }
                else if (p.gameObject.name.Contains("5")){
                    greens.Add(item: 4);
                }
                else if (p.gameObject.name.Contains("6")){
                    greens.Add(item: 5);
                }
                else if (p.gameObject.name.Contains("7")){
                    greens.Add(item: 6);
                }
                else if (p.gameObject.name.Contains("8")){
                    greens.Add(item: 7);
                }
            }
            foreach (var p in inimigos){
                if (p.gameObject.name.Contains("1")){
                    //p.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                    reds.Add(item: 0);
                }
                else if (p.gameObject.name.Contains("2")){
                    //p.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                    reds.Add(item: 1);
                }
                else if (p.gameObject.name.Contains("3")){
                    //p.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                    reds.Add(item: 2);
                }
                else if (p.gameObject.name.Contains("4")){
                    //p.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                    reds.Add(item: 3);
                }
                else if (p.gameObject.name.Contains("5")){
                    //p.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                    reds.Add(item: 4);
                }
                else if (p.gameObject.name.Contains("6")){
                    //p.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                    reds.Add(item: 5);
                }
                else if (p.gameObject.name.Contains("7")){
                    //p.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                    reds.Add(item: 6);
                }
                else if (p.gameObject.name.Contains("8")){
                    //p.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                    reds.Add(item: 7);
                }
            }
            flagManager.PlayAnims(greens, reds);
            played = true;
        }
        timer += Time.deltaTime;
        if (timer > 10.0f){
            Debug.Log("Nova ação");
            NewAction();
            timer = 0.0f;
            union.CalcResCarvao();
            union.CalcResMadeira();
            union.CalcResOuro();
            union.CalcResPetroleo();
            union.UpdateMaterialImportance();
        }
    }

    void ChooseCountries(){
        int x = UnityEngine.Random.Range(0,inimigos.Count);
        if (union.paises.Count == 0){
            Debug.Log("Oi");
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

    void NewAction(){
        int prob = UnityEngine.Random.Range(0,101);
        if(prob > 80){
            
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
                    Debug.Log("País "+ inimigos[x] + " quer atacar o país " + targets[target] + ". O que vai fazer?");
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
                    UpdateRespeito(inimigos[x], 6, atacado: targets[target], mat: mate);
                }
            }
            else if(inimigos[x].respeito>75){
                //Pais inimigo vai pedir para se juntar
                Debug.Log("País "+ inimigos[x] + " quer juntar-se à união. O que vai fazer?");
                UpdateRespeito(inimigos[x], 1);
            }

        }

    }


    void UpdateRespeito(Pais p, int action, Pais atacado = null, Mats mat = null, Mats troca = null){
        // action == 0 é quando um país se quer juntar à união e é aceite
        if(action==0){
            Debug.Log("Aceitaste!");
            int respeitoAlterado = CalcRespeitoAlterado(p);
            foreach(Pais pais in union.paises){
                pais.respeito += respeitoAlterado;
            }
            union.paises.Add(p);
            Debug.Log("Respeito: " + respeitoAlterado);
        }
        // action == 1 é quando um país se quer juntar à união e é recusado
        else if(action==1){
            Debug.Log("Recusaste!");
            int respeitoAlterado = CalcRespeitoAlterado(p);
            foreach(Pais pais in union.paises){
                pais.respeito -= respeitoAlterado;
            }
            Debug.Log("Respeito: " + respeitoAlterado);
        }
        // action == 2 é quando um país é expulso
        else if(action==2){
            Debug.Log("Recusaste!");
            int respeitoAlterado = CalcRespeitoAlterado(p);
            union.paises.Remove(p);
            foreach(Pais pais in union.paises){
                pais.respeito -= respeitoAlterado;
            }
            p.respeito-=Math.Abs(respeitoAlterado)*2;
            Debug.Log("Respeito: " + respeitoAlterado);
        }
        // action == 3 é quando um país é convidado
        else if(action==3){
            int respeitoAlterado = CalcRespeitoAlterado(p);
            if(p.respeito>60){
                foreach(Pais pais in union.paises){
                    pais.respeito += respeitoAlterado;
                }
                union.paises.Add(p);
                p.respeito+=5;
            }
            else{
                foreach(Pais pais in union.paises){
                    pais.respeito -= respeitoAlterado;
                }
                if(p.respeito<40){
                    p.respeito-=5;
                }
            }
        }
        
        else {
            int defesas = 0;
            foreach(Pais pais in union.paises){
                defesas += (int)Math.Round(pais.defesas*0.10f);
            }
            defesas += (int)Math.Round(atacado.defesas*0.90f);
            // action == 4 é se oferece um material a um país em troco de nada
            if(action==4){
                if(defesas>=p.defesas){
                    foreach(Pais pais in union.paises){
                        pais.respeito -= 10*union.materialImportance.IndexOf(mat);
                    }
                }
                else if(defesas<p.defesas){
                    foreach(Pais pais in union.paises){
                        pais.respeito -= 5*union.materialImportance.IndexOf(mat);
                    }
                }
                if(mat.name=="ouro"){
                    union.RemoveFromReservaOuro(1);
                }
                else if(mat.name=="carvão"){
                    union.RemoveFromReservaCarvao(amount: 1);
                }
                else if(mat.name=="petróleo"){
                    union.RemoveFromReservaPetroleo(1);
                }
                else if(mat.name=="madeira"){
                    union.RemoveFromReservaMadeira(1);
                }
            }
            // action == 5 é se retalia contra o país atacante
            else if (action == 5){
                if(defesas>=p.defesas){
                    foreach(Pais pais in union.paises){
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
                }
                else if(defesas<p.defesas){
                    foreach(Pais pais in union.paises){
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
                }
            }
            else if(action==6){
                if(union.materialImportance.IndexOf(mat)<union.materialImportance.IndexOf(troca)){
                    foreach(Pais pais in union.paises) {
                        pais.respeito -= 5*union.materialImportance.IndexOf(troca);
                    }
                }
                else if(union.materialImportance.IndexOf(mat)>union.materialImportance.IndexOf(troca)){
                    foreach(Pais pais in union.paises) {
                        pais.respeito += 2*union.materialImportance.IndexOf(mat);
                    }
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
            c = -5;
        }
        if(p.ouroP-p.ouroG>0){
            o = 2;
        }
        else{
            o = -5;
        }
        if(p.petroleoP-p.petroleoG>0){
            pe = 2;
        }
        else{
            pe = -5;
        }
        if(p.madeiraP-p.madeiraG>0){
            m = 2;
        }
        else{
            m = -5;
        }
        respeitoAlterado = c*union.materialImportance.IndexOf(union.GetCarvao()) + o*union.materialImportance.IndexOf(union.GetOuro()) + pe*union.materialImportance.IndexOf(union.GetPetroleo()) + m*union.materialImportance.IndexOf(union.GetMadeira());
        return respeitoAlterado;
    }
}
