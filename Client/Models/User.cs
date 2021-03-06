﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Client.Models
{
    public class User : INotifyPropertyChanged
    {
        public User(Int32 id, String name, Boolean isCurrent = false)
        {
            _Color = Colors.GetColor();
            _Messages = new ObservableCollection<Message>();
            _MsgData = "";
            _Name = name;
            _Id = id;
            _isCurrent = isCurrent;
            _Status = UserStatus.Online;
        }

        public enum UserStatus
        {
            Online,
            Offline
        }

        // Properties
        public event PropertyChangedEventHandler PropertyChanged;

        private String _MsgData;
        public String MsgData
        {
            get { return _MsgData; }
            set
            {
                if (value != _MsgData)
                {
                    _MsgData = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private Int32 _Id;
        public Int32 Id { get { return _Id; } }

        private String _Name;
        public String Name { get { return _Name; } }

        private String _Color;
        public String Color { get { return _Color; } }

        private Boolean _isCurrent;
        public Boolean isCurrent { get { return _isCurrent; } }

        public Boolean isMember { get { return (this.Room != null && this.Room.Members.Contains(this)); } }
        public Boolean isWatcher { get { return (this.Room != null && this.Room.Watchers.Contains(this)); } }

        public int _NewMsgs;
        public String NewMsgs
        {
            get
            {
                if (_NewMsgs > 0) return _NewMsgs.ToString();
                else return "";
            }
        }

        private ObservableCollection<Message> _Messages;
        public ObservableCollection<Message> Messages { get { return _Messages; } }

        public Room Room { get; set; }

        private UserStatus _Status;
        public UserStatus Status { get { return _Status; } }

        public Boolean isOnline { get { return Status == UserStatus.Online; } }

        // Public Methods
        public void AddMessage(Message msg)
        {
            _Messages.Add(msg);
            if (msg.Type != Message.MessageType.Status)
            {
                _NewMsgs++;
                NotifyPropertyChanged("NewMsgs");
            }
        }

        public void ResetNewMsgs()
        {
            _NewMsgs = 0;
            NotifyPropertyChanged("NewMsgs");
        }

        public void SetOffline()
        {
            _Status = UserStatus.Offline;
            NotifyPropertyChanged("Status");
            NotifyPropertyChanged("isOnline");
        }

        public void SetOnline()
        {
            _Status = UserStatus.Online;
            NotifyPropertyChanged("Status");
            NotifyPropertyChanged("isOnline");
        }

        // Private Methods
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
