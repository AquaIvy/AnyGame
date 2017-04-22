
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace AnyGame.Client.Controller
{


    /// <summary>
    /// Player
    /// </summary>
    
    public abstract class BasePlayerController
    {
internal abstract void OnUnlockGuideRecordResult(AnyGame.Client.Entity.Guide.GuideTypes type,bool isPass);
internal abstract void OnSyncGuideRecords(AnyGame.Client.Entity.Guide.GuideTypes[] records);
internal abstract void OnUnlockMenuResult(AnyGame.Client.Entity.Character.MenuTypes menu,bool isUnlock);
internal abstract void OnSyncUnlockMenus(AnyGame.Client.Entity.Character.MenuTypes[] menus);
internal abstract void OnPlayerRenameResult(AnyGame.Client.Entity.Character.RenameResultType result,string newName);
internal abstract void OnSyncPlayerInfo(string name,int sex,DateTime createTime,DateTime lastLoginTime,DateTime lastLogoffTime,int level,int exp,long expSum,int vipLevel);
internal abstract void OnSyncPlayerPropertyInfo(AnyGame.Client.Entity.Character.Property property);
        
    }


    /// <summary>
    /// Bag
    /// </summary>
    
    public abstract class BaseBagController
    {
internal abstract void OnUseItemResult(AnyGame.Client.Entity.Character.UseItemResult result,int itemId,int lessCount);
internal abstract void OnSyncItems(AnyGame.Client.Entity.Character.SyncType type,AnyGame.Client.Entity.Character.GameItem[] items);
internal abstract void OnSyncBag(int maxGridCount,AnyGame.Client.Entity.Character.GameItem[] items);
internal abstract void OnSyncAllResouce(int money,int gem);
internal abstract void OnSyncResouce(int resId,int num);
internal abstract void OnUpgradeBagResult(AnyGame.Client.Entity.Character.UpgradeBagResult result);
        
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
internal abstract void OnKickOfServer(AnyGame.Client.Entity.Login.OfflineType type);
internal abstract void OnSyncPlayerBaseInfo(int playerId,int gameZoonId,bool isSupperMan,int platformType);
        
    }

}

