using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Audio;

namespace Pong
{
    class soundTest
    {

        Sound hit;
        Sound loose;
        Sound win;
        soundTest()
        {
            hit = new Sound(new SoundBuffer(@"Resources\hit.wav"));
            loose = new Sound(new SoundBuffer(@"Resources\hit.wav"));
            win = new Sound(new SoundBuffer(@"Resources\hit.wav"));
        }

        public void playSound(int soundId)
        {
            switch (soundId)
            {
                case 1:
                    hit.Play();
                    break;
                case 2:
                    loose.Play();
                    break;
                case 3:
                    win.Play();
                    break;
                default:
                    break;
            }
        }
    }
}
