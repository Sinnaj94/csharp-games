using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using Pong.Properties;
using SFML.Audio;

namespace Pong
{
    class SoundManager
    {
        

        List<SoundBuffer> bufferList;
        Sound sound;
        
        public SoundManager()
        {
            sound = new Sound();
            bufferList = new List<SoundBuffer>();
            addSound(@"Resources\hit.wav");
            addSound(@"Resources\lose.wav");
            addSound(@"Resources\side.wav");

        }

        public void addSound(String stream)
        {


            SoundBuffer currentBuffer = new SoundBuffer(stream);
            bufferList.Add(currentBuffer);
            
            
        }

        public void playSound(int nr)
        {
            //TODO: play multiple sounds at once!!!
             sound.SoundBuffer = bufferList[nr];
             sound.Play();
            
        }



        public void playSound(String name)
        {

        }

       

    }
}
