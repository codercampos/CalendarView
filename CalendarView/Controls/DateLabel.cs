using System;
using Xamarin.Forms;

namespace CalendarView.Controls
{
    public class DateLabel : Label
    {
        public bool IsOnCurrentMonth { get; set; }

        public DateTime? Date => (DateTime?) BindingContext;
    }
}