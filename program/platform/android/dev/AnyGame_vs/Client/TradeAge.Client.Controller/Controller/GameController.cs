using DogSE.Client.Core;
using TradeAge.Client.Controller.Bag;
using TradeAge.Client.Controller.Login;
using TradeAge.Client.Entity;

namespace TradeAge.Client.Controller
{
    /// <summary>
    /// 游戏控制器
    /// </summary>
    public class GameController
    {
        /// <summary>
        /// 游戏控制器
        /// </summary>
        public GameController()
        {
            Model = new EntityModel();

            Net = new NetController();
            Net.Tag = this;

            Login = new LoginController(this, Net);
            Game = new Game.GameController(Net);
            Bag = new BagController(this, Net);

        }

        /// <summary>
        /// 游戏里用到数据
        /// </summary>
        public EntityModel Model { get; private set; }

        /// <summary>
        /// 游戏控制器的网络控制器部分
        /// </summary>
        public NetController Net { get; set; }

        /// <summary>
        /// 登陆相关的接口在这里
        /// </summary>
        public LoginController Login { get; private set; }

        /// <summary>
        /// 游戏的控制器
        /// </summary>
        public Game.GameController Game { get; private set; }

        /// <summary>
        /// 背包控制器
        /// </summary>
        public BagController Bag { get; private set; }
    }
}
