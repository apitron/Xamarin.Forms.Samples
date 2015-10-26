using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Apitron.PDF.Kit;
using Apitron.PDF.Kit.Styles;
using Thickness = Apitron.PDF.Kit.Styles.Thickness;
using Style = Apitron.PDF.Kit.Styles.Style;
using Font = Apitron.PDF.Kit.Styles.Text.Font;
using Apitron.PDF.Kit.FlowLayout.Content.Controls;
using System.IO;
using Apitron.PDF.Kit.FixedLayout.Resources;
using Apitron.PDF.Kit.Interactive.Forms;
using Apitron.PDF.Kit.FlowLayout.Content;
using Apitron.PDF.Kit.Styles.Appearance;

namespace PDFFormCreationSample
{
	public partial class MyPage : ContentPage
	{
		Employee currentEmployee;

		public MyPage ()
		{
			InitializeComponent ();

			BindingContext = currentEmployee = new Employee ();
		}

		public void OnSaveClicked(object sender, EventArgs args)
		{
			// create flow document and register necessary styles
			FlowDocument doc = new FlowDocument();
			doc.Margin = new Thickness (10,10,10,10);
			// the style for all document's textblocks
			doc.StyleManager.RegisterStyle("TextBlock, TextBox", new Style()
				{				
					Font = new Font("Helvetica",20),
					Color = RgbColors.Black,
					Display = Display.Block						
				});

			// the style for the section that contains employee data
			doc.StyleManager.RegisterStyle ("#border", new Style ()
				{
					Padding = new Thickness(10,10,10,10),
					BorderColor = RgbColors.DarkRed,
					Border = new Border(5),
					BorderRadius=5
				}
			);

			// add PDF form fields for later processing
			doc.Fields.Add (new TextField ("firstName", currentEmployee.FirstName));
			doc.Fields.Add (new TextField ("lastName", currentEmployee.LastName));
			doc.Fields.Add (new TextField ("position", currentEmployee.CurrentPosition));

			// create section and add text block inside
			Section section = new Section (){Id="border"};

			//  ios PDF preview doesn't display fields correctly, 
			//  uncomment this code to use simple text blocks instead of text boxes	  		  
//			section.Add(new TextBlock(string.Format("First name: {0}",currentEmployee.FirstName)));
//		    section.Add(new TextBlock(string.Format("Last name: {0}",currentEmployee.LastName)));
//		    section.Add(new TextBlock(string.Format("Position: {0}",currentEmployee.CurrentPosition)));
			    
			section.Add(new TextBlock("First name: "));
			section.Add(new TextBox("firstName"));
			section.Add(new TextBlock("Last name: "));
			section.Add(new TextBox("lastName"));
			section.Add(new TextBlock("Position: "));
			section.Add(new TextBox("position"));

			doc.Add (section);

			// get io service and generate output file path
			var fileManager = DependencyService.Get<IFileIO>();
			string filePath = Path.Combine (fileManager.GetMyDocumentsPath (), "form.pdf");

			// generate document
			using(Stream outputStream = fileManager.CreateFile(filePath))
		    {
				doc.Write (outputStream, new ResourceManager());
			}

			// request preview
			DependencyService.Get<IPDFPreviewProvider>().TriggerPreview (filePath);
		}
	}
}

