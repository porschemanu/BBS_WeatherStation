using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBS_Wittlich.Weatherstation.UI.Components
{
    public class Diagrams
    {
        public void RadzenLineChart()
        {
                 < RadzenChart >
                    < RadzenLineSeries Data = "@weatherData" CategoryProperty = "timestamp" Title = "2022" LineType = "LineType.Dashed" ValueProperty = "value" >
       
                               < RadzenMarkers MarkerType = "MarkerType.Square" />
        
                            </ RadzenLineSeries >
        
                            < RadzenCategoryAxis Padding = "20" FormatString = "{0:mm:}" />
          
                          </ RadzenChart >
        }
    }
}
