﻿using System;
using System.Threading.Tasks;
using BlazorGalaga.Models;
using Microsoft.JSInterop;

namespace BlazorGalaga.Static
{
    public static class KeyBoardHelper
    {

        public static  string KeyDown { get; set; }

        private static bool ignorenextkeyup;
        private static bool fire;
        public static  bool dontfire { get; set; }

        [JSInvokable("OnKeyDown")]
        public static void OnKeyDown(string keycode)
        {
            Utils.dOut("KeyDown",keycode);

            if (keycode == Constants.ArrowLeft || keycode == Constants.ArrowRight)
            {
                if ((KeyDown == Constants.ArrowLeft && keycode == Constants.ArrowRight) ||
                (KeyDown == Constants.ArrowRight && keycode == Constants.ArrowLeft))
                    ignorenextkeyup = true;

                KeyDown = keycode;
            }
            if (keycode == Constants.Space && !dontfire)
            {
                fire = true;
            }

        }

        [JSInvokable("OnKeyUp")]
        public static void OnKeyUp(string keycode)
        {
            Utils.dOut("KeyUp", keycode);

            if (keycode == Constants.ArrowLeft || keycode == Constants.ArrowRight)
            {
                if (ignorenextkeyup)
                    ignorenextkeyup = false;
                else
                    KeyDown = "";
            }
        }

        public static void ControlShip(Ship ship)
        {
            if (fire)
            {
                ship.IsFiring = true;
                dontfire = true;
                fire = false;
                Task.Delay(Constants.ShipMissleDelaySpeed).ContinueWith((task) =>
                {
                    dontfire = false;
                });
            }

            if (KeyDown == Constants.ArrowLeft)
                ship.Speed = Constants.ShipMoveSpeed * -1;

            if (KeyDown == Constants.ArrowRight)
                ship.Speed = Constants.ShipMoveSpeed;

            if (KeyDown == string.Empty)
                ship.Speed = 0;
        }
    }
}
