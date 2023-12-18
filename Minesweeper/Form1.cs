using Microsoft.VisualBasic.Devices;
using System.Drawing.Text;
using System.Runtime.CompilerServices;
using static System.Reflection.Metadata.BlobBuilder;

namespace Minesweeper
{
    public partial class Form1 : Form
    {
        private int[,] grid;

        private const int BombCellValue = 9;
        private int CellSize = 40;

        //Obtiznost gridu
        private int x = 9, y = 9;
        private static int bombs = 10;

        private int FlagsRemaining = bombs;

        private bool gameActive = true;

        private Button resetButton;
        private Button bombCounter;

        public Form1()
        {
            InitializeComponent();
            InitializeFont();
            InitializeGame();

            // Tlačítko Reset
            resetButton = new Button();
            resetButton.Width = CellSize * x - CellSize * 4;
            resetButton.Height = CellSize;
            resetButton.Left = CellSize * 2;
            resetButton.Text = "Reset";
            resetButton.Click += Reset_Click;
            Controls.Add(resetButton);

            bombCounter = new Button();
            bombCounter.Width = CellSize * 2;
            bombCounter.Height = CellSize;
            bombCounter.BackColor = Color.LightGray;
            bombCounter.Text = FlagsRemaining.ToString();
            Controls.Add(bombCounter);

            ShowGrid(grid);

            
        }

        private void InitializeGame()
        {
            grid = new int[x, y];

            GenerateBombs(grid, bombs);
            AroundBombs(grid);

            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateCounter();

        }

        private void InitializeFont()
        {
            // Název souboru fontu (upravte podle vašeho souboru)
            string fontFileName = "mine-sweeper.ttf";

            // Kontrola, zda je font v požadované složce
            string fontPath = Path.Combine(Application.StartupPath, fontFileName);

            if (!File.Exists(fontPath))
            {
                // Pokud není, zkopírujte font do složky s aplikací
                File.Copy(Path.Combine("Fonts", fontFileName), fontPath);
            }

            // Načtěte font ze souboru
            Font customFont = LoadFont(fontPath);

            // Nastavte font pro celý formulář
            this.Font = customFont;
        }

        private Font LoadFont(string fontPath)
        {
            try
            {
                // Načtěte font ze souboru
                PrivateFontCollection privateFonts = new PrivateFontCollection();
                privateFonts.AddFontFile(fontPath);
                return new Font(privateFonts.Families[0], 8, FontStyle.Regular);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Chyba při načítání fontu: {ex.Message}", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return SystemFonts.DefaultFont; // Pokud dojde k chybě, použijte výchozí font
            }
        }

        public void GenerateBombs(int[,] grid, int bombs)
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

