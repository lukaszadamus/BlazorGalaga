﻿using System;
using System.Collections.Generic;
using System.Drawing;
using BlazorGalaga.Interfaces;

namespace BlazorGalaga.Models
{
    public class Ship : AnimatableBase
    {
        public bool IsFiring { get; set; }

        public Ship() 
        {
            Sprite = new Sprite(Sprite.SpriteTypes.Ship);
        }
    }
}
