
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
    AutoPause,
    AutoChangeTestCase,
    AutoTest,
    AutoUpdateWebpage

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

[System.Serializable]
public enum AdroitProfiler_AutomatedTester_AutoTestType
{
    Unselected = 0,
    FunctionShouldReturnTrue,
    FunctionShouldReturnFalse,
    PassAtTime,
    FailAtTime

}

[System.Serializable]
public enum AdroitProfiler_AutomatedTester_AutoClickerAnchorPoint
{
    Unselected = 0,
    UpperLeft,
    UpperMiddle,
    UpperRight,
    CenterLeft,
    CenterMiddle,
    CenterRight,
    BottomLeft,
    BottomMiddle,
    BottomRight

}