                if (grid[x, y] == BombCellValue)
                    i--;
                else
                    grid[x, y] = BombCellValue;
            }
        }

        public void AroundBombs(int[,] grid)
        {
            //Vsechny prvky gridu
            for (int xX = 0; xX < grid.GetLength(0); xX++)
            {
                for (int yY = 0; yY < grid.GetLength(1); yY++)
                {
                    //Kdyz bomba
                    if (grid[xX, yY] == BombCellValue)
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
                                    if (grid[x, y] != BombCellValue)
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

        public void DeleteGridButtons()
        {
            foreach (Control control in Controls.OfType<Button>().ToList())
            {
                if (control != resetButton && control != bombCounter)
                {
                    Controls.Remove(control);
                    control.Dispose();
                }
            }
        }

        public void UpdateCounter()
        {
            foreach (Control control in Controls.OfType<Button>().ToList())
            {
                if (control == bombCounter)
                {
                    Controls.Remove(control);
                    control.Dispose();
                    bombCounter = new Button();
                    bombCounter.Width = CellSize * 2;
                    bombCounter.Height = CellSize;
                    bombCounter.BackColor = Color.LightGray;
                    bombCounter.Text = FlagsRemaining.ToString();
                    Controls.Add(bombCounter);
                }
            }
        }

        public void ShowGrid(int[,] grid)
        {
            DeleteGridButtons();

            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    Button b = new Button();
                    b.Left = x * CellSize;
                    b.Width = CellSize;
                    b.Top = y * CellSize + CellSize;
                    b.Height = CellSize;
                    b.Text = $"[{x},{y}]";
                    b.MouseDown += ToggleFlag;
                    Controls.Add(b);

                    //Convertuje zkryte prvky v gridu
                    switch (grid[x, y])
                    {
                        case BombCellValue:
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
            gameActive = true;
            FlagsRemaining = bombs;
            InitializeGame();
            UpdateCounter();
            ShowGrid(grid);
        }

        private Button GetButtonAt(int x, int y)
        {
            int targetLeft = x * CellSize;
            int targetTop = y * CellSize + CellSize;

            foreach (Control control in Controls)
            {
                if (control is Button button && button.Left == targetLeft && button.Top == targetTop)
                {
                    return button;
                }
            }
            return null;
        }

        private void RevealAllCells()
        {
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    Button b = GetButtonAt(x, y);
                    //b.Enabled = false; // Disable further clicks

                    switch (grid[x, y])
                    {
                        case BombCellValue:
                            b.Text = "*";
                            break;
                        case 0:
                            b.Text = " ";
                            break;
                        case 1:
                            b.Text = "1";
                            break;
                        case 2:
                            b.Text = "2";
                            break;
                        case 3:
                            b.Text = "3";
                            break;
                        case 4:
                            b.Text = "4";
                            break;
                        case 5:
                            b.Text = "5";
                            break;
                        case 6:
                            b.Text = "6";
                            break;
                        case 7:
                            b.Text = "7";
                            break;
                        case 8:
                            b.Text = "8";
                            break;
                    }
                }
            }
        }

        private void FloodFill(int x, int y)
        {
            // Check if the coordinates are within the grid bounds
            if (x < 0 || x >= grid.GetLength(0) || y < 0 || y >= grid.GetLength(1))
            {
                return;
            }

            Button currentButton = GetButtonAt(x, y);

            // Check if the current cell is already revealed or has a bomb
            if (currentButton.Text == " " || currentButton.Text == "F" || grid[x, y] == BombCellValue)
            {
                return;
            }

            // Count the number of adjacent bombs
            int adjacentBombs = CountAdjacentBombs(x, y);

            // Reveal the current cell with the appropriate number or an empty space
            currentButton.Text = (adjacentBombs > 0) ? adjacentBombs.ToString() : " ";

            // If the current cell has no adjacent bombs, recursively call flood-fill on its neighbors
            if (adjacentBombs == 0)
            {
                for (int i = x - 1; i <= x + 1; i++)
                {
                    for (int j = y - 1; j <= y + 1; j++)
                    {
                        if (i != x || j != y) // Skip the current cell
                        {
                            FloodFill(i, j);
                        }
                    }
                }
            }
        }

        private int CountAdjacentBombs(int x, int y)
        {
            int count = 0;

            for (int i = x - 1; i <= x + 1; i++)
            {
                for (int j = y - 1; j <= y + 1; j++)
                {
                    if (i >= 0 && i < grid.GetLength(0) && j >= 0 && j < grid.GetLength(1) && grid[i, j] == BombCellValue)
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        private void ToggleFlag(object sender, MouseEventArgs e)
        {
            if (gameActive)
            {
                Button button = (Button)sender;
                int x = button.Left /   CellSize;
                int y = (button.Top - CellSize) / CellSize;

                if (e.Button == MouseButtons.Right)
                {
                    if (button.Text == $"[{x},{y}]" && FlagsRemaining > 0)
                    {
                        button.Text = "F"; // Umístění vlajky
                        FlagsRemaining--;
                    }
                    else if (button.Text == "F")
                    {
                        button.Text = $"[{x},{y}]"; // Odstranění vlajky
                        FlagsRemaining++;
                    }

                    UpdateCounter();
                }
            }
        }


        private void Reset_Click(object sender, EventArgs e)
        {
            ResetGame();
        }

        //Info na gridu po kliknuti
        private void Click9(object sender, EventArgs e)
        {
            if (gameActive)
            {
                Button b = (Button)sender;
                b.Text = "*";
                b.BackColor = Color.Red;
                RevealAllCells();
                gameActive = false;
            }
        }
        private void Click0(object sender, EventArgs e)
        {
            if (gameActive)
            {
                Button b = (Button)sender;
                //b.BackColor = Color.Gray;
                int x = b.Left / CellSize;
                int y = (b.Top - CellSize) / CellSize;

                if (b.Text == " ")
                    return;

                FloodFill(x, y);
            }
        }
        private void Click1(object sender, EventArgs e)
        {
            if (gameActive)
            {
                Button b = (Button)sender;
                b.Text = "1";
                b.ForeColor = Color.Blue;
            }
        }
        private void Click2(object sender, EventArgs e)
        {
            if (gameActive)
            {
                Button b = (Button)sender;
                b.Text = "2";
                b.ForeColor = Color.Green;
            }
        }
        private void Click3(object sender, EventArgs e)
        {
            if (gameActive)
            {
                Button b = (Button)sender;
                b.Text = "3";
                b.ForeColor = Color.Red;
            }

        }
        private void Click4(object sender, EventArgs e)
        {
            if (gameActive)
            {
                Button b = (Button)sender;
                b.Text = "4";
                b.ForeColor = Color.DarkBlue;
            }

        }
        private void Click5(object sender, EventArgs e)
        {
            if (gameActive)
            {
                Button b = (Button)sender;
                b.Text = "5";
                b.ForeColor = Color.DarkRed;
            }
        }
        private void Click6(object sender, EventArgs e)
        {
            if (gameActive)
            {
                Button b = (Button)sender;
                b.Text = "6";
                b.ForeColor = Color.AliceBlue;
            }
        }
        private void Click7(object sender, EventArgs e)
        {
            if (gameActive)
            {
                Button b = (Button)sender;
                b.Text = "7";
                b.ForeColor = Color.Black;
            }
        }
        private void Click8(object sender, EventArgs e)
        {
            if (gameActive)
            {
                Button b = (Button)sender;
                b.Text = "8";
                b.ForeColor = Color.DarkGray;
            }
        }
    
    }
}