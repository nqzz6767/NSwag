﻿using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using MyToolkit.Mvvm;
using NSwag.Commands;
using NSwag.Commands.OpenApiGeneration.AspNetCore;
using NSwagStudio.ViewModels.SwaggerGenerators;

namespace NSwagStudio.Views.SwaggerGenerators
{
    public partial class AspNetCoreToSwaggerGeneratorView : ISwaggerGeneratorView
    {
        public AspNetCoreToSwaggerGeneratorView(AspNetCoreToSwaggerCommand command, NSwagDocument document)
        {
            InitializeComponent();
            ViewModelHelper.RegisterViewModel(Model, this);

            Model.Command = command;
            Model.Document = document;
        }

        private AspNetCoreToSwaggerGeneratorViewModel Model => (AspNetCoreToSwaggerGeneratorViewModel)Resources["ViewModel"];

        public string Title => "ASP.NET Core via API Explorer";

        public IOutputCommand Command => Model.Command;

        public Task<string> GenerateSwaggerAsync()
        {
            return Model.GenerateSwaggerAsync();
        }

        public override string ToString()
        {
            return Title;
        }
    }
}
