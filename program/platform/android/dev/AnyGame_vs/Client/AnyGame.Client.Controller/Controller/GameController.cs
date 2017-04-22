using DogSE.Client.Core;
using AnyGame.Client.Controller.Bag;
using AnyGame.Client.Controller.Login;
using AnyGame.Client.Entity;
using AnyGame.Client.Controller.GameSystem;
using AnyGame.Client.Controller.Player;

namespace AnyGame.Client.Controller
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
            System = new GameSystemController(this, Net);
            Game = new Game.GameController(Net);

            Player = new PlayerController(this, Net);
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
        /// GM控制器
        /// </summary>
        public GameSystemController System { get; private set; }

        /// <summary>
        /// 游戏的控制器
        /// </summary>
        public Game.GameController Game { get; private set; }

        /// <summary>
        /// 玩家角色的控制器
        /// </summary>
        public PlayerController Player { get; private set; }

        /// <summary>
        /// 背包控制器
        /// </summary>
        public BagController Bag { get; private set; }

    }
}
