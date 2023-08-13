﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopupBarMobile.Services
{
    public interface INavigationService
    {
        Task NavigateAsync(string routeName);
        Task NavigateRelativeAsync(string routeName);
    }
}
