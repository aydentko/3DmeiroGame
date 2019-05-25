using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class st_humanpos : MonoBehaviour {
    static public bool clear_25=false;

    public static void cont()
    {
        clear_25 = true;
    }

    public static bool getclear_25()
    {
        return clear_25;
    }

}
