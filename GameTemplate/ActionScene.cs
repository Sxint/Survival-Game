using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Survive;

namespace GameTemplate
{
    //this is the actual game itself
    public class ActionScene : GameScene
    {
        private SpriteBatch spriteBatch;
        private Game1 g;
        //declare all game related components
        private Player player;
        private Platform platform, platform2;
        public CollisionManager Collisionmanager;
        private Enemy enemy;
        private List<Platform> platforms = new List<Platform>();
        public EnemyCollisionManager enemyCollisionManager;

        public ActionScene(Game game) : base(game)
        {
            g = (Game1)game;
            this.spriteBatch = g._spriteBatch;
            platforms = new List<Platform>();
            Texture2D batTex = g.Content.Load<Texture2D>("images/player");
            Texture2D playerTex = g.Content.Load<Texture2D>("images/marine");
            Texture2D enemyTex = g.Content.Load<Texture2D>("images/zerglig");
            SoundEffect jumpSound = g.Content.Load<SoundEffect>("songs/jump");
            Vector2 playerPos = new Vector2(Shared.stage.X / 2 - batTex.Width / 2,Shared.stage.Y - playerTex.Height);
            Vector2 enemyPos = new Vector2((Shared.stage.X / 2 - batTex.Width / 2) + 1100 , Shared.stage.Y - enemyTex.Height);
            Vector2 platformPos = new Vector2(200, Shared.stage.Y - batTex.Height);
            Vector2 platformPos2 = new Vector2(800, Shared.stage.Y - batTex.Height);

            platform2 = new Platform(game, spriteBatch, batTex, platformPos2);
            platform = new Platform(game, spriteBatch, batTex, platformPos);
            platforms.Add(platform);
            platforms.Add(platform2);
          

            int playerSpeed = 4;
            int jump = 3;
            int jump2 = 3;

           
            player = new Player(game, spriteBatch, playerTex,  playerSpeed, jump, playerPos, "test", jumpSound);
            enemy = new Enemy(game, spriteBatch, enemyTex, playerSpeed, jump2, enemyPos, "test", player);
            Collisionmanager = new CollisionManager(g, player, platforms);
            enemyCollisionManager = new EnemyCollisionManager(g, enemy, platforms);
            this.components.Add(platform);
            this.components.Add(platform2);

            this.components.Add(player);
            this.components.Add(enemy);
            this.components.Add(Collisionmanager);
            this.components.Add(enemyCollisionManager);
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
