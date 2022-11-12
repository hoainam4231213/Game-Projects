using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum DialogIndex
{
    CardDialog = 0,
    MessDialog = 1
}

public class DialogConfig
{
    public static DialogIndex[] dialogIndices =
    {
        DialogIndex.CardDialog,
        DialogIndex.MessDialog
    };
}

public class DialogCallback
{
    public Action callback;
}

public class DialogParam
{

}

public class CardDialogParam : DialogParam
{

}
