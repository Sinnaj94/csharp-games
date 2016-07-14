﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Audio;

namespace JumpAndRun
{
    class ManageSound
    {

        private static ManageSound instance;

        List<Sound> sounds;
        Music backgroundMusic;
        private ManageSound()
        {
            sounds = new List<Sound>
            {
                new Sound(new SoundBuffer(@"Resources\sounds\swoosh.wav")),


            };
            //backgroundMusic = new Music(@"Resources/song.ogg");
            //backgroundMusic.Volume = 50;
            //backgroundMusic.Loop = true;

        }

        public void StartPlayingMusic()
        {
            backgroundMusic.Play();

        }

        public void StopMusic()
        {
            backgroundMusic.Stop();
        }

        public static ManageSound Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ManageSound();
                }
                return instance;
            }
        }

        public void setVolume(int volume)
        {
            foreach (Sound s in sounds)
            {
                s.Volume = volume;
            }
            backgroundMusic.Volume = volume / 2;
        }

        public void swoosh()
        {
            sounds[0].Play();
        }


    }
}