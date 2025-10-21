using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;

public class template : MonoBehaviour
{
    //public KMBombInfo Bomb;
    public KMAudio Audio;
    public AudioClip pressSound;
    public GameObject[] Buttons;
    public GameObject[] people;
    public GameObject[] pair;
    

    static int ModuleIdCounter = 1;
    int ModuleId;
    private bool ModuleSolved;

    private int stage = 0;
    
    private string[] names =
    {
        "Astro",
        "Bassie",
        "Bobette",
        "Blot",
        "Brusha",
        "Boxten",
        "Brightney",
        "Cocoa",
        "Connie",
        "Cosmo",
        "Dandy",
        "Dyle",
        "Eclipse",
        "Eggson",
        "Finn",
        "Flutter",
        "Flyte",
        "Gigi",
        "Ginger",
        "Glisten",
        "Goob",
        "Looey",
        "Poppy",
        "R&D",
        "Ribecca",
        "Rodger",
        "Rudie",
        "Scraps",
        "Shrimpo",
        "Shelly",
        "Soulvester",
        "Sprout",
        "Teagan",
        "Tisha",
        "Vee",
        "Yatta"
    };

    public Texture[] toons;
    public TextMesh currentNames;
    public TextMesh stageCounter;
    
    private sbyte[][] data =
    {
        new sbyte[]
        {
            0, 0, 0, 0, 5, 2, 0, -1, 2, 4, -4, 0, 0, 3, 2, 1, -1, 0, 4, 5, 4, 1, 3, 0, 4, 0, 2, -5, 3, 0, 2, 3, 4, -3,
            2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, -2, 3, -4, -4, 0, 0, 4, 1, 1, -5,
            0, 5, 5, 3, 3, 5, 0, 3, 0, 4, -5, 3, 0, 2, -3, 5, -4, -1, 0, 2, 3, -3, -5, 0, 0, 2, 2, 2, 2, 0, 3, 2, 2, 3,
            3, 0, 2, 0, 3, -4, 4, 0, 2, 2, 4, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 2, -1, 3, 0, 0, 3, 2, 1, 3, 0, 2, 1, 3, 2, 1, 0, 1, 0, 1, -3, 2, -20, 1, 1, 1, 2, 2, 1, -1,
            0, 0, 3, 1, 1, -2, -20, 3, 3, 4, 2, 2, 0, 2, 0, 2, -5, 1, 0, 5, -2, 3, -2, -2, -3, 0, 0, 2, -4, -4, -1, 0,
            1, 2, 2, -2, -5, 0, 3, 0, -3, -5, -2, 0, 1, -1, -1, -5, -3, 0, 0, -1, -3, -3, 4, 0, 2, 1, 1, -3, -4, 0, 5,
            0, 1, -4, -2, 0, 1, 3, -5, 3, -5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 3, 1, 0, 3, 4, 5, 2, -1, 0, -2, 0, 4, 5, 2,
            0, 3, -3, 2, -1, 4, -20, -2, 0, -1, 2, -3, 1, 3, 0, -3, 0, 2, -5, 1, 0, 1, 2, 1, -1, -2, -3, 0, -1, 2, -3,
            1, 3, 0, -3, 0, 2, -5, 1, 0, 1, 1, 1, -1, -2, 0, -4, -3, -3, 2, -4, 0, 4, 0, -4, -2, 2, 0, 2, 3, -1, 2, -4,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 3, 2, 4, 0, 5, 0, 1, -5, -2, 0, 3, 3, 2, -2, 2, 3, 3,
            4, 0, -1, 0, -20, -5, 3, 0, 4, 1, 2, -1, 1, 1, 2, 0, -2, 0, 3, -1, -1, 0, 2, 1, 2, -3, 5, 1, 0, 1, 0, 3, -5,
            4, 0, -1, 2, 3, 1, 3, 0, 2, 0, 1, -5, 1, 0, 1, 2, 3, -2, -5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, -4, 3,
            0, 2, 3, 1, -5, -5, 0, 0, 0, 0, 0, 0, 0, 0, 0, -5, 2, 0, 1, 3, 4, 1, 4, -5, 0, -2, 4, -5, -3, -4, 0, 2, 2,
            3, 4, 3, 0, 0, 0, 0, 0, 1, 2, -2, -1, 1, -3, -2, 1, -4, -5
        },
        new sbyte[]
        {
            3, 1, -1, 0, -1, 0, 0, 2, -2, -4, -2, 1, -1, -1, 0, 0, 1, 1, -2, -2, -1, 0, -3, -1, -1, -1, 1, -2, 5, 0, 0,
            0, 1, 4, 2, 0, 1, -1, 0, 0, -5, 0, 0, 0, 0, 0, -2, 1, 1, 3, 0, 1, 0, 0, 1, -2, -1, -2, 0, 0, 1, 0, -1, 0, 3,
            0, 0, -1, 0, 0, 0, 0, 0, 0, 1, 0, 2, 0, 0, -2, 0, 0, 0, 1, -3, -3, 0, 0, -1, -1, 0, 0, 0, -1, 0, -2, 0, 1,
            0, 0, -4, 0, 1, -2, 1, 0, 0, -1, 0, -3, 0, 0, 0, 2, -2, 2, 0, -1, 0, 0, 0, 0, 1, 0, 0, 1, 1, 0, 0, 0, 1, 2,
            1, 4, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, -2, 1, -2, -2, 0, 1, 0, -2, -1, -1, 2, -1, 0, -4, -1, 0,
            2, 1, 0, -1, -2, 0, 1, -3, 1, -2, -2, -1, 0, 0, -1, 0, 3, -2, 1, -2, 1, 0, -1, -3, 0, 0, 1, 3, -1, 1, 0, -1,
            2, 0, 0, 0, 1, 1, 0, 2, 1, -2, 2, 0, -1, -2, 2, 0, 2, 1, -1, -2, -2, 1, -1, -2, -2, -3, 0, -2, 0, 0, -1, 0,
            -2, 2, -1, 1, 0, -2, -2, 1, 1, 0, 0, 1, -1, 3, -1, 1, 1, 0, 2, -3, 0, 0, 2, 1, -2, -1, 0, 0, 1, 2, 2, 2, 0,
            0, 0, 4, 0, 0, 1, 0, 1, 0, -2, 0, -20, 0, -2, -2, -3, -1, 0, 0, 1, 0, -2, 0, 0, 0, -20, -1, 0, 0, 0, 0, 0,
            -2, 0, 1, 0, 0, 0, -5, 0, 0, -1, 1, 1, -2, 1, 0, 1, 0, -1, 0, -2, 0, 0, -2, -2, 0, 0, 1, 0, 0, -2, 0, -1, 0,
            0, -2, 0, 0, 0, -3, -3, 1, 0, 0, 1, 0, -1, -2, 0, 0, 1, 0, -2, 0, 0, -1, 0, 2, 0, -1, 0, -2, 1, 0, 1, 0, 0,
            0, 3, 1, -1, 0, 0, -2, 3, 1, 1, -1, 1, 1, 0, 0, 0, 0, 0, 0, 0, -1, -2, 0, 0, 0, 0, 1, -2, 0, 0, 0, 0, 0, 0,
            0, 1, 1, 0, 0, 2, 0, 4, 2, 0, -1, 0, 0, 0, 1, 0, 0, 2, -3, 1, 0, 0, -2, 0, 0, 1, -20, 1, 0, -1, 2, 0, 0, 0,
            0, 0, 1, 0, -1, 0, 0, 0, 0, 0, 0, 0, 2, 2, -2, 0, 1, 0, 0, 2, -1, -2, 3, 0, 0, 0, 0, 0, 2, 1, 1, 0, 0, 1, 2,
            0, 1, 1, 0, 2, 0, 3, 0, 3, 1, -2, -1, -1, 0, -1, 0, 0, -1, 1, 0, 0, 2, 0, 0, 0, 3, 4, -3, 0, 0, 0, -1, 0, 1,
            0, 0, -1, -2, -1, -3, -1, 0, -1, 1, 1, 0, 0, 1, 2, -1, 0, -3, 0, -20, 0, 0, 0, 0, -1, 0, 0, 2, 2, -3, 1, 1,
            0, 2, -2, 1, 0, 1, -2, 1, 2, 4, -1, 0, 0, 1, 0, 0, -1, 1, 0, 0, 0, 0, 0, 0, -3, 0, 0, -2, 0, 0, -1, 0, 0, 0,
            0, 0, 1, 0, 0, -1, 0, 0, -1, 0, -1, 0, 0, -3, -4, 0, 0, 0, 4, 2, 0, -1, 2, -2, 1, 0, 0, -2, 1, 0, 1, 1, 0,
            2, 1, -3, 0, -2, 0, -3, 0, -2, 1, 1, -3, 1, 2, 3, 0, -4, -5, 0, 0, 1, 1, 3, 2, -2, 0, 3, 2, -1, 0, 0, -2, 0,
            0
        },
        new sbyte[]
        {
            0, 0, 4, -3, 2, 3, 0, 0, 3, 2, 1, 0, 0, 0, 0, 1, -1, 1, 0, 5, 1, 0, 4, 0, -3, 0, 0, -2, 5, 0, 2, 0, 0, 3, 0,
            0, 0, -5, 0, 0, -5, 0, 0, -1, 0, 0, 0, 0, 0, 0, -1, 0, -1, 1, 1, 0, 0, 0, -1, 1, 1, -4, 3, 0, 2, 1, 1, 0, 0,
            0, -5, 0, 1, 1, 0, 1, -1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 1, 0, 0, 0, 0, 1, 0, 0, 1, 0, 1, 0, 0, 0, 0, -1, 5,
            0, 0, 0, 2, 0, 1, 0, 0, 2, 0, 0, 0, 0, 0, 1, 4, 0, 4, 0, 0, 0, 0, 0, 0, 0, 1, 0, 2, 0, 1, -5, -3, -4, -2,
            -5, -5, -5, 0, -3, -5, -1, -4, -3, -4, -5, -5, -4, 0, -4, 0, -4, -4, -5, -5, -4, 0, -3, -3, -5, -4, -2, 1,
            0, 0, 3, -2, 0, 0, 0, 1, 0, 0, -1, 0, 4, 3, 2, 1, 3, 0, 0, 0, 0, -2, 0, 0, 0, 0, 4, -1, 0, 0, 3, 1, 0, 0, 0,
            0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, -1, 1, 0, 0, 3, 1, 3, 0, 0, 1, -1, 0, 0, 0, 0, 0, 0, 0, 1, 0,
            1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 2, -2, 0, 0, 0, 0, 0, 2, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, -20, 0, 0, 0, 0, 0, -2, 0, 0, 0, 1, 0, 0, 0, -20, 0, 1, 5, 0, 1, 0, -2, 0, 0, 0, 2, 0, 5, 1, 0, 0, 1, 2,
            0, -1, -1, 0, 0, -4, 0, -1, -5, -2, 0, -1, 0, -1, -1, -1, -4, 0, 0, -4, -4, -2, -4, -1, 0, 0, 1, 0, 0, -1,
            0, 1, -4, 0, 0, 0, 0, 0, 0, 0, -2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -1, 0, -1, 1, 3,
            0, 1, 0, 0, 0, 0, 5, 1, 0, 0, 0, 0, 0, 0, -20, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -1, 0, 0, 0, 0, 0, -1, 0, -1, -2, -1, 0, -4, -3,
            -3, 0, -2, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 2, 0, 0, 0, 2, 1, -1, 0, -1, 0, 3, 0, 0, -2, -3, 0, -1, 1, 0,
            -1, 0, 1, 0, 2, 0, 1, 1, -20, 1, 3, 0, 2, 0, 0, -3, 0, 1, 4, 0, 0, 3, 0, -3, 1, 4, 5, 0, 0, -1, 2, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -5, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, -2, -1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, -3, 1, 0, 0, 0, 1, 0, 4, -3, 0, -2, -3, -3, -4, -4, 0, 0,
            2, 3, -4, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, -5, 0, 0
        },
        new sbyte[]
        {
            -1, -2, 3, -3, -1, 2, -2, 0, 0, 4, 0, -1, 0, -1, 1, 0, -2, 0, 0, 1, 1, 1, 5, -2, -5, -3, 0, -5, 3, 0, 1, 1,
            -3, 4, -1, 3, -1, -4, 0, 0, -5, -1, 0, 4, 0, -2, 0, 1, 1, 1, 2, -1, 0, 1, -2, 2, 0, -1, -4, -2, -1, -5, 2,
            0, 2, 3, -2, -1, 0, 0, -3, 0, 0, 1, -1, 1, 2, 0, -2, -1, 0, 1, 0, 0, 4, 0, 1, 0, 1, -1, 0, -2, -1, 4, -5,
            -1, 0, 0, 1, -1, 0, 0, 2, 2, -1, 1, 1, 0, -1, -2, 1, -1, -3, 1, 0, 1, 0, -4, 1, 4, 2, 3, 0, 0, -3, -3, -5,
            -1, 0, 0, 0, -4, 3, 3, -2, -2, -2, -4, -2, -2, -1, -3, -2, -1, 1, -2, 0, -3, -2, -2, -3, -2, -3, -5, -1, -2,
            -5, -3, -1, -2, -2, 3, 5, 0, -3, 1, 0, 0, 4, 1, 0, 0, 0, 1, 1, 0, -1, 1, 5, 1, 1, 3, 0, 0, 0, -2, 0, -5, 0,
            0, 1, 1, 0, 2, 0, -1, 5, 0, 0, 2, 0, 0, 2, 2, 0, -1, 0, -1, 1, 0, 1, 1, 0, 2, -2, 2, -5, 1, 0, 0, 0, 0, 4,
            0, 1, 2, -1, -1, 1, 1, 0, 1, 0, 0, 3, 2, 0, 0, 0, 0, -1, 0, -2, 1, -5, 0, 0, 1, 0, 0, 0, 1, 0, -2, 0, 2, -1,
            -2, 2, 0, 4, 0, 3, 0, 0, 1, 0, 1, -2, -3, 1, -5, 1, -20, -2, -4, -1, 1, 3, -3, 0, 0, -1, 0, 2, -1, 1, -20,
            1, 1, 0, 0, 0, 0, -3, -2, 0, -5, 0, 0, 4, 1, 0, 0, 0, 0, 0, -1, 0, 1, 0, -2, -1, -2, -4, 0, -1, 0, -2, -4,
            -2, 0, -5, -4, 0, -4, -5, 1, -4, -3, 0, 2, 3, 1, 0, 0, -1, 2, -4, -2, 0, 0, -1, 3, -2, 0, -5, 2, 0, 1, 0, 1,
            0, -1, 0, 0, 1, 0, 1, 0, 0, 3, -1, 1, 0, 0, 0, -2, 1, -5, 0, 1, -2, 0, -2, -1, 1, -1, 0, -1, 0, -1, -1, -2,
            0, -1, 0, -2, 1, -3, -1, -5, -1, -1, -1, 0, -1, -1, -1, 1, 0, 0, 0, -4, 3, 4, 1, -1, -2, 0, -2, 1, 4, 0, 2,
            0, 0, -3, 0, 2, -20, 4, 1, 1, 1, 1, 1, 1, 3, 1, -2, 1, -5, 1, 1, 1, 1, 1, 1, 1, 0, 3, 0, 0, 0, 0, 0, 0, 0,
            -2, 0, -5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -1, 2, 0, 3, 0, -2, 3, -5, 0, -2, -1, -1, -2, -3, 1, 0, 0, 0, 1, 0,
            1, 0, -1, 2, -5, -1, 3, 3, 1, 0, 0, 0, 1, 0, 0, 4, -2, 5, -2, 0, -5, -3, 0, 0, 1, 1, 2, -1, -1, 1, -1, 0,
            -2, -1, -20, -5, 0, 0, 1, 0, 0, 1, 2, 4, 1, -1, 0, -2, 0, -5, -1, 0, 4, 0, 0, -1, 4, 0, 0, 0, -2, 1, -5, -2,
            0, 1, 0, 0, 0, 0, -1, 1, -2, 0, -5, 0, 0, 0, 0, 0, 3, 0, -1, -3, 3, -5, 1, -1, -2, -1, -2, -3, 1, -2, 0, -5,
            1, 0, -4, 1, 3, -5, -3, -2, -5, -2, -2, -2, -2, -2, -4, -1, -5, 0, 0, -1, 0, -3, -5, 1, -5, -5, -5, -5, -5,
            -5, -5, 0, 2, 1, 4, -2, 2, 1, 1, 1, -1, 0, -1, -3, 3, 0, 1, 1, 0, -5, -1, -2
        },
        new sbyte[]
        {
            0, -1, 0, 0, 2, 0, 0, -1, 3, 5, -1, 0, 0, 2, -5, -5, 1, 0, -2, 5, 0, -1, -2, 0, -2, -1, -2, -3, 5, 0, -3,
            -4, 3, 0, 0, -2, 0, 0, 0, 0, -5, 0, 0, 0, 0, 0, -2, 0, -5, -5, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, -2, 0, 0, 1, 0, 0, 0, 0, 0, -5, -5, 0, 2, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 5, 0, 0, 5, 0,
            0, 0, 0, 0, -5, 0, 0, 0, 0, 0, 0, 0, 3, -5, -5, 0, 0, 0, 0, 5, 3, -3, 0, 0, 0, -5, 0, 0, 0, 0, 0, -5, 0, 5,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -5, -5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 0, 0, 4, 5, 3, 5,
            0, -3, 0, 0, 4, -5, -5, 3, 5, 5, 4, 5, 5, 5, 0, -4, 5, -2, 0, 4, 0, 4, 5, 5, -1, 0, 0, 0, 0, -5, 0, 0, 5, 0,
            -5, -5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -5, 3, 0, 0, 5, 4, 1, 0, 0, 0, 0, 0, 0, 0, 0, -5, -5, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 4, -5, -5, 5, 5, 5, 0, 4, 5, 4, 0, -5, 5, 0, -3,
            0, -20, 0, 4, 4, 3, 0, -5, -5, 0, 0, -1, -5, -5, 0, -20, -3, -4, 0, 3, 0, 0, -5, 0, 0, 0, 0, 0, 5, -3, 3,
            -3, 0, 0, 0, 0, -4, -5, -5, -4, -5, 0, 0, -4, -5, 0, 0, 0, 0, 0, -5, -3, 0, 0, 0, 0, 0, 0, 0, 0, 0, -5, -5,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -5, -5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, -5, -5, 0, 0, 0, 0, 0, 0, 0, 0, -3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -5, -5, 4, 0, -3,
            0, 4, 3, 0, 0, -5, 3, 0, 5, 4, 0, -1, 0, 0, 0, 0, -20, -5, -5, -5, -5, -5, -5, -5, -5, -5, -5, -5, -5, -5,
            -5, -5, -5, -5, -5, -5, -5, -5, -5, -5, -5, -5, -5, -5, -5, -5, -5, -5, -5, -5, -5, -5, -5, -5, -5, 0, -3,
            0, 0, 0, 0, 0, 0, 0, 0, -5, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -4, 0,
            3, 3, 0, -5, -3, 0, -3, 0, 0, 0, 5, 0, 0, 0, 4, 0, 0, 0, -5, 0, -20, 5, 4, 0, 0, 0, 0, -3, 2, 5, 5, 0, -5,
            5, 0, -3, 0, 0, 5, -2, 0, -5, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 0, 0, 0, -5, 0, 0, -5, 0, 0, -4, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -5, -5, -5, -5, -5, -5, 5, 0, -5, -5, -5, -4, 0, 0, 0, 5, -4, 0, 0, -5,
            0, 0, 0, 0, 0, 0, 0, -3, 0, 0, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -2, -3, 0, 0, 5, 0, 0, 0, 0, 0
        }
    };

