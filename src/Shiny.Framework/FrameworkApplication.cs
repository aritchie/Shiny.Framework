﻿using System;
using Prism.DryIoc;
using Prism.Ioc;


namespace Shiny
{
    public abstract class FrameworkApplication : PrismApplication
    {
        protected override async void OnInitialized()
        {
            if (Extensions.UsingXfMaterialDialogs)
                XF.Material.Forms.Material.Init(this);

            await FrameworkStartup.Current.RunApp(this.NavigationService);
        }


        protected override void RegisterTypes(IContainerRegistry containerRegistry)
            => FrameworkStartup.Current.ConfigureApp(this, containerRegistry);
    }
}
