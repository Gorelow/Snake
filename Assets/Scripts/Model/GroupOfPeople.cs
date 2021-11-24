using UnityEngine;

public class GroupOfPeople : MonoBehaviour 
{
    public int Group;
    public static int Amount;
    public static float Range;
    public static float MinDist;

    public void Activate(int g)
    {
        Group = g;
        Vector3[] People = new Vector3[Amount];
        bool tooClose;
        int LoopCheck = 0;

        for (int i = 0; i < Amount; i++)
        {
            var human = Instantiate(Prefabs.Human, transform);
            do
            {
                if (LoopCheck > 1000)
                {
                    Debug.LogError("It's taking too much to find a free place for another person");
                    break;
                }
                tooClose = false;
                People[i] = new Vector3(Random.Range(-Range, Range), 0, Random.Range(-Range, Range));
                for (int j = 0; j < i; j++)
                {
                    if ((People[j] - People[i]).magnitude < MinDist)
                    {
                        tooClose = true;
                        break;
                    }
                }
                LoopCheck++;
            } while (tooClose);

            human.transform.localPosition += People[i];
            Colorer.Recolor(human.GetComponent<MeshRenderer>(),g);
            human.model.Group = g;
            human.SetActive(true);
        }
    }
}
