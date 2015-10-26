using System;
using System.ComponentModel;

namespace PDFFormCreationSample
{
	/// <summary>
	/// Simple entity to be used as view model.
	/// </summary>
	public class Employee:INotifyPropertyChanged
	{

		#region Properties

		string firstName;

		public string FirstName 
		{
			get 
			{
				return firstName;
			}
			set 
			{
				if (firstName != value) 
				{
					firstName = value;

					RaisePropertyChanged ("FirstName");
				}
			}
		}

		string lastName;
		public string LastName 
		{
			get 
			{
				return lastName;
			}
			set 
			{
				if (lastName != value) 
				{
					lastName = value;

					RaisePropertyChanged ("LastName");
				}
			}
		}

		string currentPosition;
		public string CurrentPosition 
		{
			get 
			{
				return currentPosition;
			}
			set 
			{
				if (currentPosition != value) 
				{
					currentPosition = value;

					RaisePropertyChanged ("CurrentPosition");
				}
			}
		}

		#endregion
		
		public Employee ()
		{
		}			

		#region INotifyPropertyChanged implementation

		public event PropertyChangedEventHandler PropertyChanged;

		private void RaisePropertyChanged(string propertyName)
		{
			if (PropertyChanged != null) 
			{
				PropertyChanged (this, new PropertyChangedEventArgs (propertyName));
			}
		}

		#endregion
	}
}

