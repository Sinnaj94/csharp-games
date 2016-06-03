using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Audio;

namespace SpaceShooter
{
    class ManageSound
    {

        private static ManageSound instance;

        List<Sound> sounds;

        private ManageSound()
        {
            sounds = new List<Sound>
            {
                new Sound(new SoundBuffer(@"Resources\shoot.wav")),
                new Sound(new SoundBuffer(@"Resources\shoot1.wav")),
                new Sound(new SoundBuffer(@"Resources\select.wav")),
                new Sound(new SoundBuffer(@"Resources\enter.wav")),


            };
            
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
            foreach(Sound s in sounds)
            {
                s.Volume = volume;
            }
        }

        public void shoot()
        {
            sounds[0].Play();
        }

        public void shoot1()
        {
            sounds[1].Play();
        }

        public void select()
        {
            sounds[2].Play();
        }

        public void enter()
        {
            sounds[3].Play();
        }

    }
}
