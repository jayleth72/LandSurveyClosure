using System;
using Xamarin.Forms;

namespace LandSurveyClosure.Behaviors
{
    public class MinNumberFieldValidationBehavior : Behavior<Entry>
    {
		public int MinNumber { get; set; }

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
			int resultInt;
            double resultDouble;

			bool isValidInt = int.TryParse(args.NewTextValue, out resultInt);
            bool isValidDouble = double.TryParse(args.NewTextValue, out resultDouble);

			// if Entry text is larger than max number
			if (isValidInt || isValidDouble)
			{
                
				if (resultInt < MinNumber && isValidInt)
				{
					((Entry)sender).TextColor = Color.Red;
				}

				if ((int)resultDouble < MinNumber && isValidDouble)
				{
					((Entry)sender).TextColor = Color.Red;
				}
			}
            else
            {
                ((Entry)sender).TextColor = Color.Default;
            }    
		}
    }
}
