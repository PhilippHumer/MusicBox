using System.Threading;
using System.Diagnostics;
using LibVLCSharp.Shared;
using System;

namespace GUI
{
    public static class AudioPlayer
    {
        private static Thread? audioThread;
        private static MediaPlayer? mediaPlayer;
        private static readonly Object _lock = new();

        public static void Play(string filePath)
        {
            lock (_lock)
            {
                if (audioThread != null)
                    StopImpl();

                audioThread = new Thread(() => {
                    PlayImpl(filePath);
                });
                audioThread.Start();
            }
        }

        private static void PlayImpl(string filePath) 
        {
            Core.Initialize();
            LibVLC libVLC = new LibVLC();
            mediaPlayer = new MediaPlayer(libVLC);
            mediaPlayer.Media = new Media(libVLC, new Uri(filePath));
            mediaPlayer.Play();
            Trace.WriteLine("Done playing");
        }

        private static void StopImpl()
        {
            mediaPlayer?.Stop();
            audioThread?.Join();
            Trace.WriteLine("Stopped playing");
        }
    }
}
