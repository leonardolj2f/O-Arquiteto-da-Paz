using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    GameInitializer gameInitializer;

    public TMP_Text dayText;

    public TMP_Text paisToShow;

    public Button button;
    public Button t1;
    public Button t2;
    public Button t3;

    public GameObject confirm;

    public GameObject leaving;
    public GameObject fighting;

    public GameObject joining;

    public GameObject trading;
    public GameObject endgame;
    public Slider sliderOuro;
    public Slider sliderCarvao;
    public Slider sliderPetroleo;
    public Slider sliderMadeira;

    public GameObject matsp1;
    public GameObject matsp2;
    public GameObject matsp3;
    public GameObject matsp4;
    public GameObject matsp5;
    public GameObject matsp6;
    public GameObject matsp7;
    public GameObject matsp8;
    public GameObject exp1;
    public GameObject exp2;
    public GameObject exp3;
    public GameObject exp4;
    public GameObject exp5;
    public GameObject exp6;
    public GameObject exp7;
    public GameObject exp8;

    // Start is called before the first frame update
    void Start()
    {
        gameInitializer = GetComponent<GameInitializer>();
    }

    // Update is called once per frame
    void Update()
    {
        dayText.text = "Day " + gameInitializer.day;
    }

    // Sets the hover text to display
    public bool SetHoverText(Pais country)
    {
        if(!gameInitializer.isChoosing){
            gameInitializer.currentCountry = country;
            gameInitializer.p1.UpdateColor();
            gameInitializer.p2.UpdateColor();
            gameInitializer.p3.UpdateColor();
            gameInitializer.p4.UpdateColor();
            gameInitializer.p5.UpdateColor();
            gameInitializer.p6.UpdateColor();
            gameInitializer.p7.UpdateColor();
            gameInitializer.p8.UpdateColor();
            if (paisToShow != null)
            {
                foreach(Pais p in gameInitializer.inimigos){
                    if(country.nome==p.nome){
                        button.gameObject.SetActive(true);
                        break;
                    }
                    else{
                        button.gameObject.SetActive(false);
                    }
                }
                paisToShow.text = country.nome;
                paisToShow.gameObject.SetActive(true); // Show the text
            }
            return true;
        }
        return false;
    }

    // Clears the hover text
    public void ClearHoverText()
    {
        if (paisToShow != null)
        {
            paisToShow.text = "";
            paisToShow.gameObject.SetActive(false); // Hide the text
            gameInitializer.p1.UpdateColor();
            gameInitializer.p2.UpdateColor();
            gameInitializer.p3.UpdateColor();
            gameInitializer.p4.UpdateColor();
            gameInitializer.p5.UpdateColor();
            gameInitializer.p6.UpdateColor();
            gameInitializer.p7.UpdateColor();
            gameInitializer.p8.UpdateColor();
            
        }
    }

    public void Confirm(){
        button.gameObject.SetActive(false);
        confirm.SetActive(true);
    }
    public void HideConfirm(){
        confirm.SetActive(false);
    }

    public void CountryLeaving(string name){
        button.gameObject.SetActive(false);
        ClearHoverText();
        leaving.SetActive(true);
        leaving.GetComponentInChildren<TMP_Text>().text = "O país " + name + " decidiu abandonar a união, porque não tem gostado da sua liderança.";
    }

    public void HideCountryLeaving(){
        leaving.SetActive(false);
        gameInitializer.isChoosing=false;
    }

    public void CountryDecision(string name, int i){
        button.gameObject.SetActive(false);
        ClearHoverText();
        leaving.SetActive(true);
        if(i==0){
            leaving.GetComponentInChildren<TMP_Text>().text = "O país " + name + " juntou-se à união.";
        }
        else{
            leaving.GetComponentInChildren<TMP_Text>().text = "O país " + name + " não se juntou à união.";
        }
        
    }

    public void CountryJoining(){
        button.gameObject.SetActive(false);
        ClearHoverText();
        joining.SetActive(true);
        joining.GetComponentInChildren<TMP_Text>().text = "O país " + gameInitializer.currentCountry.nome + " quer juntar-se à união.";
    }

    public void HideCountryJoining(){
        joining.SetActive(false);
    }

    public void CountryFighting(string ataque, string atacado, string mat){
        button.gameObject.SetActive(false);
        ClearHoverText();
        fighting.SetActive(true);
        fighting.GetComponentInChildren<TMP_Text>().text = "O país " + ataque + " quer atacar " + atacado + ", para capturar " + mat + ". O que vai fazer?";
    }

    public void CountryGive(){
        fighting.SetActive(false);
        leaving.SetActive(true);
        leaving.GetComponentInChildren<TMP_Text>().text = "Decidiste oferecer o material que " + gameInitializer.currentCountry.nome + " pretendia.";
        UpdateSliders();    
    }

    public void CountryFight(int i){
        fighting.SetActive(false);
        leaving.SetActive(true);
        if(i==0){
            leaving.GetComponentInChildren<TMP_Text>().text = "Decidiste defender o interesse dos países da união e lutar contra " + gameInitializer.currentCountry.nome + ". Felizemente, ganhaste!";
        }
        else{
            leaving.GetComponentInChildren<TMP_Text>().text = "Decidiste defender o interesse dos países da união e lutar contra " + gameInitializer.currentCountry.nome + ". Infelizemente, perdeste!";
        }  
    }

    public void ShowTrading(){
        fighting.SetActive(false);
        trading.SetActive(true);
        trading.GetComponentInChildren<TMP_Text>().text = "Qual material queres trocar?";
        if(gameInitializer.wantedMaterial.name.Contains("ouro")){
            gameInitializer.possibleMats.Remove("Ouro");
            t1.GetComponentInChildren<TMP_Text>().text = gameInitializer.possibleMats[0];
            t2.GetComponentInChildren<TMP_Text>().text = gameInitializer.possibleMats[1];
            t3.GetComponentInChildren<TMP_Text>().text = gameInitializer.possibleMats[2];
            gameInitializer.possibleMats.Add("Ouro");
        }
        else if(gameInitializer.wantedMaterial.name.Contains("carvão")){
            gameInitializer.possibleMats.Remove("Carvão");
            t1.GetComponentInChildren<TMP_Text>().text = gameInitializer.possibleMats[0];
            t2.GetComponentInChildren<TMP_Text>().text = gameInitializer.possibleMats[1];
            t3.GetComponentInChildren<TMP_Text>().text = gameInitializer.possibleMats[2];
            gameInitializer.possibleMats.Add("Carvão");
        }
        else if(gameInitializer.wantedMaterial.name.Contains("petróleo")){
            gameInitializer.possibleMats.Remove("Petróleo");
            t1.GetComponentInChildren<TMP_Text>().text = gameInitializer.possibleMats[0];
            t2.GetComponentInChildren<TMP_Text>().text = gameInitializer.possibleMats[1];
            t3.GetComponentInChildren<TMP_Text>().text = gameInitializer.possibleMats[2];
            gameInitializer.possibleMats.Add("Petróleo");
        }
        else if(gameInitializer.wantedMaterial.name.Contains("madeira")){
            gameInitializer.possibleMats.Remove("Madeira");
            t1.GetComponentInChildren<TMP_Text>().text = gameInitializer.possibleMats[0];
            t2.GetComponentInChildren<TMP_Text>().text = gameInitializer.possibleMats[1];
            t3.GetComponentInChildren<TMP_Text>().text = gameInitializer.possibleMats[2];
            gameInitializer.possibleMats.Add("Madeira");
        }
    }

    public void ShowEndTrading(int i){
        trading.SetActive(false);
        leaving.SetActive(true);
        if(i==0){
            leaving.GetComponentInChildren<TMP_Text>().text = "Fizeste uma boa troca!";
        }
        else{
            leaving.GetComponentInChildren<TMP_Text>().text = "Fizeste uma troca prejudicial para a União!";
        }  
    }

    public void UpdateSliders(){
        if(gameInitializer.union.GetReservaOuro()*gameInitializer.union.GetOuro().baseImportance/1000>=1){
            sliderOuro.value = 1;
        }else{
            sliderOuro.value = gameInitializer.union.GetReservaOuro()*gameInitializer.union.GetOuro().baseImportance/1000;
        }
        if(gameInitializer.union.GetReservaCarvao()*gameInitializer.union.GetCarvao().baseImportance/1000>=1){
            sliderCarvao.value = 1;
        }else{
            sliderCarvao.value = gameInitializer.union.GetReservaCarvao()*gameInitializer.union.GetCarvao().baseImportance/1000;
        }
        if(gameInitializer.union.GetReservaPetroleo()*gameInitializer.union.GetPetroleo().baseImportance/1000>=1){
            sliderPetroleo.value = 1;
        }else{
            sliderPetroleo.value = gameInitializer.union.GetReservaPetroleo()*gameInitializer.union.GetPetroleo().baseImportance/1000;
        }
        if(gameInitializer.union.GetReservaMadeira()*gameInitializer.union.GetMadeira().baseImportance/1000>=1){
            sliderMadeira.value = 1;
        }else{
            sliderMadeira.value = gameInitializer.union.GetReservaMadeira()*gameInitializer.union.GetMadeira().baseImportance/1000;
        }
        
    }

    public void GameOver(int i){
        gameInitializer.p1.gameObject.SetActive(false);
        gameInitializer.p2.gameObject.SetActive(false);
        gameInitializer.p3.gameObject.SetActive(false);
        gameInitializer.p4.gameObject.SetActive(false);
        gameInitializer.p5.gameObject.SetActive(false);
        gameInitializer.p6.gameObject.SetActive(false);
        gameInitializer.p7.gameObject.SetActive(false);
        gameInitializer.p8.gameObject.SetActive(false);
        if(i==0){
            endgame.SetActive(true);
            endgame.GetComponentInChildren<TMP_Text>().text = "O jogo chegou ao fim! Foste capaz de alcançar a Paz Mundial ao juntar todos os Países na União! Clica em Continuar para voltar ao Menu Inicial.";
        }
        else if(i==1){
            endgame.SetActive(true);
            endgame.GetComponentInChildren<TMP_Text>().text = "O jogo chegou ao fim! As tuas decisões levaram ao abandono de todos os países e ao fim da União! Clica em Continuar para voltar ao Menu Inicial e tentar novamente.";
        }
    }

    public void PlayMatsAnims(){
        foreach(Pais p in gameInitializer.union.paises.ToList()){
            if (p.gameObject.name.Contains("1")){
                matsp1.SetActive(true);
            }
            else if (p.gameObject.name.Contains("2")){
                matsp2.SetActive(true);
            }
            else if (p.gameObject.name.Contains("3")){
                matsp3.SetActive(true);
            }
            else if (p.gameObject.name.Contains("4")){
                matsp4.SetActive(true);
            }
            else if (p.gameObject.name.Contains("5")){
                matsp5.SetActive(true);
            }
            else if (p.gameObject.name.Contains("6")){
                matsp6.SetActive(true);
            }
            else if (p.gameObject.name.Contains("7")){
                matsp7.SetActive(true);
            }
            else if (p.gameObject.name.Contains("8")){
                matsp8.SetActive(true);
            }
        }
    }
    public void HideMatsAnims(){
        matsp1.SetActive(false);
        matsp2.SetActive(false);
        matsp3.SetActive(false);
        matsp4.SetActive(false);
        matsp5.SetActive(false);
        matsp6.SetActive(false);
        matsp7.SetActive(false);
        matsp8.SetActive(false);
    }

    public void PlayExpAnims(Pais p){
        if (p.gameObject.name.Contains("1")){
            exp1.SetActive(true);
        }
        else if (p.gameObject.name.Contains("2")){
            exp2.SetActive(true);
        }
        else if (p.gameObject.name.Contains("3")){
            exp3.SetActive(true);
        }
        else if (p.gameObject.name.Contains("4")){
            exp4.SetActive(true);
        }
        else if (p.gameObject.name.Contains("5")){
            exp5.SetActive(true);
        }
        else if (p.gameObject.name.Contains("6")){
            exp6.SetActive(true);
        }
        else if (p.gameObject.name.Contains("7")){
            exp7.SetActive(true);
        }
        else if (p.gameObject.name.Contains("8")){
            exp8.SetActive(true);
        }
    }

    public void HideExpAnims(){
        exp1.SetActive(false);
        exp2.SetActive(false);
        exp3.SetActive(false);
        exp4.SetActive(false);
        exp5.SetActive(false);
        exp6.SetActive(false);
        exp7.SetActive(false);
        exp8.SetActive(false);
    }
}
