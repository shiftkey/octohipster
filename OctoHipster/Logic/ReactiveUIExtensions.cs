using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OctoHipster.Logic
{
    public static class ReactiveUIExtensions
    {
        public static void TryExecute(this ICommand command, object o = null)
        {
            if (command.CanExecute(o))
            {
                command.Execute(o);
            }
        }
    }
}
