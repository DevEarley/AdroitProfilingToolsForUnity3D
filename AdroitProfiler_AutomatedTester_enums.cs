
[System.Serializable]
public enum AdroitProfiler_AutomatedTester_Configuration_MovementType
{
    Unselected = 0,
    MoveForward,
    MoveBackwards,
    TurnLeft,
    TurnRight
}

[System.Serializable]
public enum AdroitProfiler_AutomatedTester_Configuration_Type
{
    Unselected = 0,
    AutoBroadcaster,
    AutoChangeScene,
    AutoChooseDialogChoice,
    AutoClicker,
    AutoClickTarget,
    AutoLookAt,
    AutoMover,
    AutoMoveTo,
    AutoTeleportTo,
}

[System.Serializable]
public enum AdroitProfiler_AutomatedTester_DialogOptions
{
    Unselected = 0,
    SortAlphabetically_PickFirst,
    SortAlphabetically_PickLast,
    SortAlphabetically_PickAtIndex,
    Unsorted_PickFirst,
    Unsorted_PickLast,
    Unsorted_PickAtIndex,
    PickRandom
}
