using static System.Reflection.Metadata.BlobBuilder;

namespace Minesweeper
{
    public partial class Form1 : Form
    {
        private int[,] grid;
        private int bombs;

        private Button resetButton;

        public Form1()
        {
            InitializeComponent();
            InitializeGame();

            // Tlačítko Reset
            resetButton = new Button();
            resetButton.Width = 40;
            resetButton.Height = 40;
            resetButton.Text = "Reset";
            resetButton.Click += Reset_Click;
            Controls.Add(resetButton);

            ShowGrid(grid);
        }

        private void InitializeGame()
        {
            //            [x, y]
            grid = new int[5, 5];
            bombs = 10;

            GenerateBombs(grid, bombs);
            AroundBombs(grid);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public static void GenerateBombs(int[,] grid, int bombs)
        {
            Random r = new Random();

            for (int y = 0; y < grid.GetLength(1); y++)
            {
                for (int x = 0; x < grid.GetLength(0); x++)
                {
                    grid[x, y] = 0;
                }
            }

            for (int i = 0; i < bombs; i++)
            {
                int x = r.Next(0, grid.GetLength(0));
                int y = r.Next(0, grid.GetLength(1));

                if (grid[x, y] == 9)
                    i--;
                else
                    grid[x, y] = 9;
            }
        }

        public static void AroundBombs(int[,] grid)
        {
            //Vsechny prvky gridu
            for (int xX = 0; xX < grid.GetLength(0); xX++)
            {
                for (int yY = 0; yY < grid.GetLength(1); yY++)
                {
                    //Kdyz bomba
                    if (grid[xX, yY] == 9)
                    {
                        //Prochazi prvky kolem bomby
                        int centerX = xX;
                        int centerY = yY;

                        for (int x = centerX - 1; x <= centerX + 1; x++)
                        {
                            for (int y = centerY - 1; y <= centerY + 1; y++)
                            {
                                //Kontroluje jestli neni mimo pole
                                if (x >= 0 && x < grid.GetLength(0) && y >= 0 && y < grid.GetLength(1))
                                {
                                    if (grid[x, y] != 9)
                                    {
                                        grid[x, y]++;

                                    }
                                }
                            }//
                        }
                    }
                }//
            }
        }

        public void ShowGrid(int[,] grid)
        {
            // Odstranění existujících tlačítek hrací plochy
            foreach (Control control in Controls.OfType<Button>().ToList())
            {
                if (control != resetButton)
                {
                    Controls.Remove(control);
                    control.Dispose();
                }
            }

            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    Button b = new Button();
                    b.Left = x * 40;
                    b.Width = 40;
                    b.Top = y * 40 + 40;
                    b.Height = 40;
                    b.Text = $"[{x},{y}]";
                    Controls.Add(b);

                    //Convertuje zkryte prvky v gridu
                    switch (grid[x, y])
                    {
                        case 9:
                            b.Click += Click9;
                            break;
                        case 0:
                            b.Click += Click0;
                            break;
                        case 1:
                            b.Click += Click1;
                            break;
                        case 2:
                            b.Click += Click2;
                            break;
                        case 3:
                            b.Click += Click3;
                            break;
                        case 4:
                            b.Click += Click4;
                            break;
                        case 5:
                            b.Click += Click5;
                            break;
                        case 6:
                            b.Click += Click6;
                            break;
                        case 7:
                            b.Click += Click7;
                            break;
                        case 8:
                            b.Click += Click8;
                            break;

                    }
                }
            }
        }

        private void ResetGame()
        {
            InitializeGame();

            ShowGrid(grid);
        }



        

        private void Reset_Click(object sender, EventArgs e)
        {
            ResetGame();
        }

        //Info na gridu po kliknuti
        private void Click9(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            b.Text = "X";
        }
        private void Click0(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            b.Text = " ";
        }
        private void Click1(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            b.Text = "1";
            //b.BackColor = Color.LightBlue;
        }
        private void Click2(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            b.Text = "2";
            //b.BackColor = Color.LightGreen;

        }
        private void Click3(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            b.Text = "3";
            //b.BackColor = Color.IndianRed;

        }
        private void Click4(object sender, EventArgs e)
        {
            //tmave modra
            Button b = (Button)sender;
            b.Text = "4";
            //b.BackColor = Color.DarkBlue;

        }
        private void Click5(object sender, EventArgs e)
        {
            //tmave cervena
            Button b = (Button)sender;
            b.Text = "5";
        }
        private void Click6(object sender, EventArgs e)
        {
            //sedo modra idk
            Button b = (Button)sender;
            b.Text = "6";
        }
        private void Click7(object sender, EventArgs e)
        {
            //cerna
            Button b = (Button)sender;
            b.Text = "7";
        }
        private void Click8(object sender, EventArgs e)
        {
            //seda
            Button b = (Button)sender;
            b.Text = "8";
        }
    
    }
}