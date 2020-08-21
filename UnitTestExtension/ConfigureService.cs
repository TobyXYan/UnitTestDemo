using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestExtension.ViewModels;
using UnitTestLibrary;
using UnitTestLibrary.ViewModels;

namespace UnitTestExtension
{
    public class ConfigureService : IConfigureService
    {
        public void Configure(SimpleContainer container)
        {
            container.PerRequest<BaseCommentViewModel, CommentViewModel>();
        }
    }
}
