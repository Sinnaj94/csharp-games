﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using Pong.Properties;


namespace Pong
{
    class SoundManager
    {
        List<SoundPlayer> soundList;
        public SoundManager()
        {
            soundList = new List<SoundPlayer>();
            addSound(@"Resources\hit.wav");
            addSound(@"Resources\lose.wav");
            addSound(@"Resources\side.wav");

        }

        public void addSound(String stream)
        {
            SoundPlayer thisSound = new SoundPlayer(stream);
            soundList.Add(@thisSound);
        }

        public void playSound(int nr)
        {
            soundList[nr].Play();
        }

        public void playSound(String name)
        {

        }

    }
}