﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ShoppingApp.Database;
using ShoppingApp.Core;
using OfficeOpenXml;

namespace ShoppingApp
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var database = new ShoppingListDBContext();

            database.Database.EnsureCreated();

            DatabaseCreationTool.MyDatabase = database;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }
    }
}
