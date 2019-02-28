using System;
using System.Collections;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CalendarView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalendarView : ContentView
    {
        public static readonly BindableProperty ItemsSourceProperty = 
            BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(CalendarView), null, BindingMode.Default);
        
        public static readonly BindableProperty MonthProperty = 
            BindableProperty.Create(nameof(Month), typeof(int), typeof(CalendarView), default(int), BindingMode.Default);

        public IEnumerable ItemsSource
        {
            get => (IEnumerable)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public int Month
        {
            get => (int) GetValue(MonthProperty);
            set => SetValue(MonthProperty, value);
        }
        
        public CalendarView()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            var baseDate = DateTime.Now;
            var currentDate = DateTime.Now.AddDays(-DateTime.Now.Day + 1);
            var firstDate = currentDate;
            var daysInMonth = DateTime.DaysInMonth(baseDate.Year, baseDate.Month);
            var endDate = currentDate.AddDays(daysInMonth - 1);
            var index = GetStartDate(currentDate);
            currentDate = currentDate.AddDays(-index);
            for (var i = 0; i < 6; i++)
            {
                for (var j = 0; j < 7; j++)
                {
                    if (i == 0)
                    {
                        calendarContainer.Children.Add(GenerateDayLabel(currentDate), j, i);
                    }
                    else
                    {
                        calendarContainer.Children.Add(
                            GenerateDateLabel(currentDate,firstDate <= currentDate && endDate >= currentDate), j, i);
                    }
                    currentDate = currentDate.AddDays(1);
                }
            }
        }

        private View GenerateDayLabel(DateTime date)
        {
            var view = new Label
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.Transparent,
                TextColor = Color.Black,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                Text = date.ToString("dddd").FirstOrDefault().ToString()
            };
            return view;
        }

        private Label GenerateDateLabel(DateTime date, bool isCurrentMonth)
        {
            var view = new Label
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = isCurrentMonth ? Color.DimGray : Color.LightGray,
                TextColor = Color.White,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                Text = date.Day.ToString()
            };
            return view;
        }

        private int GetStartDate(DateTime date)
        {
            var dayOfWeek = date.DayOfWeek;
            switch (dayOfWeek)
            {
                case DayOfWeek.Sunday:
                    return 7;
                case DayOfWeek.Monday:
                    return 8;
                case DayOfWeek.Tuesday:
                    return 9;
                case DayOfWeek.Wednesday:
                    return 10;
                case DayOfWeek.Thursday:
                    return 11;
                case DayOfWeek.Friday:
                    return 12;
                default:
                    return 13;
            }
        }
    }
}
