using System.Threading;
using NAudio.Wave;
using System.Diagnostics;
using NAudio.Wave.SampleProviders;

namespace GUI
{
    public static class AudioPlayer
    {
        private static WaveOutEvent? outputDevice;
        private static Thread? audioThread;

        public static void Play(string filePath)
        {
            if (audioThread != null)
                StopImpl();
           
            audioThread = new Thread(() => { 
                PlayImpl(filePath); 
            });
            audioThread.Start();
        }

        private static void PlayImpl(string filePath) 
        {
            outputDevice = new WaveOutEvent();
            outputDevice?.Init(new FadeInOutSampleProvider(new AudioFileReader(filePath)));
            outputDevice?.Play();
            Trace.WriteLine("Done playing");
        }

        private static void StopImpl()
        {
            outputDevice?.Stop();
            audioThread?.Join();
            Trace.WriteLine("Stopped playing");
        }
    }
}