    private string expectedAnswer = "";
    private string enteredAnswer = "";
    private string screenText = "";
    private int preffered = -1;
    private string buttonFunctions = "01234.56789";
    
void Awake()
    {
        ModuleId = ModuleIdCounter++;
        for (int i = 0; i < 5; i++)
        {
            Buttons[i].GetComponent<MeshRenderer>().material.color = new Color(0,1, 0);
            Buttons[10-i].GetComponent<MeshRenderer>().material.color = new Color(1, 0, 0);
        }
        Buttons[5].GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1);
        for (int i = 0; i < 11; i++)
        {
            int i1 = i;
            Buttons[i1].GetComponent<KMSelectable>().OnInteract += delegate
            {
                press(i1);
                return false;
            };
        }
    }

    void send()
    {
        if (enteredAnswer == expectedAnswer)
        {
            if (stage == 5)
            {
                GetComponent<KMBombModule>().HandlePass();
                pair[0].GetComponent<MeshRenderer>().material.mainTexture = null;
                pair[1].GetComponent<MeshRenderer>().material.mainTexture = null;
                pair[0].GetComponent<MeshRenderer>().material.color = Color.black;
                pair[1].GetComponent<MeshRenderer>().material.color = Color.black;
                currentNames.text = "Solved!";
                for (int i=0; i<5; i++)people[i].GetComponent<MeshRenderer>().material.color = Color.white;
                ModuleSolved = true;
            }
            else generateOneStage();
        }
        else
        {
            GetComponent<KMBombModule>().HandleStrike();
        }
    }

    void press(int button)
    {
        Audio.HandlePlaySoundAtTransform(pressSound.name, transform);
        if (ModuleSolved) return;
        if ((enteredAnswer.Contains('.')||enteredAnswer == "") && button == 5) return;
        if (enteredAnswer == "")
        {
            enteredAnswer += button < 5 ? "+" : "-";
        } 
        else if (enteredAnswer.Length == 6)
        {
            if (button < 5) send();
            enteredAnswer = "";
        }
        else
        {
            enteredAnswer+=buttonFunctions[button].ToString();
        }
        if (!ModuleSolved) currentNames.text = enteredAnswer == "" ? screenText : enteredAnswer;
        
    }

    sbyte pairOfPerson(int a, int b, int person)
    {
        if (a == b) return -6;
        if (a > b) a = a ^ b ^ (b = a);
        int index = 35 * 36 / 2 - (35 - a) * (36 - a) / 2 + b - a - 1;
        return data[person][index];
    }

    int[] getRandomPair()
    {
        int midans = -6, a=-1, b=-1;
        while (midans == -6)
        {
            a = Rnd.Range(0, 36);
            b = Rnd.Range(0, 36);
            int sum = 0;
            for (int i = 0; i < 5; i++)
                sum+=Math.Abs(pairOfPerson(a,b,i));
            if (sum > 4 && sum<99) midans = pairOfPerson(a,b,0);
        }
        return new[] { a, b };
    }

    string floatToString(decimal value)
    {
        int num0 = (int)(value * 1000);
        int num = Math.Abs(num0);
        string ans = num / 1000 + (num % 1000 < 10 ? ".00" : num % 1000 < 100 ? ".0" : ".") + num % 1000;
        return (num0<0?"-":"+") + ans;
    }
    
    void generateOneStage()
    {
        stage++;
        stageCounter.text = stage + " / 5";
        int[] pairStage = getRandomPair();
        if (Rnd.Range(0, 20) < 8) preffered = Rnd.Range(0, 5);
        else preffered = -1;
        for (int i = 0; i < 5; i++)
        {
            people[i].GetComponent<MeshRenderer>().material.color = (i==preffered)?new Color(1, 1, 1):new Color(.5f,.5f,.5f);
        }
        List<sbyte> list = new List<sbyte>();
        for (int i = 0; i < 5; i++)
        {
            list.Add(pairOfPerson(pairStage[0],pairStage[1],i));
            if (i == preffered)
            {
                list.Add(pairOfPerson(pairStage[0],pairStage[1],i));
                list.Add(pairOfPerson(pairStage[0],pairStage[1],i));
            }
        }
        decimal ans = Averages.Average(list, stage);
        expectedAnswer = floatToString(ans);
        Debug.LogFormat("[Ship Review #{0}] Expected answer for {2}({4})/{3}({5}) is: {1}", 
            ModuleId, expectedAnswer,names[pairStage[0]],names[pairStage[1]],pairStage[0],pairStage[1]
            );
        pair[0].GetComponent<MeshRenderer>().material.mainTexture = toons[pairStage[0]];
        pair[1].GetComponent<MeshRenderer>().material.mainTexture = toons[pairStage[1]];
        
        screenText = names[pairStage[0]] + "\n" + names[pairStage[1]];
        currentNames.text = screenText;
    }
    
    void Start()
    {
        generateOneStage();
    }

