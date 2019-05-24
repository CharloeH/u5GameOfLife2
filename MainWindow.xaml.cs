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

namespace _312551u5GameOfLife
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Rectangle[,] map;
        bool[,] mapMatrix;
        public MainWindow()
        {
            InitializeComponent();
        }


        private void TxtInput_IsMouseDirectlyOverChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            txtInput.Text = "";
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            
            MessageBoxResult mbr = MessageBox.Show(updateMap(), "Day Finished:" , MessageBoxButton.YesNo);
            if(mbr == MessageBoxResult.No)
            {
                removeAll();
            }
        }
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            int.TryParse(txtInput.Text[0].ToString(), out int posX);
            int.TryParse(txtInput.Text[2].ToString(), out int posY);
            initializeMap(new Point(posX, posY));
        }
        private void initializeMap(Point p)
        {
            map = new Rectangle[20, 20];
            mapMatrix = new bool[20,20];
            int posX = 0, posY = 0;
            for(int x = 0; x < map.GetLength(1); x++)
            {
                for(int y = 0; y < map.GetLength(0); y++)
                {
                    mapMatrix[y, x] = new bool();
                    map[y, x] = new Rectangle();
                    map[y, x].Stroke = Brushes.Black;
                    map[y, x].Height = 15;
                    map[y, x].Width = 15;
                    if(x == p.X & y == p.Y)
                    {
                        mapMatrix[y, x] = true;
                        map[y, x].Fill = Brushes.Red;
                        MessageBox.Show(x.ToString() + y.ToString());
                    }
                    Canvas.SetTop(map[y, x], posY);
                    Canvas.SetLeft(map[y, x], posX);
                    canvas.Children.Add(map[y, x]);
                    posY += 15;
                }
                posY = 0;
                posX += 15;
            }
        }
        private void removeAll()
        {
            foreach(Rectangle r in map)
            {
                r.Fill = Brushes.White;
            }
        }
        private string updateMap()
        {
            int counter;
            string update = "";
            for (int x = 0; x < map.GetLength(1); x++)
            {
                for (int y = 0; y < map.GetLength(0); y++)
                {
                    if(mapMatrix[y,x] == true)
                    {
                        counter = 0;
                        if(mapMatrix[x, y] == true)
                        {
                            counter++;
                        }
                        if(mapMatrix[x, y-1] == true)
                        {
                            counter++;
                        }
                        if(mapMatrix[x,y+1] == true)
                        {
                            counter++;
                        }
                        if(mapMatrix[x-1, y] == true)
                        {
                            counter++;
                        }
                        if(mapMatrix[x+1, y] == true)
                        {
                            counter++;
                        }
                        if(mapMatrix[x+1, y+1] == true)
                        {
                            counter++;
                        }
                        if(mapMatrix[x-1, y-1] == true)
                        {
                            counter++;
                        }

                        if (counter < 1)
                        {
                            update += "cell (" + x + "," + y + ") multiplied\r";
                        }
                        else if (counter == 1)
                        {
                            update += "cell (" + x + "," + y + ") lived\r";
                        }
                        else
                        {
                            update += "cell (" + x + "," + y + ") died\r";
                        }
                    }
                }
            }
            return update;
        }
    }
}
