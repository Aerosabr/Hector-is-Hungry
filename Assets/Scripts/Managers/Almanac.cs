using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Almanac : MonoBehaviour
{
    public static Almanac instance;

    //Region Glossary
    public List<GameObject> Region1 = new List<GameObject>();
    public List<GameObject> Region2 = new List<GameObject>();
    public List<GameObject> Region3 = new List<GameObject>();

    private void Awake()
    {
        instance = this;
    }
}
