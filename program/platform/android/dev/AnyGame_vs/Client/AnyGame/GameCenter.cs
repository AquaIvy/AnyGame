using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TradeAge.Client.Controller;
using TradeAge.Client.Entity;

namespace AnyGame
{
    /// <summary>
    /// 游戏的逻辑处理中心
    /// </summary>
    public static class GameCenter
    {
        private static readonly GameController m_controller = new GameController();
        private static readonly EntityModel m_entity = m_controller.Model;
        private static readonly GlobalInfo m_info = new GlobalInfo();

        public static GameController Controller
        {
            get { return m_controller; }
        }

        public static EntityModel Entity
        {
            get { return m_entity; }
        }

        public static GlobalInfo Info
        {
            get { return m_info; }
        }
    }
}
