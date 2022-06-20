using Aiv.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace BillyPassepartout
{
    class Player : Actor
    {
        private bool isMouseLeftClicked;
        private bool isMouseRightClicked;
        private bool isDead;

        public int Lives { get { return PersistentData.PlayerLives; } set { PersistentData.PlayerLives = value; } }
        public AudioSource StepsSource { get; private set; }
        public AudioSource GeneralSource { get; private set; }

        public Player() : base("dog", Game.PixelsToUnits(16), Game.PixelsToUnits(16))
        {
            AnimationStorage.LoadPlayerAnimations();

            RigidBody.Collider = ColliderFactory.CreateBoxFor(this, Game.PixelsToUnits(12), Game.PixelsToUnits(12));
            RigidBody.Collider.Offset = new Vector2(0.3f, 0.3f);
            RigidBody.Type = RigidBodyType.PLAYER;
            RigidBody.AddCollisionType(RigidBodyType.TRAP);

            Animation = GfxManager.GetAnimation("dogIdleD");
            Animation.Start();

            Agent = new Agent(this);

            isMouseLeftClicked = false;

            Speed = 3.5f;

            StepsSource = new AudioSource();
            StepsSource.Volume = 1.5f;
            GeneralSource = new AudioSource();
            AudioManager.AddClip("steps", "Assets/Sounds/Steps.ogg");
            AudioManager.AddClip("hurt", "Assets/Sounds/PlayerHurt.ogg");
            AudioManager.AddClip("dogDie", "Assets/Sounds/PlayerDeath.ogg");
            AudioManager.AddClip("swordThrow", "Assets/Sounds/SwordThrow.ogg");

            isDead = false;
            IsActive = true;
        }

        public void Input()
        {
            if (IsActive && !isDead)
            {
                if (Game.Window.MouseLeft)
                {
                    if (!isMouseLeftClicked)
                    {
                        isMouseLeftClicked = true;

                        List<Node> path = (SceneManager.CurrentScene).Map.PathfindingMap.GetPath(Agent.X, Agent.Y, (int)Game.MousePos.X, (int)Game.MousePos.Y);
                        Agent.SetPath(path);
                    }
                }
                else if (isMouseLeftClicked)
                {
                    isMouseLeftClicked = false;
                }

                if (Game.Window.MouseRight)
                {
                    if (!isMouseRightClicked && PersistentData.IsSwordCollected)
                    {
                        isMouseRightClicked = true;

                        Sword sword = new Sword();
                        sword.Position = Position;
                        sword.IsActive = true;
                        sword.AudioSource.Play(AudioManager.GetClip("rotatingSword"), true);

                        Vector2 dir = (Game.MousePos - Position).Normalized();
                        sword.RigidBody.Velocity = dir * Game.DeltaTime * sword.Speed;
                    }
                }
                else if (isMouseRightClicked)
                {
                    isMouseRightClicked = false;
                }
            }
        }

        public override void Update()
        {
            if (IsActive && !isDead)
            {
                if(RigidBody.Velocity != Vector2.Zero)
                {
                    if(!StepsSource.IsPlaying)
                    {
                        StepsSource.Play(AudioManager.GetClip("steps"));
                    }
                }
                else
                {
                    StepsSource.Stop();
                }

                if(Lives <= 0)
                {
                    isDead = true;
                    Agent.ResetPath();
                    GeneralSource.Play(AudioManager.GetClip("dogDie"));
                    Animation = GfxManager.GetAnimation("dogDie");

                    Console.WriteLine("GAME OVER!");
                }

                Node lastNode = Agent.GetLastNode();
                if(lastNode != null)
                {
                    if(lastNode.X > (int)Position.X)
                    {
                        Sprite.FlipX = false;
                        Animation = GfxManager.GetAnimation("dogWalkR");
                    }
                    else
                    {
                        Sprite.FlipX = true;
                        Animation = GfxManager.GetAnimation("dogWalkR");
                    }
                }
                else
                {
                    if(RigidBody.Velocity == Vector2.Zero)
                    {
                        Animation = GfxManager.GetAnimation("dogIdleD");
                    }
                }

                Animation.Start();
            }
        }

        public override void Draw()
        {
            if (IsActive)
            {
                base.Draw();
            }
        }

        public override void OnCollide(Collision collisionInfo)
        {
            OnTrapCollides(collisionInfo);
        }

        public void OnTrapCollides(Collision collisionInfo)
        {
            Console.WriteLine(Lives);

            StepsSource.Stop();
            GeneralSource.Play(AudioManager.GetClip("hurt"));

            if (collisionInfo.Delta.X < collisionInfo.Delta.Y)
            {
                // Horizontal Collision
                if (X < collisionInfo.Collider.X)
                {
                    // Collision from Left (inverse horizontal delta)
                    collisionInfo.Delta.X = -collisionInfo.Delta.X;
                }

                X += (int)collisionInfo.Delta.X;
            }
            else
            {
                // Vertical Collision
                if (Y < collisionInfo.Collider.Y)
                {
                    // Collision from Top
                    collisionInfo.Delta.Y = -collisionInfo.Delta.Y;
                }

                Y += (int)collisionInfo.Delta.Y;
            }
        }
    }
}
