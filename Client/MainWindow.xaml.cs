﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Pages.SignInPage SignInPage;
        public Pages.MainPage MainPage;

        public Connection Connection { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            SignInPage = new Pages.SignInPage(this);
            MainPage = new Pages.MainPage(this);

            Connection = Connection.Instance;

            Connection.Client.RaiseUpdateUserId += SignInPage.OnRaiseUpdateId;
            Connection.Client.RaiseUpdateUserId += MainPage.OnRaiseUpdateId;
            Connection.Client.RaiseUpdateUserList += MainPage.OnRaiseUpdateUserList;
            Connection.Client.RaiseSignedIn += MainPage.OnRaiseSignedIn;
            Connection.Client.RaiseSignedOut += MainPage.OnRaiseSignedOut;

        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            this.MainFrame.Content = this.SignInPage;
        }

        private void Window_Closed_1(object sender, EventArgs e)
        {
            this.Connection.Dispose();
        }


    }
}