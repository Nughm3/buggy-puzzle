using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
    int X = -1;
    int[,] levelTemplate;
    int[,] level1, level2, level3, level4, level5, level6, level7, level8, level9, level10;

    string[,] emptyDialogue = new string[62,35];

    void StoreEntities() {
        level1 = new int[35, 62] {
           //0                                       //21                                      //42                                  //61
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}, //0
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,X,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,0,0,0,0,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}, //12
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,0,0,0,0,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}, //24
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0} //34
        };

        level2 = new int[35, 62] {
           //0                                       //21                                      //42                                  //61
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}, //0
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,0,0,0,0,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}, //12
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0},
            {0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,0,0,0,0,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}, //24
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,1,0,0,0,2,0,0,0,1,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0} //34
        };

        levelTemplate = new int[35, 62] {
           //0                                       //21                                      //42                                  //61
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}, //0
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,0,0,0,0,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}, //12
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,0,0,0,0,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X,X},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}, //24
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,X,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0} //34
        };
    }

    float posx;
    float posy;
    float tileSize = 0.8f;
    public GameObject enemyPrefab;
    public GameObject personPrefab;
    public GameObject instantiatedObject;
    Vector2 spawnPos;

    string[,] dialogue1, dialogue2, dialogue3, dialogue4, dialogue5, dialogue6, dialogue7, dialogue8, dialogue9, dialogue10 = new string[35, 62];

    public void SpawnEntities(int level) {
        int[,] currentLevel = new int[35, 62];
        if (level == 1) currentLevel = level1;
        if (level == 2) currentLevel = level2;

        for (int y = 0; y < currentLevel.GetLength(0); y += 1) {
            for (int x = 0; x < currentLevel.GetLength(1); x += 1) {
                if (currentLevel[y, x] == 1) {
                    posx = -24.4f + (x * tileSize);
                    posy = 13.6f - (y * tileSize);
                    spawnPos = new Vector2(posx, posy);
                    Instantiate(enemyPrefab, spawnPos, transform.rotation);
                }
                if (currentLevel[y, x] == 2) {
                    posx = -24.4f + (x * tileSize);
                    posy = 13.6f - (y * tileSize);
                    spawnPos = new Vector2(posx, posy);
                    instantiatedObject = Instantiate(personPrefab, spawnPos, transform.rotation);
                    instantiatedObject.GetComponent<Person>().tilePos = new Vector2(x,y);
                    if (level == 1) instantiatedObject.GetComponent<Person>().dialogue = dialogue1[x, y];
                    if (level == 2) instantiatedObject.GetComponent<Person>().dialogue = dialogue2[x, y];
                }
            }
        }
    }

    public void RemoveEnemies() {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies) {
            Destroy(enemy);
        }
        var alerts = GameObject.FindGameObjectsWithTag("Alert");
        foreach (var alert in alerts) {
            Destroy(alert);
        }
    }

    public void RemovePeople() {
        var people = GameObject.FindGameObjectsWithTag("Person");
        foreach (var person in people) {
            Destroy(person);
        }
    }

    public void AssignDialogue(int[] code) {
        dialogue1 = emptyDialogue;
        dialogue2 = emptyDialogue;
        dialogue3 = emptyDialogue;
        dialogue4 = emptyDialogue;
        dialogue5 = emptyDialogue;
        dialogue6 = emptyDialogue;
        dialogue7 = emptyDialogue;
        dialogue8 = emptyDialogue;
        dialogue9 = emptyDialogue;
        dialogue10 = emptyDialogue;

        dialogue1[28,17] = $"The 3rd digit is {code[2]}.";
        dialogue1[39,1] = $"The 2nd digit is {code[1]}.";
        dialogue1[4,9] = $"The 4th digit is {code[3]}.";
        dialogue1[31,29] = $"The 1st digit is {code[0]}.";

        dialogue2[31,33] = $"The 1st digit is {code[0]}.";
    }

    void Awake() {
        StoreEntities();
    }
}
