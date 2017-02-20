using AnyGame.LoginPlugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using TradeAge.Client.Entity.Common;

namespace AnyGame.LoginPlugin
{
    public class AquaIvy : ILoginProxy
    {
        public PlatformTypes PlatformType
        {
            get { return PlatformTypes.AquaIvy; }
        }

        public event EventHandler<LoginSuccessEventArgs> LoginResult;

        public bool AutoLogin(string account)
        {
            throw new NotImplementedException();
        }

        public void Exit()
        {
            throw new NotImplementedException();
        }

        public void Login(string account)
        {
            throw new NotImplementedException();
        }

        public void Logoff()
        {
            throw new NotImplementedException();
        }

        public void SuccessLogin()
        {
            throw new NotImplementedException();
        }
    }
}

