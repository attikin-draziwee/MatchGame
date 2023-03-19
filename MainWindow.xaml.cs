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

namespace MatchGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        int tenthsOfSecondElapsed;
        int matchesFound;

        public MainWindow()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;
            SetUpGame();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            tenthsOfSecondElapsed++;
            TimerTextBlock.Text = (tenthsOfSecondElapsed / 10F).ToString("0.0s");
            if (matchesFound == 8)
            {
                timer.Stop();
                TimerTextBlock.Text += " - Сыграем заново?";
            }

        }

        private void SetUpGame()
        {
            Random random = new Random();
            List<string> animalEmoji = new List<string>() 
            {
                "🐵", "🐶", "🐺", "🐱","🦁", "🐯", "🦒", "🦊",
                "🐵", "🐶", "🐺", "🐱","🦁", "🐯", "🦒", "🦊"
            };
            foreach(TextBlock textBlock in MainGrid.Children.OfType<TextBlock>())
            {
                if (textBlock.Name == "TimerTextBlock")
                {
                    continue;
                }
                int index = random.Next(animalEmoji.Count);
                textBlock.Text = animalEmoji[index];
                textBlock.Visibility = Visibility.Visible;
                animalEmoji.RemoveAt(index);
            }
            timer.Start();
            tenthsOfSecondElapsed = 0;
            matchesFound = 0;
        }

        TextBlock lastTextBlockClicked;
        bool findingMatch = false;

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock emoji = (TextBlock)sender;
            if (findingMatch == false)
            {
                lastTextBlockClicked = emoji;
                lastTextBlockClicked.Visibility = Visibility.Hidden;
                findingMatch = true;
            } else if (lastTextBlockClicked.Text == emoji.Text)
            {
                matchesFound++;
                emoji.Visibility = Visibility.Hidden;
                findingMatch = false;
            } else
            {
                lastTextBlockClicked.Visibility = Visibility.Visible;
                findingMatch = false;
            }
        }

        private void TimerTextBlock_MouseDown(object sender, MouseEventArgs e)
        {
            if (matchesFound == 8)
            {
                SetUpGame();
            }
        }
    }
}