#pragma warning disable 414
    private readonly string TwitchHelpMessage = @"Use !{0} <rate> to input the rate. For example: !{0} +2.450";
#pragma warning restore 414

    IEnumerator ProcessTwitchCommand(string Command)
    {
        yield return null;
        if (!Command.RegexMatch(@"[+-]\d\.\d\d\d")) yield return "sendtochaterror Invalid command!";
        else
        {
            for (int i = 0; i < Command.Length; i++)
            {
                switch (Command[i])
                {
                    case '+': case '0': press(0); break;
                    case '1': press(1); break;
                    case '2': press(2); break;
                    case '3': press(3); break;
                    case '4': press(4); break;
                    case '-': case '5': press(6); break;
                    case '6': press(7); break;
                    case '7': press(8); break;
                    case '8': press(9); break;
                    case '9': press(10); break;
                    case '.': press(5); break;
                    default: yield return "sendtochaterror Invalid command!"; break;
                }

                yield return new WaitForSeconds(0.15f);
            }
            press(0);
        }

        
    }

    IEnumerator TwitchHandleForcedSolve()
    {
        yield return null;
        while (!ModuleSolved)
        {
            while (enteredAnswer != "")
            {
                press(6);
                yield return new WaitForSeconds(0.15f);
            } 
            yield return ProcessTwitchCommand(expectedAnswer);
        }
    }

}
