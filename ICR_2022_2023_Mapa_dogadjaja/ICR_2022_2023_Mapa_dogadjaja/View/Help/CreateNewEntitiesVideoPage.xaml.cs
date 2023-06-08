using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows.Threading;

namespace ICR_2022_2023_Mapa_dogadjaja.View.Help
{
	/// <summary>
	/// Interaction logic for CreateNewEntitiesVideoPage.xaml
	/// </summary>
	public partial class CreateNewEntitiesVideoPage : Page
	{
		private bool isVideoPlayerPlaying = false;
		private bool isUserDraggingSlider = false;

		public CreateNewEntitiesVideoPage()
		{
			InitializeComponent();

			// REFERENCE: https://wpf-tutorial.com/audio-video/how-to-creating-a-complete-audio-video-player/
			DispatcherTimer timer = new DispatcherTimer();
			timer.Interval = TimeSpan.FromSeconds(1);
			timer.Tick += TickTimer;
			timer.Start();

			AddHotKeys();
		}

		private void AddHotKeys()
		{
			try
			{
				MediaCommands.Play.InputGestures.Add(new KeyGesture(Key.P, ModifierKeys.Control));
				CommandBindings.Add(new CommandBinding(MediaCommands.Play, PlayVideo, CanVideoBePlayed));

				MediaCommands.Pause.InputGestures.Add(new KeyGesture(Key.Space, ModifierKeys.Control));
				CommandBindings.Add(new CommandBinding(MediaCommands.Pause, PauseVideo, CanVideoBePaused));

				MediaCommands.Stop.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Control));
				CommandBindings.Add(new CommandBinding(MediaCommands.Stop, StopVideo, CanVideoBeStoppped));
			}
			catch (Exception e)
			{
				Console.Write(e.StackTrace);
			}
		}

		// REFERENCE: https://wpf-tutorial.com/audio-video/how-to-creating-a-complete-audio-video-player/
		private void TickTimer(object sender, EventArgs e)
		{
			if (Video_player.Source != null && Video_player.NaturalDuration.HasTimeSpan && !isUserDraggingSlider)
			{
				Progress_slider.Minimum = 0;
				Progress_slider.Maximum = Video_player.NaturalDuration.TimeSpan.TotalSeconds;
				Progress_slider.Value = Video_player.Position.TotalSeconds;
			}
		}

		private void CanVideoBePlayed(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = (Video_player != null) && (Video_player.Source != null);
		}

		private void PlayVideo(object sender, ExecutedRoutedEventArgs e)
		{
			Video_player.Play();
			isVideoPlayerPlaying = true;
		}

		private void CanVideoBePaused(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = isVideoPlayerPlaying;
		}

		private void PauseVideo(object sender, ExecutedRoutedEventArgs e)
		{
			Video_player.Pause();
		}

		private void CanVideoBeStoppped(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = isVideoPlayerPlaying;
		}

		private void StopVideo(object sender, ExecutedRoutedEventArgs e)
		{
			Video_player.Stop();
			isVideoPlayerPlaying = false;
		}

		private void Progress_slider_DragStarted(object sender, DragStartedEventArgs e)
		{
			isUserDraggingSlider = true;
		}

		private void Progress_slider_DragCompleted(object sender, DragCompletedEventArgs e)
		{
			isUserDraggingSlider = false;
			Video_player.Position = TimeSpan.FromSeconds(Progress_slider.Value);
		}

		private void Progress_slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			Progress_status_label.Text = TimeSpan.FromSeconds(Progress_slider.Value).ToString(@"hh\:mm\:ss");
		}

		private void GoToStartingPage(object sender, RoutedEventArgs e)
		{
			StartingPage startingPage = new StartingPage();
			NavigationService.Navigate(startingPage);
		}
	}
}
