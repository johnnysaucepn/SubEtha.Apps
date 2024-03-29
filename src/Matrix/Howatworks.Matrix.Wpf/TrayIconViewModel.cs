﻿using System;
using System.ComponentModel;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Howatworks.Matrix.Core;
using Howatworks.Thumb.Wpf;

namespace Howatworks.Matrix.Wpf
{
    public class TrayIconViewModel : INotifyPropertyChanged
    {
        public event EventHandler OnExitApplication = delegate { };
        public event EventHandler OnAuthenticationRequested = delegate { };

        private string _tooltipText;
        public string TooltipText
        {
            get { return _tooltipText; }
            set { _tooltipText = value; NotifyPropertyChanged(); }
        }

        public static TrayIconViewModel Create(MatrixApp app) => new TrayIconViewModel(app);

        public TrayIconViewModel(MatrixApp app)
        {
            TooltipText = FormatLabel(DateTime.MinValue, DateTime.MinValue);
            // Update the tooltip description as new items come in

            app.Updates
                .Subscribe(x =>
            {
                var lastEntry = x.Value;
                var lastChecked = x.Timestamp;
                TooltipText = FormatLabel(lastEntry, lastChecked);
            });
        }

        private string FormatLabel(DateTimeOffset lastEntry, DateTimeOffset lastChecked)
        {
            if (lastChecked != DateTimeOffset.MinValue)
            {
                if (lastEntry != DateTimeOffset.MinValue)
                {
                    var labelPattern = Resources.NotifyIconLastUpdatedLabel.Replace("\\n", Environment.NewLine);
                    return string.Format(labelPattern, lastEntry.LocalDateTime.ToString("g"), lastChecked.LocalDateTime.ToString("g"));
                }
                return Resources.NotifyIconNeverUpdatedLabel;
            }
            return Resources.NotifyIconDefaultLabel;
        }

        /// <summary>
        /// Shuts down the application.
        /// </summary>
        public ICommand ExitApplicationCommand =>
            new DelegateCommand
            {
                CommandAction = () => OnExitApplication(this, EventArgs.Empty)
            };

        public ICommand ShowAuthDialogCommand =>
            new DelegateCommand
            {
                CommandAction = () => OnAuthenticationRequested(this, EventArgs.Empty)
            };

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
