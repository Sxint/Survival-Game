using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Survive;

namespace GameTemplate
{
    //this is the actual game itself
    public class ActionScene : GameScene
    {
        public static int level { get; set; }   
        private SpriteBatch spriteBatch;
        private Game1 g;
        //declare all game related components
        private Player bat;
        private Platform platform, platform2;
        public CollisionManager Collisionmanager;
        public static Texture2D Zerlig { get; private set; }
        private Enemy enemy;
        public ActionScene(Game game) : base(game)
        {
            g = (Game1)game;
            this.spriteBatch = g._spriteBatch;
            Texture2D batTex = g.Content.Load<Texture2D>("images/player");
            Zerlig = g.Content.Load<Texture2D>("images/zerlig");
            Vector2 batPos = new Vector2(Shared.stage.X / 2 - batTex.Width / 2,Shared.stage.Y - batTex.Height);
            Vector2 platformPos = new Vector2(200, Shared.stage.Y - batTex.Height);
            Vector2 platformPos2 = new Vector2(800, Shared.stage.Y - batTex.Height);
          

            int playerSpeed = 4;
            int jump = 3;

            platform2 = new Platform(game, spriteBatch, batTex, platformPos2);
            platform = new Platform(game, spriteBatch, batTex, platformPos);
            bat = new Player(game, spriteBatch, batTex,  playerSpeed, jump, batPos, "test");
           // Zerlig = new Enemy(game, spriteBatch, zerligTex, position);
            
            Collisionmanager = new CollisionManager(g, bat, platform);
            this.components.Add(platform);
            this.components.Add(platform2);

            this.components.Add(bat);
           // this.components.Add(Zerlig); 
          
            this.components.Add(Collisionmanager);

        }
    }
}
