using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EMainMenuButton
{
    MAIN_MENU_PLAY, HELP, EXIT, RESTART,
    PAUSE,
    START_GAME, WIN, FAIL, NEXT,
	//Level
	LEVEL1, LEVEL2, LEVEL3, LEVEL4,
	LEVEL5, LEVEL6, LEVEL7, LEVEL8,
	LEVEL9, LEVEL10, LEVEL11, LEVEL12,
	LEVEL13, LEVEL14, LEVEL15, LEVEL16,
	LEVEL17, LEVEL18, LEVEL19, LEVEL20,
	LEVEL21, LEVEL22, LEVEL23, LEVEL24
}

public enum EFailType
{
    NotPointTarget, //first time can't take true target
    Duration, // duration play the game too long
    HandsUp, // hand up when draw the line
    CrossLine, // cross the limit line
    Backward, // point start and then back to start again
    PointEndTarget, // point not 1st object but on the end object
	HitBlocker, // hit blocker, had intention to cheat
	MidpointSkipped // Midpoint object not crossed
}

public enum EObjectTarget
{
    START_POINT, END_POINT, OBSTACLE, BLOCKER, MID_POINT, FREE_POINT
}

public enum GameplayType
{
    LINE_DRAW_MECHANIC,
    FIND_SHAPE_MECHANIC,
    FIND_SHAPE_MECHANIC2,
    FIND_THE_DIFFERENCE_MECHANIC,
    LINE_DRAW_MECHANIC_IGNORE_START_END_POINT
}