using GameTemplate;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    static class EntityManager
    {
        static List<Entity> entities = new List<Entity>();
        public static List<Enemy> enemies = new List<Enemy>();
        

        static bool isUpdating;
        static List<Entity> addedEntities = new List<Entity>();

        public static int Count { get { return entities.Count; } }

        public static void Add(Entity entity)
        {
            if (!isUpdating)
                AddEntity(entity);
            else
                addedEntities.Add(entity);
        }

        private static void AddEntity(Entity entity)
        {
            entities.Add(entity);
           
            if (entity is Enemy)
                enemies.Add(entity as Enemy);

        }

        public static void Update()
        {
            isUpdating = true;
            //HandleCollisions();

            foreach (var entity in entities)
                entity.Update();

            isUpdating = false;

            //foreach (var entity in addedEntities)
            //    AddEntity(entity);

            addedEntities.Clear();

            entities = entities.Where(x => !x.IsExpired).ToList();
            enemies = enemies.Where(x => !x.IsExpired).ToList();
          
        }

        //static void HandleCollisions()
        //{
        //    // handle collisions between arrows and enemies
        //    for (int i = 0; i < enemies.Count; i++)
        //        for (int j = 0; j < arrows.Count; j++)
        //        {
        //            if (IsColliding(enemies[i], arrows[j]))
        //            {
        //                enemies[i].WasShot();
        //                arrows[j].IsExpired = true;
        //                ActionScene.Score++;
        //            }
        //        }

        //    // handle collisions between the player and enemies
        //    for (int i = 0; i < enemies.Count; i++)
        //    {
        //        if (enemies[i].IsActive && !ArcherStatus.IsGameOver && enemies[i].Position.X >= 900)
        //        {
        //            KillArcher();
        //            break;
        //        }
        //    }
        //}

        public static void EmptyEnemies()
        {
            enemies.ForEach(x => x.WasShot());
            EnemySpawner.Reset();
        }

        //private static void KillPlayer()
        //{
        //    ArcherStatus.IsGameOver = true;
        //    EmptyEnemies();
        //    EmptyArrows();
        //    EnemySpawner.Reset();
        //}

        //private static bool IsColliding(Entity a, Entity b)
        //{
        //    float radius = a.Radius + b.Radius;
        //    return !a.IsExpired && !b.IsExpired && Vector2.DistanceSquared(a.Position, b.Position) < radius * radius;
        //}

        public static void Draw(SpriteBatch spriteBatch)
        {
            foreach (var entity in entities)
                entity.Draw(spriteBatch);
        }
    }
}
