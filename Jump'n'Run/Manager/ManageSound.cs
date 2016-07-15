using System;
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
        List<Sound> footsteps;
        List<Sound> woodList;
        Music backgroundMusic;
        Random r;
        private ManageSound()
        {
            sounds = new List<Sound>
            {
                new Sound(new SoundBuffer(@"Resources\sounds\swoosh.wav")),
                new Sound(new SoundBuffer(@"Resources\sounds\splatter.ogg")),
                new Sound(new SoundBuffer(@"Resources\sounds\machinegun.wav")),
                new Sound(new SoundBuffer(@"Resources\sounds\rumble.wav")),

            };
            
            footsteps = new List<Sound>();
            for(int i = 1; i <= 5; i++)
            {
                footsteps.Add(new Sound(new SoundBuffer((@"Resources\sounds\footsteps\" + i + ".ogg"))));
                footsteps[i-1].Volume = 30;
            }
            woodList = new List<Sound>();

            for (int i = 1; i <= 4; i++)
            {
                woodList.Add(new Sound(new SoundBuffer((@"Resources\sounds\wood\Wood_0" + i + ".wav"))));
            }
            r = new Random(DateTime.Now.Millisecond);
            //backgroundMusic = new Music(@"Resources/song.ogg");
            //backgroundMusic.Volume = 50;
            //backgroundMusic.Loop = true;
            Init();
        }

        private void Init()
        {
            sounds[2].Volume = 20;
            sounds[3].Loop = true;
            sounds[3].Play();
            sounds[3].Volume = 50;
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

        public void footStep()
        {
            int _r = r.Next(0, 5);
            footsteps[_r].Play();
        }

        public void splatter()
        {
            sounds[1].Play();
        }

        public void machinegun()
        {
            sounds[2].Play();
        }

        public void wood()
        {
            int _r = r.Next(0, 4);
            woodList[_r].Play();
        }


    }
}
