using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColoringLine : MonoBehaviour
{
    public int Group { get; private set; }
    public MeshRenderer MeshRenderer;
    public List<ParticleSystem> particles;

    public void Load(int g)
    {
        Group = g;
        Colorer.Recolor(MeshRenderer, g);
        for (int i = 0; i < particles.Count; i++)
            particles[i].startColor = Colorer.GetColor(g);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Track.Instance.Snake.Recolor(Group);
        }
    }
}
