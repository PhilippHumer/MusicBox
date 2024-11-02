namespace GUI
{
    public class TrackInfoGridItem
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public TrackInfoItem Item { get; set; }

        public TrackInfoGridItem(int row, int col, TrackInfoItem item)
        {
            this.Row = row;
            this.Col = col;
            this.Item = item;
        }
    }
}
