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

        List<SoundBuffer> bufferList;
        Sound hitSound;
        Sound loseSound;
        Sound sideSound;



        private ManageSound()
        {
            bufferList = new List<SoundBuffer>();
            hitSound = new Sound(new SoundBuffer(@"Resources\hit.wav"));
            loseSound = new Sound(new SoundBuffer(@"Resources\lose.wav"));
            sideSound = new Sound(new SoundBuffer(@"Resources\side.wav"));
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

        public void hit()
        {
            hitSound.Play();
        }

        public void lose()
        {
            loseSound.Play();
        }

        public void side()
        {
            sideSound.Play();
        }

    }
}
