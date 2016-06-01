using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Aga.Diagrams
{
	public static class AutoScrollDecorator
	{
		public static readonly DependencyProperty IsEnabledProperty = 
			DependencyProperty.RegisterAttached(
				  "IsEnabled",
				  typeof(bool),
				  typeof(AutoScrollDecorator), new PropertyMetadata(false, IsEnabledValueChanged));


		public static void SetIsEnabled(DependencyObject element, bool value)
		{
			element.SetValue(IsEnabledProperty, value);
		}

		public static bool GetIsEnabled(DependencyObject element)
		{
			return (bool)element.GetValue(IsEnabledProperty);
		}

		public static readonly DependencyProperty SensivityProperty =
			DependencyProperty.RegisterAttached(
				  "Sensivity",
				  typeof(double),
				  typeof(AutoScrollDecorator), new PropertyMetadata(20.0));

		public static void SetSensivity(DependencyObject element, double value)
		{
			element.SetValue(SensivityProperty, value);
		}

		public static double GetSensivity(DependencyObject element)
		{
			return (double)element.GetValue(SensivityProperty);
		}

		public static readonly DependencyProperty StepProperty =
			DependencyProperty.RegisterAttached(
				  "Step",
				  typeof(double),
				  typeof(AutoScrollDecorator), new PropertyMetadata(16.0));


		public static void SetStep(DependencyObject element, double value)
		{
			element.SetValue(StepProperty, value);
		}

		public static double GetStep(DependencyObject element)
		{
			return (double)element.GetValue(StepProperty);
		}


		static void IsEnabledValueChanged(DependencyObject depObj, DependencyPropertyChangedEventArgs e)
		{
			var view = depObj as ScrollViewer;
			if (view != null && e.NewValue is bool)
			{
				if ((bool)e.NewValue)
					view.PreviewMouseMove += ViewPreviewMouseMove;
				else
					view.PreviewMouseMove -= ViewPreviewMouseMove;
			}
		}

		private static void ViewPreviewMouseMove(object sender, MouseEventArgs e)
		{
			var _scrollView = sender as ScrollViewer;
			if (_scrollView == null || !(_scrollView.Content is DependencyObject))
				return;
			if (!GetIsEnabled((DependencyObject)_scrollView.Content))
				return;

			var point = e.GetPosition(_scrollView);
			double sensivity = GetSensivity(_scrollView);
			double step = GetStep(_scrollView);
			double dx = 0;
			double dy = 0;

			if (point.X < sensivity)
				dx = -step;
			else if (point.X > _scrollView.ActualWidth - sensivity)
				dx = +step;

			if (point.Y < sensivity)
				dy = -step;
			else if (point.Y > _scrollView.ActualHeight - sensivity)
				dy = +step;

			_scrollView.ScrollToHorizontalOffset(_scrollView.HorizontalOffset + dx);
			_scrollView.ScrollToVerticalOffset(_scrollView.VerticalOffset + dy);
		}
	}
}
