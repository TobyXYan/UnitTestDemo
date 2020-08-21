
using Caliburn.Micro;
using UnitTestDemo.Common;
using UnitTestLibrary.ViewModels;

namespace UnitTestDemo.ViewModels
{
    public class ShellViewModel:Conductor<IScreen>.Collection.AllActive
    {
        private int _age;
        private const int MinAge = 18;
        private const int MaxAge = 68;

        public int Age
        {
            get { return _age; }
            set { ValidateAge(value); NotifyOfPropertyChange(nameof(Age)); }
        }

        public BaseCommentViewModel CommentVm { get; private set; }

        private void ValidateAge(int val)
        {
            if (val < MinAge)
                _age = MinAge;
            if (val > MaxAge)
                _age = MaxAge;
        }

        public ShellViewModel()
        {
            CommentVm = UnitIoC.Get<BaseCommentViewModel>();
            CommentVm.SetData("Toby's Comment");
            ActivateItem(CommentVm);
            Items.Add(CommentVm);
        }

        public void OnCalculate()
        {
            var calService = UnitIoC.Get<ICalculateService>();
            Age = calService.DoMathWork(Age);
        }
    }
}
