using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace FlappyBird
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer gameTimer = new DispatcherTimer();

		double gravity = 2;
		double jumpStrength = -12;

        List<Rectangle> pipes = new List<Rectangle>();
        Random rnd = new Random();
        int pipeTimer = 0;

		int score = 0;
		List<Rectangle> scorePipes = new List<Rectangle>();

		public MainWindow()
        {
            InitializeComponent();

            this.KeyDown += OneKeyDown;

            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            gameTimer.Tick += GameLoop;
            gameTimer.Start();
        }

        public void GameLoop(object sender, EventArgs e)
        {

			double y = Canvas.GetTop(bird);
            Canvas.SetTop(bird, y + gravity);
            gravity += 1;

			if (y < 0 || y + bird.Height > gameCanvas.ActualHeight)
			{
				GameOver();
			}


			pipeTimer++;
			if (pipeTimer > 90)
			{
				MakePipePair();
				pipeTimer = 0;
			}

			for (int i = pipes.Count - 1; i >= 0; i--)
			{
				double x = Canvas.GetLeft(pipes[i]);
				if (x + pipes[i].Width < 0)
				{
					gameCanvas.Children.Remove(pipes[i]);
					pipes.RemoveAt(i);
				}
				else
				{
					Canvas.SetLeft(pipes[i], x - 5);
				}
			}

			foreach (var pipe in pipes)
			{
				Rect pipeHitBox = new Rect(Canvas.GetLeft(pipe), Canvas.GetTop(pipe), pipe.Width, pipe.Height);

				double birdX = Canvas.GetLeft(bird);
				double birdY = Canvas.GetTop(bird);
				Rect birdHitBox = new Rect(birdX, birdY, bird.Width, bird.Height);

				if (birdHitBox.IntersectsWith(pipeHitBox))
				{
					GameOver();
				}
			}

			foreach (var pipe in pipes)
			{
				if (!scorePipes.Contains(pipe))
				{
					double pipeX = Canvas.GetLeft(pipe);
					double birdX = Canvas.GetLeft(bird);
					if (pipeX + pipe.Width < birdX)
					{
						score++;
						scoreText.Text = "Score: " + score/2;
						scorePipes.Add(pipe);
					}
				}
			}
		}

		public void OneKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                gravity = jumpStrength;
            }
        }

        Rectangle CreatePipe(double x, double y, double width, double height)
        {
            Rectangle pipe = new Rectangle
            {
                Width = width,
                Height = height,
                Fill = Brushes.Green
            };
            Canvas.SetLeft(pipe, x);
            Canvas.SetTop(pipe, y);
            gameCanvas.Children.Add(pipe);
            pipes.Add(pipe);
            return pipe;
		}

		public void MakePipePair()
		{
			double gapHeight = 150;
			double pipeWidth = 60;
			double minPipeHeight = 50;

			double canvasHeight = gameCanvas.ActualHeight;
			double canvasWidth = gameCanvas.ActualWidth;

			double maxPipeHeight = canvasHeight - gapHeight - minPipeHeight;

			double topPipeHeight = rnd.Next((int)minPipeHeight, (int)maxPipeHeight);
			double bottomPipeHeight = canvasHeight - topPipeHeight - gapHeight;

			CreatePipe(canvasWidth, 0, pipeWidth, topPipeHeight);
			CreatePipe(canvasWidth, canvasHeight - bottomPipeHeight, pipeWidth, bottomPipeHeight);
		}

		public void GameOver()
		{
			gameTimer.Stop();
			gameOverScreen.Visibility = Visibility.Visible;
		}
		public void StartButton_Click(object sender, RoutedEventArgs e)
		{

		}
		public void BackButton_Click(object sender, RoutedEventArgs e)
		{

		}
		public void ExitButton_Click(object sender, RoutedEventArgs e)
		{

		}
		public void DifficultytButton_Click(object sender, RoutedEventArgs e)
		{

		}
	}
}