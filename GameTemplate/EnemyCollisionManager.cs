using GameTemplate;
using Microsoft.Xna.Framework;
using SharpDX.MediaFoundation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Survive
{
    public class EnemyCollisionManager : GameComponent
    {

        private Enemy enemy;
        private List<Platform> platforms;
        private Platform Activeplatform;


        public EnemyCollisionManager(Game game, Enemy Enemy, List<Platform> PlatForms) : base(game)
        {
            this.enemy = Enemy;
            this.platforms = PlatForms;
        }



        public override void Update(GameTime gameTime)
        {
            foreach (Platform item in platforms)
            {
                Rectangle enemyRect = enemy.getBounds();

                Rectangle platformRect = item.getBounds();


                if (enemyRect.Intersects(platformRect))
                {
                    enemy.platform = platforms.IndexOf(item).ToString();
                    Activeplatform = platforms.ElementAtOrDefault(platforms.IndexOf(item));
                }

                if (Activeplatform != null)
                {
                    Rectangle platformRectLeft = Activeplatform.getLeftBounds();

                    Rectangle platformRectRight = Activeplatform.getRightBounds();

                    Rectangle platformRectTop = Activeplatform.getTopBounds();

                    //if the enemy hits the left side of the platform
                    //set iscollideleft to true
                    if (enemyRect.Intersects(platformRectLeft))
                    {
                        enemy.isCollideLeft = true;
                    }

                    //if the enemy hits the right side of the platform
                    //set iscolliderightt to true
                    if (enemyRect.Intersects(platformRectRight))
                    {
                        enemy.isCollideRight = true;
                    }

                    //if the enemy hits the top platform by jumping (hasjumped is true) and is not hitting either side of the platform
                    if (enemyRect.Intersects(platformRectTop) && enemy.hasjumped && enemy.isCollideLeft == false && enemy.isCollideRight == false)
                    {
                        //if the enemy's y coordinate is not equal to the top of the platform then fix that
                        //and set hasjumped to false and collideup to true
                        if (enemy.position.Y != platformRectTop.Top)
                        {
                            enemy.gravity = 0f;
                            enemy.isCollideUp = true;
                            enemy.position.Y = platformRectTop.Top - (enemy.tex.Height);
                            enemy.hasjumped = false;
                        }
                    }

                    //if the enemy hits a wall and hasjumped is not true
                    if (enemy.isCollideLeft && enemy.hasjumped != true)
                    {
                        //set the position of the enemy
                        enemy.position.X = platformRectLeft.Right;

                        //if the emeny's height is not higher than the platform (they are on the ground)
                        //make the enemy jump
                        if (enemy.position.Y - enemy.tex.Height > platformRectLeft.Height)
                        {
                            enemy.isCollideLeft = false;
                            enemy.position.Y -= enemy.jump;
                            enemy.gravity = -10f;
                            enemy.hasjumped = true;
                        }
                    }

                    //if the enemy hits a wall and hasjumped is not true
                    if (enemy.isCollideRight && enemy.hasjumped != true)
                    {
                        //set the position of the enemy
                        enemy.position.X = platformRectRight.Left - enemy.tex.Width;

                        //if the emeny's height is not higher than the platform (they are on the ground)
                        //make the enemy jump
                        if (enemy.position.Y - enemy.tex.Height > platformRectLeft.Height)
                        {
                            enemy.isCollideRight = false;
                            enemy.position.Y -= enemy.jump;
                            enemy.gravity = -10f;
                            enemy.hasjumped = true;
                        }
                    }

                    //if the enemy is on top of a platform
                    if (enemy.isCollideUp)
                    {
                        //if the enemy walks off the platform
                        //make it fall back to ground level
                        if (enemy.position.X > platformRectTop.Right && enemy.hasjumped != true)
                        {
                            enemy.hasjumped = true;

                            enemy.isCollideUp = false;
                        }

                        //if the enemy walks off the platform
                        //make it fall back to ground level
                        if (enemy.position.X + enemy.tex.Width < platformRectTop.X && enemy.hasjumped != true)
                        {
                            enemy.hasjumped = true;

                            enemy.isCollideUp = false;
                        }
                    }
                }
            }
            base.Update(gameTime);
        }
    }
}
