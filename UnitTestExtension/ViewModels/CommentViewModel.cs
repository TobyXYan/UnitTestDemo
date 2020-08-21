using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestLibrary.ViewModels;

namespace UnitTestExtension.ViewModels
{
    public class CommentViewModel:BaseCommentViewModel
    {

        private string _comments;

        public string Comments
        {
            get { return _comments; }
            set { _comments = value;NotifyOfPropertyChange(()=>Comments); }
        }

        public override void SetData(string str)
        {
            Comments = str;
        }

        public override string GetData()
        {
            return Comments;
        }
    }
}
