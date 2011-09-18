using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Emo
{
    class Health
    {
        private int _health;

        private Texture2D _healthBar;

        public int Healths{
            get { return _health; }
            set {_health = value; }
        }

        public Health(Texture2D _healthBar) 
        {
            this._healthBar = _healthBar;
        }


        public void Init() {
        }

        public void Update() { }

        public void Draw() {}
    }
}
