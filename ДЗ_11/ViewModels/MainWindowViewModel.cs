﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ДЗ_11.Models;
using ДЗ_11.ViewModels.Base;

namespace ДЗ_11.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
         
        public IEnumerable<Client> Test { get; set; }
        public static ObservableCollection<Client> users = new ObservableCollection<Client>();


        //public void AddClients()
        //{
        //    Random r = new Random();
        //    for (int i = 0; i < 15; i++)
        //    {
        //        Client temp = new Client("name" + i, "LastName" + i, "Patroinymic" + i, (uint)(i * Math.Pow(10, 8))
        //                                , new Passport((ushort)(i * Math.Pow(10, 32)), (uint)(i * Math.Pow(10, 3))));
        //        users.Add(temp);
        //    }
        //    Test = users;
        //}

        public MainWindowViewModel()
        {
            for (int i = 0; i < 15; i++)
            {
                Client temp = new Client("name" + i, "LastName" + i, "Patroinymic" + i, (uint)(i * Math.Pow(10, 8))
                                        , new Passport((ushort)(i * Math.Pow(10, 32)), (uint)(i * Math.Pow(10, 3))));
                users.Add(temp);
            }
            Test = users;
        }
    }
}
