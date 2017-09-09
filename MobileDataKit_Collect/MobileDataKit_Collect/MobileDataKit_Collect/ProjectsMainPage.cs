
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using System.Text;

using Xamarin.Forms;

namespace MobileDataKit_Collect
{

    class NamedColorPage : ContentPage
    {
        public NamedColorPage(bool includeBigLabel)
        {
            // This binding is necessary to label the tabs in 
            //      the TabbedPage.
            this.SetBinding(ContentPage.TitleProperty, "Name");

            // BoxView to show the color.
            BoxView boxView = new BoxView
            {
                WidthRequest = 100,
                HeightRequest = 100,
                HorizontalOptions = LayoutOptions.Center
            };
            boxView.SetBinding(BoxView.ColorProperty, "Color");

            // Function to create six Labels.
            Func<string, string, Label> CreateLabel = (string source, string fmt) => {
                Label label = new Label
                {
                    FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                    HorizontalTextAlignment = TextAlignment.End
                };
                label.SetBinding(Label.TextProperty,
                    new Binding(source, BindingMode.OneWay, null, null, fmt));

                return label;
            };

            // Build the page
            this.Content = new StackLayout
            {
                Children = {
                    new StackLayout {
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        Children = {
                            CreateLabel ("Color.R", "R = {0:F2}"),
                            CreateLabel ("Color.G", "G = {0:F2}"),
                            CreateLabel ("Color.B", "B = {0:F2}"),
                        }
                    },
                    boxView,
                    new StackLayout {
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        Children = {
                            CreateLabel ("Color.Hue", "Hue = {0:F2}"),
                            CreateLabel ("Color.Saturation", "Saturation = {0:F2}"),
                            CreateLabel ("Color.Luminosity", "Luminosity = {0:F2}")
                        }
                    }
                }
            };

            // Add in the big Label at top for CarouselPage.
            if (includeBigLabel)
            {
                Label bigLabel = new Label
                {
                    FontSize = 50,
                    HorizontalOptions = LayoutOptions.Center
                };
                bigLabel.SetBinding(Label.TextProperty, "Name");

                (this.Content as StackLayout).Children.Insert(0, bigLabel);
            }
        }
    }


  public  class ProjectInfo
    {
        public ProjectInfo(string name)
        {
            this.Name = name;
           
        }
        public List<ProjectInfoFormsInfo> Forms { get; } = new List<ProjectInfoFormsInfo>();
        public string Name { private set; get; }

       

        public override string ToString()
        {
            return Name;
        }
    }


  public  class ProjectInfoFormsInfo
    {
        public static  ProjectInfoFormsInfo CurrentProjectInfoFormsInfo;
        public string ID { get; set; }

        public string Name { get; set; }

       
    }




    public class ProjectsMainPage : MasterDetailPage
    {
        public ProjectsMainPage()
        {
            this.Detail = new NavigationPage(new NamedColorPage(true));
            


            // Create the master page with the ListView.
            this.Master = new ContentPage
            {
                Title = "Projects",
                Content = new StackLayout
                {
                    Children =
                    {
                         new Label
            {
                Text = "Production",
                Font = Font.BoldSystemFontOfSize(30),
                HorizontalOptions = LayoutOptions.Center
            },
                       
                            new Label
            {
                Text = "Testing",
                Font = Font.BoldSystemFontOfSize(30),
                HorizontalOptions = LayoutOptions.Center
            },
                            new ListView
            {
               
            },

            new Label
            {
                Text = "Development",
                Font = Font.BoldSystemFontOfSize(30),
                HorizontalOptions = LayoutOptions.Center
            },new ListView
            {
               
            }
        }
                }
            };
        }


    }
        
}
