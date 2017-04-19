
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace AnyGame.Client.Controller
{


    /// <summary>
    /// Bag
    /// </summary>
    
    public abstract class BaseBagController
    {
internal abstract void OnUseItemResult(AnyGame.Client.Entity.Bags.UseItemResult result,int itemId,int lessCount);
internal abstract void OnSyncItems(AnyGame.Client.Entity.Common.SyncType type,AnyGame.Client.Entity.Bags.GameItem[] items);
internal abstract void OnSyncBag(int maxGridCount,AnyGame.Client.Entity.Bags.GameItem[] items);
internal abstract void OnSyncAllResouce(int money,int gem);
internal abstract void OnSyncResouce(int resId,int num);
internal abstract void OnUpgradeBagResult(AnyGame.Client.Entity.Bags.UpgradeBagResult result);
        
    }


    /// <summary>
    /// Game
    /// </summary>
    
    public abstract class BaseGameController
    {
internal abstract void OnSyncServerTime(DateTime serverTime,int id);
        
    }


    /// <summary>
    /// GameSystem
    /// </summary>
    
    public abstract class BaseGameSystemController
    {
internal abstract void OnGetSystemTimeResult(long time);
internal abstract void OnNotice(string noticeContext);
internal abstract void OnServerStatus(string title,string context,bool isNoticeOnce,bool isMaintain);
        
    }


    /// <summary>
    /// Login
    /// </summary>
    
    public abstract class BaseLoginController
    {
internal abstract void OnLoginServerResult(AnyGame.Client.Entity.Login.LoginServerResult result,bool isCreatedPlayer);
internal abstract void OnCreatePlayerResult(AnyGame.Client.Entity.Login.CraetePlayerResult result);
internal abstract void OnSyncInitDataFinish();
        
    }

}

