﻿using System;
using Xamarin.Forms;

namespace LandSurveyClosure.Behaviors
{
    /// <summary>
    /// Restricts the number of characters that can be entered into an entry field
    /// </summary>
    public class TextFieldLengthRestrictorBehavior : Behavior<Entry>
    {
		public int MaxFieldLength { get; set; }

		protected override void OnAttachedTo(Entry bindable)
		{
			base.OnAttachedTo(bindable);
			bindable.TextChanged += OnEntryTextChanged;
		}

		protected override void OnDetachingFrom(Entry bindable)
		{
			base.OnDetachingFrom(bindable);
			bindable.TextChanged -= OnEntryTextChanged;
		}

		void OnEntryTextChanged(object sender, TextChangedEventArgs e)
		{
			var entry = (Entry)sender;

			// if Entry text is longer then valid length
			if (entry.Text.Length > this.MaxFieldLength)
			{
				string entryText = entry.Text;
				entry.TextChanged -= OnEntryTextChanged;
				entry.Text = e.OldTextValue;
				entry.TextChanged += OnEntryTextChanged;
			}
		}
    }
}
