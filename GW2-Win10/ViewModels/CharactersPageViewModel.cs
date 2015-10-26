using GW2_Win10.Services;
using Template10.Mvvm;

namespace GW2_Win10.ViewModels
{
    public class CharactersPageViewModel : ViewModelBase
    {
        public AccountPartViewModel AccountPart { get; } = new AccountPartViewModel();

        public bool CanViewCharacters => AccountPart?.Session?.HasPermission("characters") ?? false;
    }
}

