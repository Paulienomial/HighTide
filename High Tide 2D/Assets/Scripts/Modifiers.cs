using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modifiers : MonoBehaviour
{
    void Start()
    {
        Events.curr.afterHit+=hit;
        Events.curr.onPurchaseDefender+=purchaseDef;
        Events.curr.onUpgradeDefender+=upgradeDef;
        Events.curr.onSellDefender+=sellDef;
        Events.curr.onSetDefender+=setDef;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //add modifier if it's not already present, or if the new modifier is higher lvl than the old one
    public void tryAddModifier(Modifier d, GameObject g){
        Modifier oldModifier = findModifier(d, g);
        if( oldModifier==null ){//if modifier not already present
            d.add();//add modifier to warrior that was specified when creating the modifier
        }else{//if modifier present
            if(d.lvl > oldModifier.lvl){//if new modifier higher lvl thatn old modifier
                oldModifier.remove();//remove old modifier
                d.add();//add new modifier
            }
        }
    }

    public Modifier findModifier(Modifier d, GameObject g){
        foreach(Modifier wd in g.GetComponent<Warrior>().attributes.modifiers){
            if(wd.name==d.name){
                return wd;
            }
        }
        return null;
    }

    public Modifier findModifier(string n, GameObject g){
        foreach(Modifier wd in g.GetComponent<Warrior>().attributes.modifiers){
            if(wd.name==n){
                return wd;
            }
        }
        return null;
    }

    void hit(GameObject attacker, GameObject victim){
        Warrior attackerW = attacker.GetComponent<Warrior>();
        FightManager attackerF = attacker.GetComponent<FightManager>();
        Warrior victimW = victim.GetComponent<Warrior>();
        FightManager victimF = victim.GetComponent<FightManager>();
        
        if(attackerW.attributes.attackModifiers.Contains("Dragon slow")){
            DragonSlow slow = new DragonSlow("Dragon slow", attackerW.getLevel(), victim);
            tryAddModifier(slow, victim);
        }

        if(attackerW.attributes.attackModifiers.Contains("Bandit damage reduction")){
            BanditDamageReduction reduce = new BanditDamageReduction(attackerW.getLevel(), victim);
            tryAddModifier(reduce, victim);
        }

        if(attackerW.attributes.attackModifiers.Contains("Dragon damage reduction")){
            DragonDamageReduction reduce = new DragonDamageReduction(attackerW.getLevel(), victim);
            tryAddModifier(reduce, victim);
        }

    }

    void purchaseDef(GameObject g){
        if(AuraRangerBuffActive()){
            applyGlobalModifier("Aura ranger buff", topAuraRangerLvl());
        }

        applyLoneRangerBuff();
    }

    void upgradeDef(GameObject g, int i1, int i2){
        if(AuraRangerBuffActive()){
            applyGlobalModifier("Aura ranger buff", topAuraRangerLvl());
        }
        
        applyLoneRangerBuff();
    }

    void sellDef(GameObject g){
        //firstly remove old aura ranger buffs
        if(g.GetComponent<Warrior>().attributes.ability=="Aura ranger buff"){
            foreach(GameObject def in Global.curr.defenders){
                Modifier m = findModifier("Aura ranger buff", def);
                if(m!=null){
                    m.remove();
                }
            }
        }
        //re apply aura ranger buff to defenders
        if(AuraRangerBuffActive()){
            applyGlobalModifier("Aura ranger buff", topAuraRangerLvl());
        }

        applyLoneRangerBuff();
    }

    void setDef(GameObject g){
        if(AuraRangerBuffActive()){
            applyGlobalModifier("Aura ranger buff", topAuraRangerLvl());
        }
    }

    //tries to add a modifier to each defender
    void applyGlobalModifier(string mName, int lvl){
        if(mName=="Aura ranger buff"){
            foreach(GameObject def in Global.curr.defenders){
                AuraRangerBuff dmgBuff = new AuraRangerBuff(lvl, def);
                tryAddModifier(dmgBuff, def);
            }
        }
    }

    void tryAuraRangerBuff(GameObject g){
        Warrior w = g.GetComponent<Warrior>();
        applyGlobalModifier("Aura ranger buff", w.getLevel());
    }

    bool AuraRangerBuffActive(){
        foreach(GameObject def in Global.curr.defenders){
            if(findModifier("Aura ranger buff", def)!=null || def.GetComponent<Warrior>().attributes.ability=="Aura ranger buff"){
                return true;
            }
        }
        return false;
    }

    int topAuraRangerLvl(){
        int highestLvl=0;
        foreach(GameObject def in Global.curr.defenders){
            if(def.GetComponent<Warrior>().attributes.ability=="Aura ranger buff"){
                if(def.GetComponent<Warrior>().getLevel() > highestLvl){
                    highestLvl = def.GetComponent<Warrior>().getLevel();
                }
            }
        }
        return highestLvl;
    }

    void applyLoneRangerBuff(){
        Warrior loneRanger = onlyLoneRanger();
        if(loneRanger!=null){//if true rogue ranger, then try apply rogue ranger buff
            LoneRangerBuff buff = new LoneRangerBuff(loneRanger.getLevel(), loneRanger.gameObject);
            tryAddModifier(buff, loneRanger.gameObject);
        }else{//if no true rogue ranger, then remove rogue ranger buffs
            removeLoneRangerBuffs();
        }
    }
    public Warrior onlyLoneRanger(){
        int count=0;
        Warrior onlyLone=null;
        foreach(GameObject g in Global.curr.defenders){
            Warrior w = g.GetComponent<Warrior>();
            if(w.attributes.name.Contains("ranger")){
                count++;
            }
            if(w.attributes.name=="Rogue ranger"){
                onlyLone=w;
            }
            if(count>1) return null;
        }
        return onlyLone;
    }

    void removeLoneRangerBuffs(){
        foreach(GameObject def in Global.curr.defenders){
            Modifier mod = findModifier("Rogue ranger buff", def); 
            if( mod!=null ){
                mod.remove();
            }
        }
    }

}
