using GameTemplate;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Survive
{

    //NOTE: None of this works yet!
    //wait for alfredo to help us wiht multiple platforms before continuing as this should follow the same logic
    //for dealing with multiple objects of the same class
    public class Spawner : GameComponent
    {

        List<Enemy> activeEnemies = new List<Enemy>();
        Timer spawnTimer;
        private Enemy enemy;

        //Just one random instance per class, this is considered best practice to improve
        //the randomness of the generated numbers
        Random rng = new Random(); //Don't use a specific seed, that reduces randomness!

        public Spawner(Game game, Enemy Enemy) : base(game)
        {
            this.enemy = Enemy;
            //Min/max time for the next enemy to appear, in milliseconds
            spawnTimer = new Timer(rng.Next(500, 2000));
            spawnTimer.Elapsed += SpawnEnemy;
            spawnTimer.Start();
        }

        public void SpawnEnemy(object sender, ElapsedEventArgs e)
        {
            Vector2 newEnemyPosition = Vector2.Zero;
            newEnemyPosition.X = rng.Next(0, 1280); //Whatever parameters you want    
            newEnemyPosition.Y = rng.Next(0, 1280); //Whatever parameters you want    

            Vector2 newEnemyDirection = new Vector2(newEnemyPosition.X, newEnemyPosition.Y);

            Enemy spawnedEnemy = enemy;
            activeEnemies.Add(spawnedEnemy);

            //Min/max time for the next enemy to appear, in milliseconds
            spawnTimer = new Timer(rng.Next(500, 2000));
        }
    }
}

