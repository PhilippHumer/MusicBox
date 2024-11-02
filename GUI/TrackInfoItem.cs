namespace GUI
{
    public class TrackInfoItem
    {
        public string Title { get; set; }
        public string JpgPath { get; set; }
        public string Mp3Path { get; set; }

        public TrackInfoItem(string title, string jpgPath, string mp3path)
        {
            this.Title = title;
            this.JpgPath = jpgPath;
            this.Mp3Path = mp3path;
        }
    }
}
