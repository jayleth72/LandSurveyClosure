﻿using System;
using Xamarin.Forms;

namespace LandSurveyClosure.Behaviors
{
	public class NumberIntegerValidationBehavior : Behavior<Entry>
	{
		protected override void OnAttachedTo(Entry entry)
		{
			entry.TextChanged += OnEntryTextChanged;
			base.OnAttachedTo(entry);
		}

		protected override void OnDetachingFrom(Entry entry)
		{
			entry.TextChanged -= OnEntryTextChanged;
			base.OnDetachingFrom(entry);
		}

		void OnEntryTextChanged(object sender, TextChangedEventArgs args)
		{
			int result;

			bool isValid = int.TryParse(args.NewTextValue, out result);

			((Entry)sender).TextColor = isValid ? Color.Default : Color.Red;
		}
	}
}
