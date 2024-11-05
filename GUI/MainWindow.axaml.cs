using Avalonia.Controls;
using Avalonia.Media;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace GUI;

public partial class MainWindow : Window
{
    public ObservableCollection<TrackInfoGridItem> TrackItemInfoList { get; set; }

    public MainWindow()
    {
        InitializeComponent();
        TrackItemInfoList = new ObservableCollection<TrackInfoGridItem>();

        InitGridItems();
        InitGrid();
    }

    private void InitGridItems()
    {
        string json = File.ReadAllText(Constants.JsonDatabasePath);

        int rowCnt = 0;
        int colCnt = 0;
        foreach (var item in JsonSerializer.Deserialize<List<TrackInfoItem>>(json))
        {
            TrackItemInfoList.Add(new TrackInfoGridItem(rowCnt, colCnt++, item));
            if (colCnt == 4)
            {
                colCnt = 0;
                rowCnt++;
            }
        }
    }

    private void InitGrid()
    {
        Grid? grid = this.FindControl<Grid>("MusicButtons");
        if (grid != null)
        {
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            int lastRow = -1;
            foreach (TrackInfoGridItem gridItem in TrackItemInfoList)
            {
                if (lastRow != gridItem.Row)
                {
                    grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(120) });
                    lastRow = gridItem.Row;
                }

                Button button = new Button
                {
                    Name = gridItem.Item.Title,
                    Content = new Image
                    {
                        Source = new Avalonia.Media.Imaging.Bitmap(Constants.ResourcesLocation + gridItem.Item.JpgPath),
                        Stretch = Avalonia.Media.Stretch.Fill,
                        Width = 118,
                        Height = 118,
                    },
                    CornerRadius = new Avalonia.CornerRadius(0),
                    Height = 120,
                    Width = 120,
                    BorderThickness = new Avalonia.Thickness(1),

                    // Replace the problematic line with the following:
                    BorderBrush = new SolidColorBrush(Color.Parse("#6b092a"))
                };
                button.Click += OnClick;
                Grid.SetRow(button, gridItem.Row);
                Grid.SetColumn(button, gridItem.Col);
                grid.Children.Add(button);
            }
        }
    }

    private void OnClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        Button? button = sender as Button;
        TrackInfoItem? track = TrackItemInfoList?.ToList()?.Find(gridItem => gridItem.Item.Title == button?.Name)?.Item;
        if (track != null)
        {
            Trace.WriteLine("Playing '" + track.Title + "'!");
            AudioPlayer.Play(Constants.ResourcesLocation + track.Mp3Path);
        }
    } 
}
