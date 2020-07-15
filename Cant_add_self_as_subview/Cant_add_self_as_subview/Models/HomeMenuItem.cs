using System;
using System.Collections.Generic;
using System.Text;

namespace Cant_add_self_as_subview.Models {
    public enum MenuItemType {
        Browse,
        About
    }
    public class HomeMenuItem {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
