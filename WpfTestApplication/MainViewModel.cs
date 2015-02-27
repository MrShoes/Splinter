using System;
using System.Linq.Expressions;
using Splinter;
using Splinter.Messaging;
using Splinter.Messaging.Helpers;

namespace WpfTestApplication
{
    class MainViewModel : ViewModel
    {
        private int _number1;

        public int Number1
        {
            get { return _number1; }
            set
            {
                SetPropertyAndNotify(ref _number1, value);
            }
        }

        private int _number2;

        public int Number2
        {
            get { return _number2; }
            set
            {
                SetPropertyAndNotify(ref _number2, value);
            }
        }

        private int _number3;

        public int Number3
        {
            get { return _number3; }
            set
            {
                SetPropertyAndNotify(ref _number3, value);
            }
        }

        private bool _isOne;

        public bool IsOne
        {
            get { return _isOne; }
            set
            {
                // Either of the below works.
                SetPropertyAndNotify(ref _isOne, value, new Expression<Func<object>>[] { () => CurrentViewModel }); 
                //SetPropertyAndNotify(ref _isOne, value, new[] { "CurrentViewModel" });
            }
        }

        public ViewModel CurrentViewModel
        {
            get { return IsOne ? (ViewModel)new ViewModel1() : new ViewModel2(); }
        }

        public MainViewModel()
        {
            IsOne = true;
            Broker.Current.Subscribe<SwitchViewMessage>(this, message => IsOne = !IsOne, ThreadOption.Dispatcher);
        }
    }
}
