using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Levels { TitleScreen, House, BattleScene };
public enum TransitionAnimations { FadeIn }
public enum StatType { PATTACK, MATTACK, PDEFENSE, MDEFENSE }
public enum Look { Up,Left,Right,Down}
public enum SceneName { TitleScreen, House , ManagerScene , BattleScene }

public enum ArmorType { Helmet,Chest}
public enum Places { grass, inside, desert, cave } //???????????????

public enum PopUpNames
    { GearInfo, // Use in character ui view to show stats from different gear.
      SaveSettings,
      ItemDescription,
      PlayerTarget,


    }

public enum UIEvents {InventoryArrowEndAnimation, }

public enum GameEvents
{   Pause,
    Unpause,
    Freeze,
    Unfreeze,
    StartMessage,
    EndMessage,
    TeleportLocal,
    Teleport,
    StopWalking,
    TeleportingLocal,
    NextTurn,

 
}
public static class Utilities
{
    public static void SetUpButtonNavigation(Button buttonToChange, Button up, Button down, Button left, Button right)
    {
        var newNavigation = new Navigation();
        newNavigation.mode = Navigation.Mode.Explicit;
        newNavigation.selectOnUp = up;
        newNavigation.selectOnRight = right;
        newNavigation.selectOnDown = down;
        newNavigation.selectOnLeft = left;
        buttonToChange.navigation = newNavigation;
    }
}
