using Aiv.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillyPassepartout
{
    class RandomizeSoundEmitter : Component
    {
        protected AudioSource source;
        protected List<AudioClip> clips;

        public float Volume { get { return source.Volume; } set { source.Volume = value; } }

        public RandomizeSoundEmitter(GameObject owner) : base(owner)
        {
            source = new AudioSource();
            clips = new List<AudioClip>();
        }

        public void AddClip(string clipName)
        {
            AudioClip clip = AudioManager.GetClip(clipName);
            if (clip != null)
            {
                clips.Add(clip);
            }
        }

        public void Play()
        {
            RandomizePitch();
            source.Play(GetRandomClip());
        }

        public void Play(float volume)
        {
            source.Volume = volume;
            Play();
        }


        protected void RandomizePitch()
        {
            source.Pitch = RandomGenerator.GetRandomFloat() * 0.4f + 0.8f;
        }

        protected AudioClip GetRandomClip()
        {
            return clips[RandomGenerator.GetRandomInt(0, clips.Count)];
        }
    }
}
