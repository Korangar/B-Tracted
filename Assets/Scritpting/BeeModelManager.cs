using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeModelManager : MonoBehaviour
{
    public List<Texture> body;
    public List<Texture> helmet;

    private Transform model;


    private BeeBehaviour bee;


    public void SetModel(int id)
    {
        if (model == null)
            model = transform.GetChild(0);


        model.Find("Helmet1").GetComponent<SkinnedMeshRenderer>().material.mainTexture = helmet[id];
        model.Find("Worker_Bee").GetComponent<SkinnedMeshRenderer>().material.mainTexture = body[id];


    }

    internal void SetWarrior(bool v)
    {
        if (model == null)
            model = transform.GetChild(0);

        model.Find("Helmet1").GetComponent<SkinnedMeshRenderer>().enabled = v;
    }
}
