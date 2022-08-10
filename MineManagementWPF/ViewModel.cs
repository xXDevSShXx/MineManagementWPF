﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MineManagementWPF
{
    public class ViewModel : INotifyPropertyChanged
    {
        public static ViewModel Instance { get; set;}

        private Page currentPage;

        public Page CurrentPage
        {
            get
            { 
                return currentPage; 
            }
            set 
            { 
                currentPage = value;
                NotifyPropertyChanged("CurrentPage");
            }
        }


        public void NotifyPropertyChanged(string propname)
        {
            if(PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propname));
        }
         
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
