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
		double jumpStrength = -15;

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
        }

        public void OneKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                gravity = jumpStrength;
            }
        }
	}
}