using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portfolio.Project
{
    public partial class HomePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Add your image paths here
                string[] imagePaths = {
                "Azrof.jpg",
                "Azrof.jpg"
                
            };

                // Create slides
                foreach (string imgPath in imagePaths)
                {
                    Panel slide = new Panel();
                    slide.CssClass = "slide";
                    slide.Style.Add("background-image", $"url({ResolveUrl(imgPath)})");
                    sliderContainer.Controls.Add(slide);
                }

                // Create dots
                for (int i = 0; i < imagePaths.Length; i++)
                {
                    Panel dot = new Panel();
                    dot.CssClass = "dot";
                    if (i == 0) dot.CssClass += " active";
                    dotsContainer.Controls.Add(dot);
                }
            }
        }
    }
}