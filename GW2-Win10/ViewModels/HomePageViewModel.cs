using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;

namespace GW2_Win10.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        public AccountPartViewModel AccountPart { get; } = new AccountPartViewModel();
    }
}
