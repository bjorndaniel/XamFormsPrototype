﻿using MvvmHelpers;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XamFormsPrototype.Enumerators;
using XamFormsPrototype.Helpers.Messages;
using XamFormsPrototype.UI.Helpers;

namespace XamFormsPrototype.UI.ViewModels
{
    public class SidebarPageViewModel : BaseViewModel
    {
        private List<MainPageMenuItem> _menuItems;
        private MainPageMenuItem _selectedItem;
        
        public string AppName => "Prototype";

        public List<MainPageMenuItem> MenuItems
        {
            get { return _menuItems; }
            set { SetProperty(ref _menuItems, value); }
        }

        public MainPageMenuItem SelectedItem
        {
            get { return _selectedItem; }
            set { SetProperty(ref _selectedItem, value); }
        }

        public ICommand ItemSelectedCommand => new Command(() => Navigate());

        public async Task<bool> Initialize()
        {
            MenuItems = new List<MainPageMenuItem>
            {
                new MainPageMenuItem
                {
                    Id = 1,
                    PageType = Pages.Users,
                    Title = "Users"
                },
                new MainPageMenuItem
                {
                    Id = 2,
                    PageType = Pages.Albums,
                    Title = "Albums"
                }
            };
            return await Task.FromResult(true);
        }

        private void Navigate()
        {
            MessagingCenter.Send(new NavigationMessage { Page = SelectedItem.PageType }, string.Empty);
            SelectedItem = null;
        }

    }
}
