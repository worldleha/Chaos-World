using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour
{
    public LifeGenerate lifeGenerate;
    public List<Character> lives;
    public PlayerControl player;
    // Start is called before the first frame update
    private void Awake()
    {
        lives = new List<Character>();
    }


    public void RandomGenerateLifeBasePlanet(Planet planet)  {

        int count = 50;
        for (int i = 0; i < count; i++)
        {
            Slam slam = lifeGenerate.RandomGenerateLife<Slam>(planet, "Slam");
            slam.Init();
            lives.Add(slam);
        }
    }

    public void Save()
    {
        LifeSave.SaveLives(lives.ToArray());
    }
    public void Load()
    {
        SerCharacterData[] cd = FileOperator.CharacterDataLoad();
        foreach(SerCharacterData cdItem in cd)
        {
            Character c = lifeGenerate.GenerateLife(cdItem);
            lives.Add(c);
            if(cdItem.characterName != "Player")
                c.Init();
            else
            {
                player = (PlayerControl)c;
            }
        }
    }
    public PlayerControl GeneratePlayer(Planet planet) {

        player = lifeGenerate.RandomGenerateLife<PlayerControl>(planet, "Player");
        lives.Add(player);
        return player;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
