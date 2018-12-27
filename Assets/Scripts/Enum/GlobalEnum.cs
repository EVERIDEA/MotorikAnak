using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EMainMenuButton
{
    MAIN_MENU_PLAY, HELP, EXIT, RESTART,
    PAUSE,
    START_GAME, WIN, FAIL
}

public enum EFailType
{
    NotPointTarget, //first time can't take true target
    Duration, // duration play the game to long
    HandsUp, // hand up when draw the line
    CrossLine, // cross the limit line
    Backward, // point start and then back to start again
    PointEndTarget // point not 1st object but on the end object
}
public enum EObjectTarget
{
    START_POINT, END_POINT
}