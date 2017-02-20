
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace TradeAge.Client.Controller
{


    /// <summary>
    /// Bag
    /// </summary>
    
    public abstract class BaseBagController
    {
internal abstract void OnUseItemResult(TradeAge.Client.Entity.Bags.UseItemResult result,int itemId,int lessCount);
internal abstract void OnSyncBag(int MaxCount,int CurCount);
        
    }


    /// <summary>
    /// Game
    /// </summary>
    
    public abstract class BaseGameController
    {
internal abstract void OnSyncServerTime(DateTime serverTime,int id);
        
    }


    /// <summary>
    /// Login
    /// </summary>
    
    public abstract class BaseLoginController
    {
internal abstract void OnLoginServerResult(TradeAge.Client.Entity.Login.LoginServerResult result,bool isCreatePlayer);
internal abstract void OnCreatePlayerResult(TradeAge.Client.Entity.Login.CraetePlayerResult result);
internal abstract void OnSyncInitDataFinish();
        
    }

}

