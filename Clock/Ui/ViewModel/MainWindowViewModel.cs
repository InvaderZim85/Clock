using System;
using System.Collections.Generic;
using System.Timers;
using Clock.DataObjects;
using ZimLabs.WpfBase;

namespace Clock.Ui.ViewModel
{
    /// <summary>
    /// Provides the logic for the <see cref="View.MainWindow"/>
    /// </summary>
    internal sealed class MainWindowViewModel : ObservableObject
    {
        /// <summary>
        /// The action to set the blackout dates
        /// </summary>
        private Action _setBlackoutDates;

        /// <summary>
        /// Backing field for <see cref="CurrentDateTime"/>
        /// </summary>
        private DateTime _currentDateTime = DateTime.Now;

        /// <summary>
        /// Gets or sets the current date
        /// </summary>
        public DateTime CurrentDateTime
        {
            get => _currentDateTime;
            set => SetField(ref _currentDateTime, value);
        }

        /// <summary>
        /// Contains the timer for the clock
        /// </summary>
        private readonly Timer _clockTimer = new Timer(1000);

        /// <summary>
        /// Backing field for <see cref="HourParts"/>
        /// </summary>
        private List<ClockPart> _hourParts;

        /// <summary>
        /// Gets or sets the hour parts
        /// </summary>
        public List<ClockPart> HourParts
        {
            get => _hourParts;
            private set => SetField(ref _hourParts, value);
        }

        /// <summary>
        /// Backing field for <see cref="SecondParts"/>
        /// </summary>
        private List<ClockPart> _secondParts;

        /// <summary>
        /// Gets or sets the second parts
        /// </summary>
        public List<ClockPart> SecondParts
        {
            get => _secondParts;
            private set => SetField(ref _secondParts, value);
        }

        /// <summary>
        /// Backing field for <see cref="AngleHour"/>
        /// </summary>
        private double _angleHour;
        /// <summary>
        /// Gets or sets the angle of the hour hand
        /// </summary>
        public double AngleHour
        {
            get => _angleHour;
            set => SetField(ref _angleHour, value);
        }

        /// <summary>
        /// Backing field for <see cref="AngleMin"/>
        /// </summary>
        private double _angleMin;
        /// <summary>
        /// Gets or sets the angle of the minute hand
        /// </summary>
        public double AngleMin
        {
            get => _angleMin;
            set => SetField(ref _angleMin, value);
        }

        /// <summary>
        /// Backing field for <see cref="AngleSec"/>
        /// </summary>
        private double _angleSec;
        /// <summary>
        /// Gets or sets the angle of the seconds hand
        /// </summary>
        public double AngleSec
        {
            get => _angleSec;
            set => SetField(ref _angleSec, value);
        }

        /// <summary>
        /// Backing field for <see cref="DateTime"/>
        /// </summary>
        private string _dateTimeValue = DateTime.Now.ToString("G");

        /// <summary>
        /// Gets or sets the current date / time
        /// </summary>
        public string DateTimeValue
        {
            get => _dateTimeValue;
            private set => SetField(ref _dateTimeValue, value);
        }

        /// <summary>
        /// Init the view model
        /// </summary>
        /// <param name="setBlackoutDates">The action to set the blackout dates</param>
        public void InitViewModel(Action setBlackoutDates)
        {
            _setBlackoutDates = setBlackoutDates;

            HourParts = ClockPart.GetHourParts();
            SecondParts = ClockPart.GetSecondParts();

            CurrentDateTime = DateTime.Now;

            _clockTimer.Elapsed += (s, e) =>
            {
                var time = DateTime.Now;
                DateTimeValue = time.ToString("G");
                AngleHour = GetAccurateAngle(time, true);
                AngleMin = GetAccurateAngle(time, false);
                AngleSec = time.Second * (360 / 60);

                if (CurrentDateTime.Date == DateTime.Now.Date) 
                    return;

                CurrentDateTime = DateTime.Now;
                _setBlackoutDates();
            };

            _clockTimer.Start();
        }

        /// <summary>
        /// Calculates the accurate angle for the hour
        /// </summary>
        /// <param name="time">The current time</param>
        /// <param name="hour">true to calculate the hour, false to calculate the minutes</param>
        /// <returns>The angle</returns>
        private static double GetAccurateAngle(DateTime time, bool hour)
        {
            var decimalValue = hour ? 1d / 60 * time.Minute : 1d / 60 * time.Second;

            double result;
            if (hour)
                result = (time.Hour + decimalValue) * (360d / 12);
            else
                result = (time.Minute + decimalValue) * (360d / 60);

            return result;
        }
    }
}
