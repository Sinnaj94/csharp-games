using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Audio;

namespace Pong
{
    class ManageSound
    {

        private static ManageSound instance;

        List<Sound> sounds;

        private ManageSound()
        {
            sounds = new List<Sound>
            {
            new Sound(new SoundBuffer(@"Resources\hit.wav")),
            new Sound(new SoundBuffer(@"Resources\lose.wav")),
            new Sound(new SoundBuffer(@"Resources\side.wav"))
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

        public void hit()
        {
            sounds[0].Play();
        }

        public void lose()
        {
            sounds[1].Play();
        }

        public void side()
        {
            sounds[2].Play();
        }

    }
}
