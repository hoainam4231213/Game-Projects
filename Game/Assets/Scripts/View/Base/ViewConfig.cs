using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum ViewIndex
{
    EmptyView = 0,
    HomeView = 1,
    WeaponView = 2,
    ShopView = 3,

}
public class ViewConfig 
{
    public static ViewIndex[] viewIndices =
    {
        ViewIndex.EmptyView,
        ViewIndex.HomeView,
        ViewIndex.WeaponView,
        ViewIndex.ShopView,
    };
}

public class ViewCallback
{
    public Action callback;
}

public class ViewParam
{

}

public class HomeViewParam : ViewParam
{

}

public class WeaponViewParam : ViewParam
{

}

