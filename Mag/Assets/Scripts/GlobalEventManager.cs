using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalEventManager
{
    public static Action OnDeath;

    public static Action OnTic;

    public static Action OnUse;

    public static List<InteractiveObject> InteractiveObjects = new List<InteractiveObject>();


}
