using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jokester.ViewModels
{
    public partial class GeekyJokeViewModel: ObservableObject
    {
        [ObservableProperty]
        private string test;


        public GeekyJokeViewModel()
        {
            Test = "OTHER JOKE!";        
        }

    }
}
