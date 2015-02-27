using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Splinter;
using Splinter.Messaging;

namespace WpfTestApplication
{
    class SwitchViewCommand : Command
    {
        public override void Execute(object parameter)
        {
            Broker.Current.Publish(new SwitchViewMessage());
        }
    }
}
