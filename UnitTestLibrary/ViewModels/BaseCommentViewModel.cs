using Caliburn.Micro;
using System;

namespace UnitTestLibrary.ViewModels
{
    public abstract class BaseCommentViewModel:Screen
    {
        public abstract void SetData(string str);
        public abstract string GetData();
    }
}
