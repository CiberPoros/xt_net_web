using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomCanvas;
using GameInterface;

namespace TanksGame
{
    public class Hero : TanksGameObject
    {
        public Hero(Point leftTop, AbstractGame gameState) : base(leftTop, gameState)
        {

        }

        public override TanksGameObjectType GetGameObjType() => TanksGameObjectType.Hero;

        public override Bullet Shoot()
        {
            var bullet =  base.Shoot();

            bullet.Creater = TanksGameObjectType.Hero;

            return bullet;
        }
    }
}
