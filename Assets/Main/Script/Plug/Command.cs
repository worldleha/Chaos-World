
using UnityEditor;
using UnityEngine;

public class Command : MonoBehaviour
{
    public Transform dropPoint;
    public BackpackContainer container;
    public PlayerAnimation playerAnimation;
    public string id = "1";
    public string lifeName = "Slam";

    private bool flag = false;
    private void OnGUI()
    {
        id = GUI.TextField(new Rect(10, 75, 128, 30), id);
        if (GUI.Button(new Rect(10, 10, 128, 60), "Add Item")){

            Debug.Log(container);
            container.AddItem(ItemGenerate.GenerateItemById(int.Parse(id)).item2D);
        }
        lifeName = GUI.TextField(new Rect(10, 180, 128, 30), lifeName);
        if (GUI.Button(new Rect(10, 120, 128, 60), "Add Life"))
        {

            GameObject slam = Instantiate(PrefabManager.GetPrefabManager().GetLivesByName(lifeName));
            slam.GetComponent<CharacterData>().position = dropPoint.position;
            slam.GetComponent<Slam>().Init();
            slam.transform.localScale = Vector3.one*Random.Range(0.2f,3f);

        }
        if (GUI.Button(new Rect(140, 10, 128, 60), "Change Speed"))
        {
            if (flag) {playerAnimation.SetAnimationMoveSpeed(1); flag = false;}
        else{ playerAnimation.SetAnimationMoveSpeed(3);flag = true;}    
        }

       
    }
    

}
