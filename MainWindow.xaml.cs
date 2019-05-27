using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
namespace _312551u5GameOfLife
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Rectangle[,] map;
        bool[,] mapMatrix;
        List<Point> liveCells;
        List<Point> deadCells;
        DispatcherTimer timer;

        public MainWindow()
        {
            InitializeComponent();
            liveCells = new List<Point>();
            initializeMap(new Point(-1,-1));
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 1000 / 15);
            timer.Tick += timer_Tick;
            // timer.Start();
            updateMap();

        }
        private void timer_Tick(object sender, EventArgs e)
        {
            updateMap();
        }
        private void initializeMap(Point p)
        {
         
            mapMatrix = new bool[4,4];
            map = new Rectangle[4,4];
            int posX = 0, posY = 0;
            for(int x = 0; x < mapMatrix.GetLength(1); x++)
            {
                for(int y = 0; y < mapMatrix.GetLength(0); y++)
                {
                    map[y, x] = new Rectangle();
                    map[y, x].Height = 25;
                    map[y, x].Width = 25;
                    map[y, x].Stroke = Brushes.Black;
                    if(new Point(x,y) == p)
                    {
                        map[y, x].Fill = Brushes.Red;
                        liveCells.Add(new Point(x, y));
                        foreach (Point point in liveCells)
                        {
                            Console.WriteLine(point);
                        }
                    }

                    canvas.Children.Add(map[y, x]);
                    Canvas.SetTop(map[y, x], posY);
                    Canvas.SetLeft(map[y, x], posX);
                    posY += 25;
                }
                posY = 0;
                posX += 25;
            }
        }
        private void removeAll()
        {
           
        }
        private void updateMap()
        {
            deadCells = new List<Point>();
            for (int x = 0; x < map.GetLength(1); x++)
            {
                for (int y = 0; y < map.GetLength(0); y++)
                {
                   
                   
                        int counter = 0;
                        if (liveCells.Contains(new Point(x, y + 1)))
                        {
                            counter++;
                        }
                        if (liveCells.Contains(new Point(x, y - 1)))
                        {
                            counter++;
                        }
                        if (liveCells.Contains(new Point(x + 1, y - 1)))
                        {
                            counter++;
                        }
                        if (liveCells.Contains(new Point(x - 1, y - 1)))
                        {
                            counter++;
                        }
                        if (liveCells.Contains(new Point(x + 1, y + 1)))
                        {
                            counter++;
                        }
                        if (liveCells.Contains(new Point(x - 1, y)))
                        {
                            counter++;
                        }
                        if (liveCells.Contains(new Point(x - 1, y)))
                        {
                            counter++;
                        }
                        if (counter > 2)
                        {
                            liveCells.Add(new Point(x, y));
                        }
                        else if(counter < 1) 
                        {
                                deadCells.Add(new Point(x, y));
                            
                        }
                    
                }
            }
            foreach(Point p in deadCells)
            {
                if(liveCells.Contains(p))
                {
                    liveCells.Remove(p);
                    map[(int)p.Y, (int)p.X].Fill = Brushes.White;
                }
          
            }
            foreach(Point p in liveCells)
            {
                
                map[(int)p.Y, (int)p.X].Fill = Brushes.Red;
            }
        }
           
        private void BtnHelp_Click(object sender, RoutedEventArgs e)
        {

        }


        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Point mPoint = new Point((int)Mouse.GetPosition(canvas).X/25, (int)Mouse.GetPosition(canvas).Y/25);
            initializeMap(mPoint);
        }
    }
}
